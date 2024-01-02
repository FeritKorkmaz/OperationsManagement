using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.MachineOperations.Commands.CreateMachine
{
    public class CreateMachineCommand
    {
        public CreateMachineModel? Model { get; set; }
        private readonly IOperationsManagmentDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateMachineCommand(IOperationsManagmentDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            if (Model is null)
                throw new ArgumentNullException(nameof(Model), "Model cannot be null");
            
            var machine = _dbContext.machines.Include(x => x.ProductionStage).SingleOrDefault(x => x.Name == Model.Name);
            if(machine is not null)
                throw new InvalidOperationException("machine already exists");            
            machine = _mapper.Map<Machine>(Model);
            _dbContext.machines.Add(machine);
            _dbContext.SaveChanges();
        }
        
    }
    public class CreateMachineModel
    {
        public string? Name { get; set; }
    }
  
    
}