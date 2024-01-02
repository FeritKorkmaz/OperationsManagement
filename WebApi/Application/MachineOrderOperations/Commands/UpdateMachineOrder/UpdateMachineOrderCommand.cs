using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.MachineOrderOperations.Commands.UpdateMachineOrder
{
    public class UpdateMachineOrderCommand
    {
        public int OrderId { get; set; }

        public UpdateMachineOrderModel? Model { get; set; }
        private readonly IOperationsManagmentDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateMachineOrderCommand(IOperationsManagmentDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        { 
            var machineOrder = _dbContext.machineorders.SingleOrDefault(x => x.Id == OrderId);
            if(machineOrder is null)
                throw new InvalidOperationException("machine order does not exist");

            machineOrder = _mapper.Map(Model, machineOrder);

            if (machineOrder.Machines is not null) 
            {
                _dbContext.machineorders.Entry(machineOrder).Collection(mo => mo.Machines).Load();
                _dbContext.machines.RemoveRange(machineOrder.Machines);
                machineOrder.Machines.Clear();
                for (int i = 0; i < machineOrder.OrderQuantity; i++)
                {
                    var machine = new Machine();
                    machine = _mapper.Map(Model, machine);
                    machineOrder.Machines.Add(machine);
                }
            }
            _dbContext.SaveChanges();
        }
        
        
    }
    public class UpdateMachineOrderModel
    {
        public string? CompanyName { get; set; }
        public int  OrderQuantity { get; set; }
        public string? MachineType { get; set; } 
       
    }
  
    
}