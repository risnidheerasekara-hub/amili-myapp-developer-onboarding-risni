using Microsoft.AspNetCore.Mvc;
using Amili.Myapp.Todo.Service.Core.Models.Request;
using Amili.Myapp.Todo.Service.Core.Models.Response;
using Amili.Myapp.Todo.Service.Core.Services;

namespace Amili.Myapp.Todo.Service.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoItemsController(ITodoService todoService) : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(TodoItemResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateTodoItem([FromBody] CreateTodoItemRequest request)
    {
        var response = await todoService.CreateTodoItemAsync(request);
        return CreatedAtAction(nameof(GetTodoItemById), new { id = response.Id }, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TodoItemResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetTodoItemById(long id)
    {
        var response = await todoService.GetTodoItemByIdAsync(id);
        if (response == null)
        {
            return NotFound();
        }
        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TodoItemResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllTodoItems()
    {
        var response = await todoService.GetAllTodoItemsAsync();

        return Ok(response);
    }
}