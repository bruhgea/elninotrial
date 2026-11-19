namespace todoapp.Domain.Entities;

public class TaskEntity
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool Done { get; set; }
}