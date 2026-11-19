using System.ComponentModel.DataAnnotations;

namespace todoapp.API.Requests;

public class UpdateTaskCompletionRequest
{
    [Required]
    public bool Done { get; set; }
}