using todoapp.Domain.Entities;

namespace todoapp.Domain.UseCases.Interfaces;

public interface IDeleteTaskUseCase
{
    Task<bool> ExecuteAsync(long id);
}