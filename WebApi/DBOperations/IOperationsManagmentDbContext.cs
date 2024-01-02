using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations
{
   public interface IOperationsManagmentDbContext
   {
        DbSet<Machine> machines { get; set; }
        DbSet<ProductionStage> productionstages {get; set;}
        DbSet<MachineOrder> machineorders {get; set;}

        

   


     

       int SaveChanges();
   } 
}