using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WebApi.Application.TokenOperations.Models;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommand
    {
        public CreateTokenModel? Model { get; set; }
        private readonly  UserManager<CustomUser> _userManager;
        private readonly IConfiguration _configuration;


        public CreateTokenCommand(IConfiguration configuration, UserManager<CustomUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }



        public async Task<Token> Handle()
        {
            if (Model is null)
                throw new ArgumentNullException(nameof(Model), "Model cannot be null");
            
            var user = await _userManager.FindByNameAsync(Model.Name);
            if(user != null && Model.Password != null  && await _userManager.CheckPasswordAsync(user, Model.Password))
            {
                TokenHandler handler = new TokenHandler(_configuration, _userManager);

                Token token = await handler.CreateAccessToken(user);
                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenEndDate = token.Expiration.AddMinutes(5);
                await _userManager.UpdateAsync(user);
                return token;
            }
            else
            {
                throw new InvalidOperationException("Kullanici Adi veya Sifre hatali");
            }
                                    
        }
     
    }

    public class CreateTokenModel
    {      
        public string? Name { get; set; }
        public string? Password { get; set; }
    }
}
