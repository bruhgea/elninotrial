using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todoapp.Data;

namespace todoapp.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskModel>>> GetAllTasks()
    {
        return await context.Tasks.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskModel>> GetTaskById(long id)
    {
        var taskItem = await context.Tasks.FindAsync(id);
        if (taskItem == null)
            return NotFound();
        
        return Ok(taskItem);
    }

    [HttpPost]
    public async Task<ActionResult<TaskModel>> CreateTask([FromBody]TaskModel taskItem)
    {
        context.Tasks.Add(taskItem);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTaskById), new { id = taskItem.Id }, taskItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(long id,[FromBody] TaskModel taskItem)
    {
        var task = await context.Tasks.FindAsync(id);
        if(task == null)
            return NotFound();
        task.Title = taskItem.Title;
        task.Description = taskItem.Description;
        task.Done = taskItem.Done;

        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateTaskCompletion(long id, bool isDone)
    {
        var task = await context.Tasks.FindAsync(id);
        if(task == null)
            return NotFound();
    
        task.Done = isDone;

        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(long id)
    {
        var task = await context.Tasks.FindAsync(id);
        if(task == null)
            return NotFound();
        context.Tasks.Remove(task);
        await context.SaveChangesAsync();
        return NoContent();
    }
}