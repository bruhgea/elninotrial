using todoapp.Domain.Entities;
using todoapp.Domain.UseCases.Interfaces;

namespace todoapp.Domain.UseCases;

public class GetActiveTasksUseCase(ITaskRepository taskRepository) : IGetActiveTasksUseCase
{
    public async Task<IEnumerable<TaskEntity>> ExecuteAsync()
    {
        return await taskRepository.GetAllActiveAsync();
    }
}