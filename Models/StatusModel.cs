using System.ComponentModel.DataAnnotations;

namespace TaskManagementMVC.Models
{
    public class StatusModel
    {
        public int StatusId { get; set; }

        [Required(ErrorMessage = "Please select a status.")]
        [StringLength(50)]
        public string? StatusName { get; set; }

        
    }
}
