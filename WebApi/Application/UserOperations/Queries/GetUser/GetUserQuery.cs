using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.UserOperations.Queries.GetUser
{
    
    public class GetUserQuery
    {
        private readonly IMapper _mapper;
        private readonly UserManager<CustomUser> _userManager;

        public GetUserQuery(IMapper mapper, UserManager<CustomUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }
        public async  Task<List<UserViewModel>> Handle()
        {
            var existingUser = await  _userManager.Users.OrderBy(x => x.Id).ToListAsync();
            List<UserViewModel> userViewModels = _mapper.Map<List<UserViewModel>>(existingUser);

            foreach (var user in userViewModels)
            {
                var currentUser = await _userManager.FindByNameAsync(user.Name);
                if (currentUser != null)
                {
                    var roles = await _userManager.GetRolesAsync(currentUser);
                    user.Role = roles.FirstOrDefault();
                }
                
            }


            return userViewModels;
        }
        
    }

    public class UserViewModel
    {
        public string? Id { get; set; } 
        public string? Name { get; set; }
        public string? Role { get; set; }
    }
}