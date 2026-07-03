

using Amili.Myapp.Todo.Service.Core.Models.Request;
using Amili.Myapp.Todo.Service.Core.Models.Response;

namespace Amili.Myapp.Todo.Service.Core.Services;

public interface ITodoService
{
    Task<TodoItemResponse> CreateTodoItemAsync(CreateTodoItemRequest request);

    Task<TodoItemResponse?> GetTodoItemByIdAsync(long id);
}