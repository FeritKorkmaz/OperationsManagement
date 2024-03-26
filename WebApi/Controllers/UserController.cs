using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.TokenOperations.Models;
using WebApi.Application.UserOperations.Commands.CreateToken;
using WebApi.Application.UserOperations.Commands.CreateUser;
using WebApi.Application.UserOperations.Commands.DeleteUser;
using WebApi.Application.UserOperations.Commands.UpdateUser;
using WebApi.Application.UserOperations.Queries.GetUser;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    
    public class UserController : ControllerBase
    { 
        private readonly IOperationsManagmentDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly UserManager<CustomUser> _userManager;



        public UserController(IOperationsManagmentDbContext dbContext, IConfiguration configuration, IMapper mapper, UserManager<CustomUser> userManager)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost("connect/token")]
        public async Task<ActionResult<Token>> CreateToken([FromBody] CreateTokenModel model)
        {
            CreateTokenCommand command = new CreateTokenCommand(_configuration, _userManager);
            command.Model = model;
            var token = await command.Handle();
            return token;
        }
        [HttpPost]
        public async Task<ActionResult> AddUser([FromBody] CreateUserModel model)
        {
            CreateUserCommand command = new CreateUserCommand(_mapper, _userManager);
            command.Model = model;
            await command.Handle();
            return Ok();
        }        
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<ActionResult> GetUsers()
        {
            try
            {
                GetUserQuery query = new GetUserQuery(_mapper, _userManager);
                var result = await query.Handle();
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Hata durumlarında uygun HTTP durumu ve hata mesajını dönebilirsiniz
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpDelete("{UserName}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<ActionResult> DeleteUser(string UserName)
        {
            try
            {
                DeleteUserCommand command = new DeleteUserCommand(_userManager);
                command.UserName = UserName;
                await command.Handle();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("{UserName}")]
        public async Task<ActionResult> UpdateUser(string UserName, [FromBody] UpdateUserModel model)
        {
            try
            {
                UpdateUserCommand command = new UpdateUserCommand(_mapper, _userManager);
                command.UserName = UserName;
                command.Model = model;
                await command.Handle();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
      
    }
}
 