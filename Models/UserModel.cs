using System.ComponentModel.DataAnnotations;

namespace TaskManagementMVC.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [MinLength(4, ErrorMessage = "Username must be at least  characters long")]
        [StringLength(100)]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(4, ErrorMessage = "Password must be at least 4 characters long")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage ="Please select a gender")]
        public int GenderId { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        public int CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        [Required]
        public DateTime CreatedDT { get; set; } = DateTime.Now;

        public DateTime? UpdateDT { get; set; }
    }
}
