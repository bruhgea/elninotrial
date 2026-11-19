using todoapp.Domain.UseCases.Interfaces;

namespace todoapp.Domain.UseCases;

public class UpdateTaskCompletionUseCase(ITaskRepository taskRepository) : IUpdateTaskCompletionUseCase
{
    public async Task<bool> ExecuteAsync(long id, bool isDone)
    {
        return await taskRepository.UpdateTaskCompletionAsync(id, isDone);
    }
}