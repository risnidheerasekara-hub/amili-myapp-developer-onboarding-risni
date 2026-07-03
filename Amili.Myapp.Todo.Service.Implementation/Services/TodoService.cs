using AutoMapper;
using Amili.Myapp.Todo.Service.Core.DataModels;
using Amili.Myapp.Todo.Service.Core.Models.Request;
using Amili.Myapp.Todo.Service.Core.Models.Response;
using Amili.Myapp.Todo.Service.Core.Services;
using Amili.Myapp.Todo.Service.Implementation.Data;

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
}