using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.ProductionStageOperations.Commands.CreateProductionStage;
using WebApi.Application.ProductionStageOperations.Queries.GetProductionStage;
using WebApi.DBOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class ProductionStageController : ControllerBase
    {
        private readonly IOperationsManagmentDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductionStageController(IOperationsManagmentDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetProductionStages()
        {
            GetProductionStageQuery query = new GetProductionStageQuery(_dbContext, _mapper);
            var result = query.Handle();
            return Ok(result);
        }
       
        
        [HttpPost]
        public ActionResult CreateProductionStage([FromBody] CreateProductionStageModel model)
        {
            CreateProductionStageCommand command = new CreateProductionStageCommand(_dbContext, _mapper);
            command.Model = model;
                    
            command.Handle();                        
            return Ok();
        }

        [HttpDelete("{Id}")]
        public ActionResult DeleteProductionStage(int Id)
        {
            DeleteProductionStageCommand command = new DeleteProductionStageCommand(_dbContext);
            command.productionStageId = Id;
            command.Handle();
            return Ok();
        }

        [HttpPut("{Id}")]
        public ActionResult UpdateProductionStage(int Id, [FromBody] UpdateProductionStageModel model)
        {
            UpdateProductionStageCommand command = new UpdateProductionStageCommand(_dbContext, _mapper);
            command.productionStageId = Id;           
            command.Model = model;
            command.Handle();
            return Ok();
        }
    }
    
}