using NSubstitute;
using todoapp.Domain;
using todoapp.Domain.Entities;
using todoapp.Domain.UseCases;
using todoapp.Domain.UseCases.Interfaces;

namespace testToDoApp.Domain.UseCases;

public class UpdateTaskUseCaseTests
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUpdateTaskUseCase _updateTaskUseCase;

    public UpdateTaskUseCaseTests()
    {
        _taskRepository = Substitute.For<ITaskRepository>();
        _updateTaskUseCase = new UpdateTaskUseCase(_taskRepository);
    }

    [Fact]
    public async Task UpdateTask_TrimsInput_AndReturnsTrue_WhenTaskExists()
    {
        // Arrange
        _taskRepository.UpdateTaskAsync(Arg.Any<TaskEntity>()).Returns(true);
        
        var input = new TaskEntity
        {
            Id = 1,
            Title = "    Task 1     ",
            Description = "    Updated details     ",
            Done = false
        };
    
        // Act
        var result = await _updateTaskUseCase.ExecuteAsync(input);
        
        // Assert
        Assert.True(result);
        Assert.Equal("Task 1", input.Title);
        Assert.Equal("Updated details", input.Description);

        await _taskRepository.Received(1).UpdateTaskAsync(Arg.Is<TaskEntity>(task =>
            task.Id == 1 &&
            task.Title == "Task 1" &&
            task.Description == "Updated details" &&
            task.Done == false
        ));
    }

    [Fact]
    public async Task UpdateTask_ReturnsFalse_WhenTaskDoesNotExist()
    {
        // Arrange
        var input = new TaskEntity
        {
            Id = 1,
            Title = "Task 1",
            Description = "Updated details",
            Done = false
        };
        
        _taskRepository.UpdateTaskAsync(input).Returns(false);
        
        // Act
        var result = await _updateTaskUseCase.ExecuteAsync(input);
        
        // Assert
        Assert.False(result);
        await  _taskRepository.Received(1).UpdateTaskAsync(input);
    }
}