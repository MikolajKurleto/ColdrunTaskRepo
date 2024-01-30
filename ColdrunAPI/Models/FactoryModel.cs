using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColdrunAPI.Models
{
    public class FactoryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string FactoryName { get; set; } = default!;
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        public EmployeeModel Employee { get; set; }
    }
}
