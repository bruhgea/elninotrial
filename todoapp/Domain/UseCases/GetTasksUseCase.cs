using todoapp.Domain.Entities;
using todoapp.Domain.UseCases.Interfaces;

namespace todoapp.Domain.UseCases;

public class GetTasksUseCase(ITaskRepository taskRepository) : IGetTasksUseCase
{
    public async Task<IEnumerable<TaskEntity>> ExecuteAsync()
    {
        return await taskRepository.GetAllAsync();
    }
}