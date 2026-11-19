using todoapp.Domain.Entities;
using todoapp.Domain.UseCases.Interfaces;

namespace todoapp.Domain.UseCases;

public class UpdateTaskUseCase(ITaskRepository taskRepository): IUpdateTaskUseCase
{
    public async Task<bool> ExecuteAsync(TaskEntity task)
    {
        task.Title = task.Title.Trim();
        task.Description = task.Description.Trim();
        
        if(string.IsNullOrWhiteSpace(task.Title))
            throw new ArgumentException("Task title is required.");
        
        return await taskRepository.UpdateTaskAsync(task);
    }
}