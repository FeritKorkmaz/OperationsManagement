using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.MachineOperations.Commands.UpdateMachine
{
    public class UpdateMachineCommand
    {
        public int MachineId { get; set; }

        public UpdateMachineModel? Model { get; set; }
        private readonly IOperationsManagmentDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateMachineCommand(IOperationsManagmentDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            
            
            var machine = _dbContext.machines.SingleOrDefault(x => x.Id == MachineId);
            if(machine is not null)
            {
                int userStageId = GetUserStageId();
                int currentMachineStageId = machine.ProductionStageId == null ? throw new InvalidOperationException("current machine stage id is null") : machine.ProductionStageId.Value;
                if (userStageId == currentMachineStageId && Model != null)
                {
                    var newStage = _dbContext.productionstages.FirstOrDefault(x => x.Id > currentMachineStageId);
                    Model.ProductionStageId = newStage != null ? newStage.Id : currentMachineStageId;
                }
                else
                {   
                    throw new InvalidOperationException("User does not have permission to update the machine at this stage.");
                }
                                                            
                machine = _mapper.Map(Model, machine);
                _dbContext.SaveChanges();
                
            }
            else
                throw new InvalidOperationException("machine does not exist");
                  
              
            
        }
        public int GetUserStageId ()
        {
            return 1;
        }
        
    }
    public class UpdateMachineModel
    {
        public int ProductionStageId { get; set; }
       
    }
  
    
}