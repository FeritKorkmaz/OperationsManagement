using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.MachineOperations.Commands.CreateMachine;
using WebApi.Application.MachineOperations.Commands.DeleteMachine;
using WebApi.Application.MachineOperations.Commands.UpdateMachine;
using WebApi.Application.MachineOperations.Queries.GetMachine;
using WebApi.Application.ProductionStageOperations.Commands.CreateProductionStage;
using WebApi.DBOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class MachineController : ControllerBase
    {
        private readonly IOperationsManagmentDbContext _dbContext;
        private readonly IMapper _mapper;

        public MachineController(IOperationsManagmentDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpGet("incomplete")]
        public ActionResult GetMachine()
        {
            GetMachineQery query = new GetMachineQery(_dbContext, _mapper);
            var result = query.Handle();
            return Ok (result);
        }
        [HttpGet("complete")]
        public ActionResult GetMachineComplete()
        {
            GetMachineCompleteQery query = new GetMachineCompleteQery(_dbContext, _mapper);
            var result = query.Handle();
            return Ok (result);
        }
        
        [HttpPost]
        public ActionResult CreateMachine([FromBody] CreateMachineModel model)
        {
            CreateMachineCommand command = new CreateMachineCommand(_dbContext, _mapper);
            command.Model = model;
                    
            command.Handle();                        
            return Ok();
        }

        [HttpDelete("{Id}")]
        public ActionResult DeleteMachine(int Id)
        {
            DeleteMachineCommand command = new DeleteMachineCommand(_dbContext);
            command.MachineId = Id;
            command.Handle();
            return Ok();
        }

        [HttpPut("{Id}")]
        public ActionResult UpdateMachine(int Id)
        {
            
            UpdateMachineCommand command = new UpdateMachineCommand(_dbContext, _mapper);
            command.MachineId = Id;
            UpdateMachineModel model = new UpdateMachineModel();           
            command.Model = model;
            command.Handle();
            return Ok();
        }
        
        [HttpPut("{Id}ProductionStage")]
        public ActionResult UpdateMachineStage(int Id, [FromBody] UpdateMachineStageModel model)
        {
            
            UpdateMachineStageCommand command = new UpdateMachineStageCommand(_dbContext, _mapper);
           command.MachineId = Id;
           command.Model = model;
           command.Handle();
           return Ok();
        }
    }
    
}