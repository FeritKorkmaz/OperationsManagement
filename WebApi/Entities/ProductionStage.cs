using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class ProductionStage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
      
    }
    
}