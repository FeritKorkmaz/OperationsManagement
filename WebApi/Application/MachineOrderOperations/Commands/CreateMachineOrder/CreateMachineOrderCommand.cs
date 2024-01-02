using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.MachineOrderOperations.Commands.CreateMachineOrder
{
    public class CreateMachineOrderCommand
    {
        public CreateMachineOrderModel? Model { get; set; }
        private readonly IOperationsManagmentDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateMachineOrderCommand(IOperationsManagmentDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            if (Model is null)
                throw new InvalidOperationException("Model is null");

            var machineOrder = _mapper.Map<MachineOrder>(Model);
            machineOrder.Machines = new List<Machine>();            
            for (int i = 0; i < Model.OrderQuantity; i++)
            {
                var machine = new Machine();
                machine = _mapper.Map(Model, machine);
                machineOrder.Machines.Add(machine);                             
            }
            _dbContext.machines.AddRange(machineOrder.Machines);
            _dbContext.machineorders.Add(machineOrder);
            _dbContext.SaveChanges();
        }
        
    }
    public class CreateMachineOrderModel
    {
        public string? CompanyName { get; set; }
        public int  OrderQuantity { get; set; }
        public string? MachineType { get; set; }
    }
    
}