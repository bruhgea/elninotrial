using NSubstitute;
using todoapp.Domain;
using todoapp.Domain.Entities;
using todoapp.Domain.UseCases;
using todoapp.Domain.UseCases.Interfaces;

namespace testToDoApp.Domain.UseCases;

public class GetTaskByIdUseCaseTests
{
    private readonly ITaskRepository _taskRepository;
    private readonly IGetTaskByIdUseCase _getTaskByIdUseCase;

    private readonly TaskEntity _task = new TaskEntity()
    {
        Id = 1,
        Title = "Task1",
        Description = "eat",
        Done = false
    };

    public GetTaskByIdUseCaseTests()
    {
        _taskRepository = Substitute.For<ITaskRepository>();
        _getTaskByIdUseCase = new GetTaskByIdUseCase(_taskRepository);
    }

    [Fact]
    public async Task GetTaskById_ReturnsTask_WhenTaskExists()
    {
        // Arrange
        _taskRepository.GetByIdAsync(1).Returns(_task);
        
        // Act
        var result = await _getTaskByIdUseCase.ExecuteAsync(1);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(_task, result);
        await _taskRepository.Received(1).GetByIdAsync(1);
    }

    [Fact]
    public async Task GetTaskById_ReturnsNull_WhenTaskDoesNotExist()
    {
        // Arrange
        _taskRepository.GetByIdAsync(1).Returns((TaskEntity?)null);
        
        // Act
        var result = await _getTaskByIdUseCase.ExecuteAsync(1);
        
        // Assert 
        Assert.Null(result);
        await _taskRepository.Received(1).GetByIdAsync(1);
    }
    
}