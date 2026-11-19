using todoapp.Domain.UseCases.Interfaces;

namespace todoapp.Domain.UseCases;

public class DeleteTaskUseCase(ITaskRepository taskRepository) : IDeleteTaskUseCase
{
    public async Task<bool> ExecuteAsync(long id)
    {
        return await taskRepository.DeleteAsync(id);
    }
}