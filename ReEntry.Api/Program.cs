using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

using var db = new TodoContext();

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

app.MapGet("/health", () => { return new {status = "Ok" }; }).WithName("health");


// GET list of all TODOs 
app.MapGet("/todos", () => 
{ 

    return db.Todos.Select( todo => new
    {
        id = todo.Id,
        title = todo.Title,
        isdone = todo.IsDone
    });
});

// Get a specific TODO item based on ID
app.MapGet("/todos/{id:int}", (int id) =>
{
    return db.Todos
    .Where(it => it.Id == id)
    .Select( todo => new
    {
        id = todo.Id,
        title = todo.Title,
        isdone = todo.IsDone
    });
});

// POST a new TODO item
app.MapPost("/todos", (string title) =>
{
    var item = db.Todos.Add(new TodoItem{ Title = title, IsDone = false});
    db.SaveChanges();
    return Results.Created($"/todos/{item.Entity.Id}", item.Entity.Id);
});

// DELETE a specific TODO item using its id
app.MapDelete("/todos/{id:int}", (int id) =>
{
    var item = db.Todos
    .Where(it => it.Id == id).FirstOrDefault();
    db.Todos.Remove(item);
    db.SaveChanges();
    return Results.NoContent();
});

app.Run();

public class TodoContext : DbContext
{
    public DbSet<TodoItem> Todos {get; set;}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("Todo_Database");
    }
}

public class TodoItem
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsDone {get; set;}
}
