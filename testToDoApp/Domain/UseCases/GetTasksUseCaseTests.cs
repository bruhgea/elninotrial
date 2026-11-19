using NSubstitute;
using todoapp.Domain;
using todoapp.Domain.Entities;
using todoapp.Domain.UseCases;
using todoapp.Domain.UseCases.Interfaces;
using Xunit.Abstractions;

namespace testToDoApp.Domain.UseCases;

public class GetTasksUseCaseTests
{
    private readonly ITaskRepository _taskRepository;
    private readonly IGetTasksUseCase _getTasksUseCase;

    private readonly List<TaskEntity> _tasksList = new List<TaskEntity>  
    {            
        new TaskEntity  
        {  
            Id = 1,  
            Title = "Task1",  
            Description = "eat",  
            Done = false  
        },  
        new TaskEntity  
        {  
            Id = 2,  
            Title = "Task2",  
            Description = "sleep",  
            Done = true  
        }  
    };
    
    public GetTasksUseCaseTests()
    {
        _taskRepository = Substitute.For<ITaskRepository>();
        _getTasksUseCase= new GetTasksUseCase(_taskRepository);
    }
    
    [Fact]
    public async Task GetAllTasks_Returns_ListOfTasks_WhenTasksExist()
    {
        // Arrange
        _taskRepository.GetAllAsync().Returns(_tasksList);
        
        // Act
        var result = await _getTasksUseCase.ExecuteAsync();
        
        // Assert
        Assert.Equal(_tasksList, result);
        await _taskRepository.Received(1).GetAllAsync();
    }
    
    [Fact]
    public async Task GetAllTasks_Returns_EmptyList_WhenNoTasksExist()
    {
        // Arrange
        _taskRepository.GetAllAsync().Returns(new List<TaskEntity>());
        
        // Act
        var result = await _getTasksUseCase.ExecuteAsync();
        
        // Assert
        Assert.Empty(result);
        await _taskRepository.Received(1).GetAllAsync();
    }
}