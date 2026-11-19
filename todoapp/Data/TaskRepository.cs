using Microsoft.EntityFrameworkCore;
using todoapp.Data.Models;
using todoapp.Domain;
using todoapp.Domain.Entities;

namespace todoapp.Data;

public class TaskRepository(AppDbContext context) : ITaskRepository
{
    public async Task<IEnumerable<TaskEntity>> GetAllAsync()
    {
        var tasksModel = await context.Tasks.ToListAsync();

        var tasksEntities = tasksModel.Select(task => new TaskEntity
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Done = task.Done
            }
        );
        return tasksEntities;
    }

    public async Task<TaskEntity?> GetByIdAsync(long id)
    {
        var taskItem = await context.Tasks.FindAsync(id);
        if (taskItem == null)
            return  null;

        var taskEntity = new TaskEntity
        {
            Id = taskItem.Id,
            Title = taskItem.Title,
            Description = taskItem.Description,
            Done = taskItem.Done
        };
        return taskEntity;
    }

    public async Task<TaskEntity> CreateAsync(string title, string description)
    {
        var taskModel = new TaskModel
        {
            Title = title,
            Description = description,
            Done = false
        };
        context.Tasks.Add(taskModel);
        await context.SaveChangesAsync();

        return new TaskEntity()
        {
            Id = taskModel.Id,
            Title = taskModel.Title,
            Description = taskModel.Description,
            Done = taskModel.Done
        };
    }

    public async Task<bool> UpdateTaskAsync(TaskEntity task)
    {
        var taskModel = await context.Tasks.FindAsync(task.Id);
        if (taskModel == null)
            return false;
        
        taskModel.Title = task.Title;
        taskModel.Description = task.Description;
        taskModel.Done = task.Done;

        await context.SaveChangesAsync(); 
        return true;
    }

    public async Task<bool> UpdateTaskCompletionAsync(long id, bool isDone)
    {
        var taskModel = await context.Tasks.FindAsync(id);
        if (taskModel == null)
            return false;
        
        taskModel.Done = isDone;
        
        await context.SaveChangesAsync(); 
        return true;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var task = await context.Tasks.FindAsync(id);
        if(task == null)
            return false;
        context.Tasks.Remove(task);
        await context.SaveChangesAsync();
        return true;
    }
}