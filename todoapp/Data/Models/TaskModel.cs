namespace todoapp.Data;

public class TaskModel
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool Done { get; set; }
}