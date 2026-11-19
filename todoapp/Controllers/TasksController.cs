using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todoapp.Data;

namespace todoapp.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController(AppDbContext context) : ControllerBase
{
    //private readonly AppDbContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskModel>>> GetAllTasks()
    {
        return await context.Tasks.ToListAsync();
    }
}