using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommand
    {
        public CreateUserModel? Model { get; set; }
        private readonly UserManager<CustomUser> _userManager;
        private readonly IMapper _mapper;

        public CreateUserCommand(IMapper mapper, UserManager<CustomUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task Handle()
        {
            if (Model is null)
                throw new ArgumentNullException(nameof(Model), "Model cannot be null");
            
            var existingUser  = await _userManager.FindByNameAsync(Model.Name);
            if(existingUser is not null)
                throw new InvalidOperationException("User already exists"); 

            var newUser = _mapper.Map<CustomUser>(Model);
            try
            {
                var userCreationResult = await _userManager.CreateAsync(newUser, Model.Password);

                if (userCreationResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, Model.Role);
                }
                else
                {
                    var errors = userCreationResult.Errors.Select(e => e.Description);
                    throw new InvalidOperationException($"User creation failed. Errors: {string.Join(", ", errors)}");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred during user creation: {ex.Message}", ex);
            }
        }

    }
        
  
    public class CreateUserModel
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
  
    
}