namespace todoapp.Domain.Entities;

public class TaskEntity
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool Done { get; set; }
}