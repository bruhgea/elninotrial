using todoapp.Domain.Entities;
using todoapp.Domain.UseCases.Interfaces;

namespace todoapp.Domain.UseCases;

public class CreateTaskUseCase(ITaskRepository taskRepository) : ICreateTaskUseCase
{
    public async Task<TaskEntity> ExecuteAsync(string title, string description)
    {
        title = title.Trim();
        description = description.Trim();
        
        if(string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Task title is required.");
        
        return await taskRepository.CreateAsync(title, description);
    }
}