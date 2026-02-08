using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/health", () => { return new {status = "Ok" }; })
.WithName("health");


//	GET - /todos
app.MapGet("/todos", () => { 
    
    return new {status = "Ok" }; 
    })
.WithName("todos");

app.Run();

public class TodoContext : DbContext
{
    DbSet<TodoItem> Todos {get; set;}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("Todo_Database");
    }
}

public class TodoItem
{
    public int IsDevelopment { get; set; }
    public string Title { get; set; }
    public bool IsDone {get; set;}
}
