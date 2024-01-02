using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.MachineOperations.Commands.UpdateMachine
{
    public class UpdateMachineStageCommand
    {
        public int MachineId { get; set; }

        public UpdateMachineStageModel? Model { get; set; }
        private readonly IOperationsManagmentDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateMachineStageCommand(IOperationsManagmentDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            
            
            var machine = _dbContext.machines.SingleOrDefault(x => x.Id == MachineId);
            if(machine is not null && Model != null)
            {
                var productionstages = _dbContext.productionstages.SingleOrDefault(x => x.Id == Model.ProductionStageId);
                if(productionstages is null)
                    throw new InvalidOperationException("production stage does not exist");                                            
                machine = _mapper.Map(Model, machine);
                _dbContext.SaveChanges(); 
                
            }
            else
            {
                throw new InvalidOperationException("machine does not exist");                  
            }    
            
        }
    
    }
    public class UpdateMachineStageModel
    {
        public int ProductionStageId { get; set; }
       
    }
  
    
}