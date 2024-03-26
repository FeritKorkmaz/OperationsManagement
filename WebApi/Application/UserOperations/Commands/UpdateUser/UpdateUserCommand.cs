using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.UserOperations.Commands.UpdateUser
{
    public class UpdateUserCommand
    {
        public string? UserName { get; set; }
        public UpdateUserModel? Model { get; set; }
        private readonly IMapper _mapper;
        private readonly UserManager<CustomUser> _userManager;

        public UpdateUserCommand(IMapper mapper, UserManager<CustomUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task Handle()
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == UserName);
            if (user is null)
                throw new InvalidOperationException("user not found");
            if (Model is null)
                throw new InvalidOperationException("Model cannot be null");
            
            user = _mapper.Map(Model, user);
            await _userManager.UpdateAsync(user);
       
        }
    }
    public class UpdateUserModel
    {
        public string? Name { get; set; }
        public string? Role { get; set; }
    }
}