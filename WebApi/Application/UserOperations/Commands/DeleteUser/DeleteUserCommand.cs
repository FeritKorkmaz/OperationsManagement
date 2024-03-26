using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Application.UserOperations.Commands.DeleteUser
{
    public class DeleteUserCommand
    {
        public string? UserName { get; set; }
        private readonly UserManager<CustomUser> _userManager;

        public DeleteUserCommand(UserManager<CustomUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task Handle()
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == UserName);
            if(user is null)
                throw new InvalidOperationException("user not found");
            else    
            await _userManager.DeleteAsync(user);
        }
        
    }
    
}