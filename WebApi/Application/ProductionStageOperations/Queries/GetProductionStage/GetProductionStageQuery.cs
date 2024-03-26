using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.ProductionStageOperations.Queries.GetProductionStage
{
    
    public class GetProductionStageQuery
    {
        private readonly IOperationsManagmentDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetProductionStageQuery(IOperationsManagmentDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<ProductionStageViewModel> Handle()
        {
            var productionStage = _dbContext.productionstages.OrderBy(x => x.Id).ToList();
            List<ProductionStageViewModel> vm = _mapper.Map<List<ProductionStageViewModel>>(productionStage);
            return vm;
        }
    }

    public class ProductionStageViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}