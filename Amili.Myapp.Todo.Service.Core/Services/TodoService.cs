

using Amili.Myapp.Todo.Service.Core.Models.Request;
using Amili.Myapp.Todo.Service.Core.Models.Response;

namespace Amili.Myapp.Todo.Service.Core.Services;

public interface ITodoService
{
    Task<TodoItemResponse> CreateTodoItemAsync(CreateTodoItemRequest request);

    Task<TodoItemResponse?> GetTodoItemByIdAsync(long id);

    Task<TodoItemResponse[]> GetAllTodoItemsAsync();

    Task<TodoItemResponse?> UpdateTodoItemAsync(long id, UpdateTodoItemRequest request);
    Task<string?> DeleteTodoItemAsync(long id);
}