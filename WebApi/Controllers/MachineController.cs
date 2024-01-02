using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.MachineOperations.Commands.UpdateMachine;
using WebApi.Application.MachineOperations.Queries.GetMachine;
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
        public ActionResult GetIncompleteMachineQuery()
        {
            GetMachineQery query = new GetMachineQery(_dbContext, _mapper);
            var result = query.Handle(isDone: false);
            return Ok (result);
        }
        [HttpGet("complete")]
        public ActionResult GetMachineCompleteQuery()
        {
            GetMachineQery query = new GetMachineQery(_dbContext, _mapper);
            var result = query.Handle(isDone: true);
            return Ok (result);
        }
        [HttpGet("machines/stage")]
        public ActionResult GetMachinesByStage()
        {
            GetMachinesByStage query = new GetMachinesByStage(_dbContext, _mapper);
            var result = query.Handle(stage_id: 1);
            return Ok (result);
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