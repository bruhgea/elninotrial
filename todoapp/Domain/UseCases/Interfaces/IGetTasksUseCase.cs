using todoapp.Domain.Entities;

namespace todoapp.Domain.UseCases.Interfaces;

public interface IGetTasksUseCase
{
    Task<IEnumerable<TaskEntity>> ExecuteAsync();
}