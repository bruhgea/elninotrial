using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using todoapp.API.Controllers;
using todoapp.API.DTOs;
using todoapp.API.Requests;
using todoapp.Domain.Entities;
using todoapp.Domain.UseCases.Interfaces;

namespace testToDoApp.API;

public class TasksControllerTests
{
    private readonly IGetTasksUseCase _getTasksUseCase;
    private readonly IGetTaskByIdUseCase _getTaskByIdUseCase;
    private readonly IGetActiveTasksUseCase _getActiveTasksUseCase;
    private readonly ICreateTaskUseCase _createTaskUseCase;
    private readonly IUpdateTaskUseCase _updateTaskUseCase;
    private readonly IUpdateTaskCompletionUseCase _updateTaskCompletionUseCase;
    private readonly IDeleteTaskUseCase _deleteTaskUseCase;
    private readonly TasksController _controller;

    public TasksControllerTests()
    {
        _getTasksUseCase = Substitute.For<IGetTasksUseCase>();
        _getTaskByIdUseCase = Substitute.For<IGetTaskByIdUseCase>();
        _getActiveTasksUseCase = Substitute.For<IGetActiveTasksUseCase>();
        _createTaskUseCase = Substitute.For<ICreateTaskUseCase>();
        _updateTaskUseCase = Substitute.For<IUpdateTaskUseCase>();
        _updateTaskCompletionUseCase = Substitute.For<IUpdateTaskCompletionUseCase>();
        _deleteTaskUseCase = Substitute.For<IDeleteTaskUseCase>();
        _controller = new TasksController(_getTasksUseCase, _getTaskByIdUseCase, _getActiveTasksUseCase,
            _createTaskUseCase, _updateTaskUseCase, _updateTaskCompletionUseCase, _deleteTaskUseCase);
    }
    // Shared test data
    private readonly TaskEntity _task1 = new()
    {
        Id = 1,
        Title = "Task 1",
        Description = "Some details",
        Done = false
    };

    private readonly TaskEntity _task2 = new()
    {
        Id = 2,
        Title = "Task 2",
        Description = "Other details",
        Done = true
    };

    private readonly CreateTaskRequest _createRequest = new()
    {
        Title = "New task",
        Description = "New details"
    };

    private readonly TaskEntity _createdTask = new()
    {
        Title = "New task",
        Description = "New details",
        Done = false
    };

    private readonly UpdateTaskRequest _updateRequest = new()
    {
        Title = "Updated task",
        Description = "Updated details",
        Done = true
    };

    private readonly UpdateTaskCompletionRequest _completionRequest = new()
    {
        Done = true
    };

    [Fact]
    public async Task GetAllTasks_ReturnsOkAndListOfTasks_WhenTasksExist()
    {
        // Arrange
        var tasks = new List<TaskEntity> { _task1, _task2 };
        _getTasksUseCase.ExecuteAsync().Returns(tasks);
        
        // Act
        var result = await _controller.GetAllTasks();
        
        // Assert
       
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        
        var returnedTasks = Assert.IsAssignableFrom<IEnumerable<TaskDto>>(okResult.Value);

        var returnedTasksList = returnedTasks.ToList();

        Assert.Equal(2, returnedTasksList.Count);

        Assert.Equal(1, returnedTasksList[0].Id);
        Assert.Equal("Task 1", returnedTasksList[0].Title);
        Assert.Equal("Some details", returnedTasksList[0].Description);
        Assert.False(returnedTasksList[0].Done);

        Assert.Equal(2, returnedTasksList[1].Id);
        Assert.Equal("Task 2", returnedTasksList[1].Title);
        Assert.Equal("Other details", returnedTasksList[1].Description);
        Assert.True(returnedTasksList[1].Done);
        
        await _getTasksUseCase.Received(1).ExecuteAsync();
    }

    [Fact]
    public async Task GetAllTasks_ReturnsOkAndEmptyList_WhenNoTasksExist()
    {
        // Arrange
        _getTasksUseCase.ExecuteAsync().Returns(new List<TaskEntity>());
        
        // Act
        var result = await _controller.GetAllTasks();
        
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedTasks = Assert.IsAssignableFrom<IEnumerable<TaskDto>>(okResult.Value);
        
        Assert.Empty(returnedTasks);
        
        await _getTasksUseCase.Received(1).ExecuteAsync();
    }

    [Fact]
    public async Task CreateTask_ReturnsCreatedAtAction_WhenTaskIsCreated()
    {
        // Arrange
        _createTaskUseCase.ExecuteAsync(_createRequest.Title, _createRequest.Description).Returns(_createdTask);
        
        // Act 
        var result = await _controller.CreateTask(_createRequest);
        
        // Assert
        var okResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnedTask = Assert.IsAssignableFrom<TaskDto>(okResult.Value);
        Assert.Equal(_createdTask.Title, returnedTask.Title);
        Assert.Equal(_createdTask.Description, returnedTask.Description);
        Assert.Equal(_createdTask.Done, returnedTask.Done);
        await  _createTaskUseCase.Received(1).ExecuteAsync(_createRequest.Title, _createRequest.Description);
    }
}