using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class OperationsManagmentDbContext : DbContext, IOperationsManagmentDbContext
    {
        public OperationsManagmentDbContext(DbContextOptions<OperationsManagmentDbContext> options): base(options)
        { }
        public DbSet<Machine> machines { get; set; }
        public DbSet<ProductionStage> productionstages {get; set;}
        public DbSet<MachineOrder> machineorders {get; set;}
        


              

        public override int SaveChanges()

        {
            return base.SaveChanges();
        }

                
    }
    
     
}
