using System.ComponentModel.DataAnnotations;

namespace TaskManagementMVC.Models
{
    public class PriorityModel
    {
        public int PriorityId { get; set; }

        [Required(ErrorMessage = "Please select a priority.")]
        [StringLength(50)]
        public string? PriorityName { get; set; } 
    }
}
