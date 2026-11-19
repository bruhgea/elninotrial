using Microsoft.EntityFrameworkCore;
using todoapp.Data;
using todoapp.Domain;
using todoapp.Domain.UseCases;
using todoapp.Domain.UseCases.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IGetTasksUseCase, GetTasksUseCase>();
builder.Services.AddScoped<IGetTaskByIdUseCase, GetTaskByIdUseCase>();
builder.Services.AddScoped<IGetActiveTasksUseCase, GetActiveTasksUseCase>();
builder.Services.AddScoped<ICreateTaskUseCase, CreateTaskUseCase>();
builder.Services.AddScoped<IUpdateTaskUseCase, UpdateTaskUseCase>();
builder.Services.AddScoped<IUpdateTaskCompletionUseCase, UpdateTaskCompletionUseCase>();
builder.Services.AddScoped<IDeleteTaskUseCase, DeleteTaskUseCase>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddProblemDetails();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseExceptionHandler();  
app.UseStatusCodePages();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();