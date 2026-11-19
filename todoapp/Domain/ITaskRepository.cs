using todoapp.API.Requests;
using todoapp.Domain.Entities;

namespace todoapp.Domain;

public interface ITaskRepository
{
    Task<IEnumerable<TaskEntity>> GetAllAsync();
    Task<TaskEntity?> GetByIdAsync(long id);
    Task<TaskEntity> CreateAsync(string title, string description);
    Task<bool> UpdateTaskAsync(TaskEntity task);
    Task<bool> UpdateTaskCompletionAsync(long id, bool isDone);
    Task<bool> DeleteAsync(long id);
}