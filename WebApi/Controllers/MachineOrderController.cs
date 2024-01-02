using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.MachineOrderOperations.Commands.CreateMachineOrder;
using WebApi.Application.MachineOrderOperations.Commands.DeleteMachineOrder;
using WebApi.Application.MachineOrderOperations.Commands.UpdateMachineOrder;
using WebApi.Application.MachineOrderOperations.Queries.GetMachineOrder;
using WebApi.Application.MachineOrderOperations.Queries.GetMachineOrderDetail;
using WebApi.DBOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class MachineOrderController : ControllerBase
    {
        private readonly IOperationsManagmentDbContext _dbContext;
        private readonly IMapper _mapper;

        public MachineOrderController(IOperationsManagmentDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpGet("incompleteOrders")]
        public ActionResult GetOrderQuery()
        {
            GetOrderQuery query = new GetOrderQuery(_dbContext, _mapper);
            var result = query.Handle();
            return Ok (result);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetOrderDetailQuery query = new GetOrderDetailQuery(_dbContext, _mapper);
            query.OrderId = id;            

            var result = query.Handle();          
            return Ok(result); 
        }
        // [HttpGet("complete")]
        // public ActionResult GetMachineComplete()
        // {
        //     GetMachineCompleteQery query = new GetMachineCompleteQery(_dbContext, _mapper);
        //     var result = query.Handle();
        //     return Ok (result);
        // }
        
        [HttpPost]
        public ActionResult CreateMachine([FromBody] CreateMachineOrderModel model)
        {
            CreateMachineOrderCommand command = new CreateMachineOrderCommand(_dbContext, _mapper);
            command.Model = model;
                    
            command.Handle();                        
            return Ok();
        }

        [HttpDelete("{Id}")]
        public ActionResult DeleteMachineOrder(int Id)
        {
            DeleteMachineOrderCommand command = new DeleteMachineOrderCommand(_dbContext);
            command.OrderId = Id;
            command.Handle();
            return Ok();
        }

        [HttpPut("{Id}")]
        public ActionResult UpdateMachineOrder(int Id, [FromBody] UpdateMachineOrderModel model)
        {
            UpdateMachineOrderCommand command = new UpdateMachineOrderCommand(_dbContext, _mapper);
            command.OrderId = Id;           
            command.Model = model;
            command.Handle();
            return Ok();
        }
    }
    
}