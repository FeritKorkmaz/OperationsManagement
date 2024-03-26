using System.Collections;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenarator
    {
       

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new OperationsManagmentDbContext(serviceProvider.GetRequiredService<DbContextOptions<OperationsManagmentDbContext>>()))
            {
                // Look for any productionstages.
                
                if (!context.productionstages.Any())
                {
                    context.productionstages.AddRange(
                        new ProductionStage
                        {
                            Name = "Cut"
                        },
                        new ProductionStage
                        {
                            Name = "Welding"
                        },
                        new ProductionStage
                        {
                            Name = "Assembly"
                        }
                    );
                    context.SaveChanges();
                }
                if (!context.machineorders.Any())
                {
                    context.machineorders.AddRange(
                        new MachineOrder
                        {
                            CompanyName = "Company01",
                            OrderQuantity = 3
                        }
                    );
                    context.SaveChanges(); 
                }
               // Look for any machines.
                if (!context.machines.Any())
                {
                    context.machines.AddRange(
                        new Machine
                        {
                            Name = "machine01-01",
                            ProductionStageId = 1,
                            MachineOrderId = 1
                        },
                        new Machine
                        {
                            Name = "machine02-02",
                            ProductionStageId = 1,
                            MachineOrderId = 1
                        },
                        new Machine
                        {
                            Name = "machine03-03",
                            ProductionStageId = 1,
                            MachineOrderId = 1
                        }
                    );
                    context.SaveChanges(); 
                }
                
           
           

            }
        }
    }
}