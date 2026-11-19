using todoapp.Domain.Entities;

namespace todoapp.Domain.UseCases.Interfaces;

public interface IGetTaskByIdUseCase
{
    Task<TaskEntity?> ExecuteAsync(long id);
}