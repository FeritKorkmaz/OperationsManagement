using WebApi.DBOperations;

namespace WebApi.Application.MachineOperations.Commands.DeleteMachine
{
    public class DeleteMachineCommand
    {
        public int MachineId { get; set; }
        private readonly IOperationsManagmentDbContext _dbContext;

        public DeleteMachineCommand(IOperationsManagmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var machine = _dbContext.machines.SingleOrDefault(x => x.Id == MachineId);
            if(machine is null)
                throw new InvalidOperationException("machine does not exist");
            _dbContext.machines.Remove(machine);
            _dbContext.SaveChanges();
            
        }
        
    }

    
}