using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Machine
    {
    
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CompanyName { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public ProductionStage? ProductionStage { get; set; }
        public int? ProductionStageId { get; set; }
        public MachineOrder? MachineOrder { get; set; }
        public int MachineOrderId { get; set; }
        public bool IsDone { get; set; } = false;
    }
}