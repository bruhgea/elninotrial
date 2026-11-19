using todoapp.Domain.Entities;

namespace todoapp.Domain.UseCases.Interfaces;

public interface IUpdateTaskUseCase
{
    Task<bool> ExecuteAsync(TaskEntity task);
}