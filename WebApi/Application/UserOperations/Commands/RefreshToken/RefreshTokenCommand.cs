// using WebApi.Application.TokenOperations.Models;
// using WebApi.DBOperations;

// namespace WebApi.Application.UserOperations.Commands.RefreshToken
// {
//     public class RefreshTokenCommand
//     {
//         public string? RefreshToken { get; set; }

//         private readonly IOperationsManagmentDbContext _dbContext;
//         private readonly IConfiguration _configuration;


//         public RefreshTokenCommand(IOperationsManagmentDbContext dbContext, IConfiguration configuration)
//         {
//             _dbContext = dbContext;
//             _configuration = configuration;
//         }

       
//         public Token Handle()
//         {
//             var user = _dbContext.users.FirstOrDefault(x => x.RefreshToken == RefreshToken && x.RefreshTokenEndDate > DateTime.UtcNow);
//             if (user != null)
//             {
//                 TokenHandler tokenHandler = new TokenHandler(_configuration);
//                 Token token = tokenHandler.CreateAccessToken(user);

//                 user.RefreshToken = token.RefreshToken;
//                 user.RefreshTokenEndDate = token.Expiration.AddMinutes(5);
//                 _dbContext.SaveChanges();

//                 return token;
//             }
//             else
//             {
//                 throw new InvalidOperationException("Valid bir refresh token bulunamadi");
//             }

//         }
//     }
// }