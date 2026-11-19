using NSubstitute;
using todoapp.Domain;
using todoapp.Domain.Entities;
using todoapp.Domain.UseCases;
using todoapp.Domain.UseCases.Interfaces;

namespace testToDoApp.Domain.UseCases;

public class CreateTaskUseCaseTests
{
    private readonly ITaskRepository _taskRepository;
    private readonly ICreateTaskUseCase _createTaskUseCase;

    public CreateTaskUseCaseTests()
    {
        _taskRepository = Substitute.For<ITaskRepository>();
        _createTaskUseCase = new CreateTaskUseCase(_taskRepository);
    }
    
    [Fact]
    public async Task CreateTask_TrimsInputAndCreatesTask()
    {
        // Arrange
        var task = new TaskEntity
        {
            Id = 1,
            Title = "Task 1",
            Description = "Some details",
            Done = false
        };

        _taskRepository.CreateAsync("Task 1", "Some details").Returns(task);

        // Act
        var result = await _createTaskUseCase.ExecuteAsync("  Task 1  ", "  Some details  ");

        // Assert
        Assert.Equal(task, result);

        await _taskRepository.Received(1).CreateAsync("Task 1", "Some details");
    }

    [Fact]
    public async Task CreateTask_ThrowsException_WhenNameIsEmpty()
    {
        // Arrange
        
        // Act
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => _createTaskUseCase.ExecuteAsync("   ", "Some details"));

        // Assert
        Assert.Equal("Task title is required.", exception.Message);
        await _taskRepository.DidNotReceive().CreateAsync(Arg.Any<string>(), Arg.Any<string>());
    }

    [Fact]
    public async Task CreateTask_AllowsEmptyDetails()
    {
        // Arrange
        var task = new TaskEntity
        {
            Id = 1,
            Title = "Task 1",
            Description = "",
            Done = false
        };

        _taskRepository.CreateAsync("Task 1", "").Returns(task);

        // Act
        var result = await _createTaskUseCase.ExecuteAsync("Task 1", "   ");

        // Assert
        Assert.Equal(task, result);
        await _taskRepository.Received(1).CreateAsync("Task 1", "");
    }
    
}