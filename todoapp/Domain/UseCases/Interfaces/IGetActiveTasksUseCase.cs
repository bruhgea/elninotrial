using todoapp.Domain.Entities;

namespace todoapp.Domain.UseCases.Interfaces;

public interface IGetActiveTasksUseCase
{
    Task<IEnumerable<TaskEntity>> ExecuteAsync();

}