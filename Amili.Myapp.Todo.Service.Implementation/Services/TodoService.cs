using AutoMapper;
using Amili.Myapp.Todo.Service.Core.DataModels;
using Amili.Myapp.Todo.Service.Core.Models.Request;
using Amili.Myapp.Todo.Service.Core.Models.Response;
using Amili.Myapp.Todo.Service.Core.Services;
using Amili.Myapp.Todo.Service.Implementation.Data;
using Microsoft.EntityFrameworkCore;

namespace Amili.Myapp.Todo.Service.Implementation.Services;

public class TodoService(TodoDbContext dbcontext, IMapper mapper) : ITodoService
{
    public async Task<TodoItemResponse> CreateTodoItemAsync(CreateTodoItemRequest request)
    {
        var todoItem = mapper.Map<TodoItem>(request);
        todoItem.CreatedAt = DateTime.UtcNow;

        dbcontext.TodoItems.Add(todoItem);
        await dbcontext.SaveChangesAsync();

        return mapper.Map<TodoItemResponse>(todoItem);
    }

    public async Task<TodoItemResponse?> GetTodoItemByIdAsync(long id)
    {
        var todoItem = await dbcontext.TodoItems.FindAsync(id);
        if (todoItem == null)
        {
            return null;
        }
        return mapper.Map<TodoItemResponse>(todoItem);
    }

    public async Task<TodoItemResponse[]> GetAllTodoItemsAsync()
    {
        var todoItems = await dbcontext.TodoItems
            .OrderBy(t => t.CreatedAt)
            .ToListAsync();
        return mapper.Map<TodoItemResponse[]>(todoItems);
    }

    public async Task<TodoItemResponse?> UpdateTodoItemAsync(long id, UpdateTodoItemRequest request)
    {
        var todoItem = await dbcontext.TodoItems.FindAsync(id);
        if (todoItem == null)
        {
            return null;
        }

        if (request.Name != null)
        {
            todoItem.Name = request.Name;
        }
        if (request.Description != null)
        {
            todoItem.Description = request.Description;
        }
        if (request.IsCompleted == true)
        {
            todoItem.IsCompleted = request.IsCompleted.Value;
            todoItem.CompletedAt = DateTime.UtcNow;
        }
        if (request.IsCompleted == false)
        {
            todoItem.IsCompleted = request.IsCompleted.Value;
            todoItem.CompletedAt = null;
        }

        await dbcontext.SaveChangesAsync();

        return mapper.Map<TodoItemResponse>(todoItem);
    }

    public async Task<string?> DeleteTodoItemAsync(long id)
    {
        var todoItem = await dbcontext.TodoItems.FindAsync(id);
        if (todoItem == null)
        {
            return null;
        }

        dbcontext.TodoItems.Remove(todoItem);
        await dbcontext.SaveChangesAsync();

        return $"Todo item with ID {id} has been deleted.";
    }
}