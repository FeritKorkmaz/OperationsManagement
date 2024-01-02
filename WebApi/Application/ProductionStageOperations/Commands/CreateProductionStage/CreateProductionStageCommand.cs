using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.ProductionStageOperations.Commands.CreateProductionStage
{
    public class CreateProductionStageCommand
    {
        public CreateProductionStageModel? Model { get; set; }
        private readonly IOperationsManagmentDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateProductionStageCommand(IOperationsManagmentDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            if (Model is null)
                throw new ArgumentNullException(nameof(Model), "Model cannot be null");
            
            var productionStage = _dbContext.productionstages.SingleOrDefault(x => x.Name == Model.Name);
            if(productionStage is not null)
                throw new InvalidOperationException("productionStage already exists");            
            productionStage = _mapper.Map<ProductionStage>(Model);
            _dbContext.productionstages.Add(productionStage);
            _dbContext.SaveChanges();
        }
        
    }
    public class CreateProductionStageModel
    {
        public string? Name { get; set; }
       
    }
  
    
}