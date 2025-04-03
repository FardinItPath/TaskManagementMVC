using System.ComponentModel.DataAnnotations;

namespace TaskManagementMVC.Models
{
    public class GenderModel
    {
        public int GenderId { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [StringLength(50)]
        public string? GenderName { get; set; }
    }
}
