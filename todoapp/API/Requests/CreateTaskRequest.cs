using System.ComponentModel.DataAnnotations;

namespace todoapp.API.Requests;

public class CreateTaskRequest
{
    [Required]
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}