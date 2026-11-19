using System.ComponentModel.DataAnnotations;

namespace todoapp.API.Requests;

public class UpdateTaskRequest
{
    [Required]
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    [Required]
    public bool Done { get; set; }
}