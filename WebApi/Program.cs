using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApi.DBOperations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using WebApi.Entities;
using WebApi.Application.UserOperations;




var builder = WebApplication.CreateBuilder(args);


  // Add services to the container.

  builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => {
      opt.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidAudience = builder.Configuration["Token:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]?? "default-key")),
        ClockSkew = TimeSpan.Zero  
      };
  });
  builder.Services.AddHttpContextAccessor();
  builder.Services.AddControllers();
  // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
  builder.Services.AddEndpointsApiExplorer();
  builder.Services.AddSwaggerGen();
  builder.Services.AddDbContext<OperationsManagmentDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));
  builder.Services.AddScoped<IOperationsManagmentDbContext, OperationsManagmentDbContext>();
  builder.Services.AddIdentity<CustomUser, IdentityRole>().AddEntityFrameworkStores<OperationsManagmentDbContext>();
  builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
  builder.Services.AddAuthorization();
//   builder.Services.AddSingleton<ILoggerService, ConsoleLogger>();

  var app = builder.Build();

  // Configure the HTTP request pipeline.
  if (app.Environment.IsDevelopment())
  {
      app.UseSwagger();
      app.UseSwaggerUI();
  }
  app.UseAuthentication();

  app.UseHttpsRedirection();

  app.UseAuthorization();

  // app.UseCustomExeptionMiddleWare();

  app.MapControllers();

   
  //1. Find the service layer within our scope.
  var scope = app.Services.CreateScope();
  var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
  await RoleInitializer.InitializeRoles(roleManager);
  

  //2. Get the instance of BoardGamesDBContext in our services layer
  var services = scope.ServiceProvider;

  //3. Call the DataGenerator to create sample data
  DataGenarator.Initialize(services);

  //Continue to run the application

  app.Run();
