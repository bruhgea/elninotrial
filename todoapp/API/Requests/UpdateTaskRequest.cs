using System.ComponentModel.DataAnnotations;

namespace todoapp.API.Requests;

public class UpdateTaskRequest
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    public bool Done { get; set; }
}