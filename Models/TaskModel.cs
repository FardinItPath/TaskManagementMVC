using System.ComponentModel.DataAnnotations;

namespace TaskManagementMVC.Models
{
    public class TaskModel
    {
        public int TaskId { get; set; }

        [Required]
        public int UserId { get; set; }

        
        [StringLength(255)]
        [Required(ErrorMessage = "Title is required.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Please select a status.")]
        public string StatusId { get; set; }

        [Required(ErrorMessage = "Due Date is required.")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "Please select a priority.")]
        public string PriorityId { get; set; }
        public bool IsActive { get; set; } = true;

        [Required]
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }

        [Required]
        public DateTime CreatedDT { get; set; } = DateTime.Now;
        public DateTime UpdateDT { get; set; }

    }
}
