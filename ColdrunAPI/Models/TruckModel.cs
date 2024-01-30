using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColdrunAPI.Models
{
    public class TruckModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; } = default!;
        [Required]
        public string Name { get; set; } = default!;
        [Required]
        public Status Status { get; set; }
        public string? Description { get; set; }
    }

    public enum Status 
    {
        [Description("Out of Service")]
        Out_Of_Service,
        [Description("Loading")]
        Loading,
        [Description("To Job")]
        To_Job,
        [Description("At Job")]
        At_Job,
        [Description("Returning")]
        Returning
    }
}
