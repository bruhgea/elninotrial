using todoapp.Domain.Entities;

namespace todoapp.Domain.UseCases.Interfaces;

public interface ICreateTaskUseCase
{
    Task<TaskEntity> ExecuteAsync(string title, string description);
}