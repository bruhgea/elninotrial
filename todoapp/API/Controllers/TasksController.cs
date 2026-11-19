using Microsoft.AspNetCore.Mvc;
using todoapp.API.DTOs;
using todoapp.API.Requests;
using todoapp.Domain.Entities;
using todoapp.Domain.UseCases.Interfaces;

namespace todoapp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController(IGetTasksUseCase getTasksUseCase,
    IGetTaskByIdUseCase getTaskByIdUseCase,
    ICreateTaskUseCase createTaskUseCase,
    IUpdateTaskUseCase updateTaskUseCase,
    IUpdateTaskCompletionUseCase updateTaskCompletionUseCase,
    IDeleteTaskUseCase deleteTaskUseCase) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetAllTasks()
    {
       var tasksEntities = await getTasksUseCase.ExecuteAsync();

       var tasksDto = tasksEntities.Select(task => new TaskDto
       {
           Id = task.Id,
           Title = task.Title,
           Description = task.Description,
           Done = task.Done
       });
       return Ok(tasksDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskDto>> GetTaskById(long id)
    {
        var taskItem = await getTaskByIdUseCase.ExecuteAsync(id);
        if (taskItem == null)
            return NotFound();
        var taskDto = new TaskDto()
        {
            Id = taskItem.Id,
            Title = taskItem.Title,
            Description = taskItem.Description,
            Done = taskItem.Done
        };
        return Ok(taskDto);
    }

    [HttpPost]
    public async Task<ActionResult<TaskDto>> CreateTask([FromBody]CreateTaskRequest createTaskRequest)
    {
        var createdTask = await createTaskUseCase.ExecuteAsync(createTaskRequest.Title, createTaskRequest.Description);
        
        var taskDto = new TaskDto()
        {
            Id = createdTask.Id,
            Title = createdTask.Title,
            Description = createdTask.Description,
            Done = createdTask.Done
        };
        
        return CreatedAtAction(nameof(GetTaskById), new { id = taskDto.Id }, taskDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(long id,[FromBody] UpdateTaskRequest updateTaskRequest)
    {
        var updateTaskEntity = new TaskEntity()
        {
            Id = id,
            Title = updateTaskRequest.Title,
            Description = updateTaskRequest.Description,
            Done = updateTaskRequest.Done

        };
        var isSuccessful = await updateTaskUseCase.ExecuteAsync(updateTaskEntity);
        
        if(!isSuccessful)
            return NotFound();

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateTaskCompletion(long id, [FromBody] UpdateTaskCompletionRequest updateTaskCompletionRequest)
    {
        var isSuccessful = await updateTaskCompletionUseCase.ExecuteAsync(id, updateTaskCompletionRequest.Done);
        
        if(!isSuccessful)
            return NotFound();
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(long id)
    {
        var isSuccessful = await deleteTaskUseCase.ExecuteAsync(id);
        
        if(!isSuccessful)
            return NotFound();
        
        return NoContent();
    }
}