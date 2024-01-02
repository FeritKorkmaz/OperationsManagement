using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class MachineOrder
    {
    
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? CompanyName { get; set; }
        public int  OrderQuantity { get; set; }
        public string? MachineType { get; set; }      
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public ICollection<Machine> Machines { get; set; } = new List<Machine>();
        public bool IsDone { get; set; } = false;
    }


    
}