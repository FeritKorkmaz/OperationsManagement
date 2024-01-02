using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.ProductionStageOperations.Commands.CreateProductionStage
{
    public class UpdateProductionStageCommand
    {
        public int productionStageId { get; set; }
        public UpdateProductionStageModel? Model { get; set; }
        private readonly IOperationsManagmentDbContext _dbContext;
        private readonly IMapper _mapper;
        public UpdateProductionStageCommand(IOperationsManagmentDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var productionStage = _dbContext.productionstages.SingleOrDefault(x => x.Id == productionStageId);
            if (productionStage is null)
                throw new InvalidOperationException("productionStage not found");
            if (Model is null)
                throw new InvalidOperationException("Model cannot be null");
            
            productionStage = _mapper.Map(Model, productionStage);
            _dbContext.SaveChanges();
            
        }
    }

    public class UpdateProductionStageModel
    {
        public string? Name { get; set; }
    }
}