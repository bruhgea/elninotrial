using todoapp.Domain;
using todoapp.Domain.Entities;

namespace todoapp.Data;

public class TaskRepository : ITaskRepository
{
    public Task<IEnumerable<TaskEntity>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TaskEntity> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(string title, string description)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateTaskAsync(TaskEntity task)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateTaskCompletionAsync(long id, bool isDone)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }
}