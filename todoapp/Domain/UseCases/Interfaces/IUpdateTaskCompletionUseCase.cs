namespace todoapp.Domain.UseCases.Interfaces;

public interface IUpdateTaskCompletionUseCase
{
    Task<bool> ExecuteAsync(long id, bool isDone);
}