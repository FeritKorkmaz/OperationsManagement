using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.Application.UserOperations.Commands.CreateToken;
using WebApi.Entities;

namespace WebApi.Application.TokenOperations.Models
{
   public class TokenHandler
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<CustomUser> _userManager;

    public TokenHandler(IConfiguration configuration, UserManager<CustomUser> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }
    
        public async Task<Token> CreateAccessToken(CustomUser user)
        {
            Token tokenmodel = new Token();

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration ["Token:SecurityKey"] ?? "default-key"));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            tokenmodel.Expiration = DateTime.UtcNow.AddMinutes(15);

            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // user.Id kullanıcı kimliğidir
                    new Claim(ClaimTypes.Name, user.UserName),
                   // Diğer claim'ler...
                }),
                Expires = tokenmodel.Expiration,
                SigningCredentials = signingCredentials,
                Audience = _configuration["Token:Audience"],
                Issuer = _configuration["Token:Issuer"],
                NotBefore = DateTime.UtcNow
            };
            var roles = await _userManager.GetRolesAsync(user);
            tokenDescriptor.Subject.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken? securityToken = tokenHandler.CreateToken(tokenDescriptor) as JwtSecurityToken;

            // Token Uretiliyor
            tokenmodel.AccessToken = tokenHandler.WriteToken(securityToken);
            tokenmodel.RefreshToken = CreateRefreshToken();
            return tokenmodel;
        }

        public string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

 
     
    }

    // public async Task<Token> CreateAccessToken(CreateTokenModel model)
    // {
    //     CustomUser user = await _userManager.FindByNameAsync(model.Name);

    //     if (user != null)
    //     {
    //         List<Claim> claims = await GetClaimsAsync(user);

    //         SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"] ?? "default-key"));
    //         SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    //         DateTime expiration = DateTime.UtcNow.AddMinutes(15);

    //         var tokenDescriptor = new SecurityTokenDescriptor
    //         {
    //             Subject = new ClaimsIdentity(claims),
    //             Expires = expiration,
    //             SigningCredentials = signingCredentials,
    //             Audience = _configuration["Token:Audience"],
    //             Issuer = _configuration["Token:Issuer"],
    //             NotBefore = DateTime.UtcNow
    //         };

    //         JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
    //         JwtSecurityToken? securityToken = tokenHandler.CreateToken(tokenDescriptor) as JwtSecurityToken;

    //         Token tokenModel = new Token
    //         {
    //             AccessToken = tokenHandler.WriteToken(securityToken),
    //             RefreshToken = CreateRefreshToken(),
    //             Expiration = expiration
    //         };

    //         return tokenModel;
    //     }

    //     throw new Exception("Invalid username");
    // }

    // private async Task<List<Claim>> GetClaimsAsync(CustomUser user)
    // {
    //     var claims = new List<Claim>
    //     {
    //         new Claim(ClaimTypes.NameIdentifier, user.Id),
    //         new Claim(ClaimTypes.Name, user.UserName),
    //     };

    //     var userRoles = await _userManager.GetRolesAsync(user);
    //     foreach (var role in userRoles)
    //     {
    //         claims.Add(new Claim(ClaimTypes.Role, role));
    //     }

    //     return claims;
    // }

    // public string CreateRefreshToken()
    // {
    //     return Guid.NewGuid().ToString();
    // }
    // }

}
