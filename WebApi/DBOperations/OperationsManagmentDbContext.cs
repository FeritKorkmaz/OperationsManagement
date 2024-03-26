using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class OperationsManagmentDbContext :IdentityDbContext<CustomUser>,  IOperationsManagmentDbContext
    {
        public OperationsManagmentDbContext(DbContextOptions<OperationsManagmentDbContext> options): base(options)
        { }
        public DbSet<Machine> machines { get; set; }
        public DbSet<ProductionStage> productionstages {get; set;}
        public DbSet<MachineOrder> machineorders {get; set;}
        public DbSet<CustomUser> customUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            // modelBuilder.Entity<ProductionStage>().ToTable("productionstages");
            // modelBuilder.Entity<MachineOrder>().ToTable("machineorders");
            // modelBuilder.Entity<User>().ToTable("users");
        }
                
    }
    
     
}
