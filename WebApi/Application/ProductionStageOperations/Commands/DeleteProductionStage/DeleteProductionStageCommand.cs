using WebApi.DBOperations;

namespace WebApi.Application.ProductionStageOperations.Commands.CreateProductionStage
{
    public class DeleteProductionStageCommand
    {
        public int productionStageId { get; set; }
        private readonly IOperationsManagmentDbContext _dbContext;

        public DeleteProductionStageCommand(IOperationsManagmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var productionStage = _dbContext.productionstages.SingleOrDefault(x => x.Id == productionStageId);
            if (productionStage is null)
                throw new InvalidOperationException("productionStage not found");

            var machine = _dbContext.machines.SingleOrDefault(x => x.ProductionStageId == productionStage.Id);
            if (machine is not null)
                machine.ProductionStageId = null;
                
            _dbContext.productionstages.Remove(productionStage);
            _dbContext.SaveChanges();
        }
    }
}