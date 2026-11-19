using todoapp.Domain.Entities;
using todoapp.Domain.UseCases.Interfaces;

namespace todoapp.Domain.UseCases;

public class GetTaskByIdUseCase(ITaskRepository taskRepository) : IGetTaskByIdUseCase
{
    public async Task<TaskEntity?> ExecuteAsync(long id)
    {
        return await taskRepository.GetByIdAsync(id);
    }
}