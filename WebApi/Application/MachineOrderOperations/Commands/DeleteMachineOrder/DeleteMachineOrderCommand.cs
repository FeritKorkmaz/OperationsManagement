using WebApi.DBOperations;

namespace WebApi.Application.MachineOrderOperations.Commands.DeleteMachineOrder
{
    public class DeleteMachineOrderCommand
    {
        public int OrderId { get; set; }
        private readonly IOperationsManagmentDbContext _dbContext;

        public DeleteMachineOrderCommand(IOperationsManagmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var machineOrder = _dbContext.machineorders.SingleOrDefault(x => x.Id == OrderId);
            if(machineOrder is null)
                throw new InvalidOperationException("machine order does not exist");
            if (machineOrder.Machines is not null) 
            {
                _dbContext.machineorders.Entry(machineOrder).Collection(mo => mo.Machines).Load();
                _dbContext.machines.RemoveRange(machineOrder.Machines);
                machineOrder.Machines.Clear();
            }
            _dbContext.machineorders.Remove(machineOrder);
            _dbContext.SaveChanges();
            
        }
        
    }

    
}