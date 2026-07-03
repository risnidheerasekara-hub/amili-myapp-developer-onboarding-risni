using System.ComponentModel.DataAnnotations;

namespace Amili.Myapp.Todo.Service.Core.Models.Request;

public class CreateTodoItemRequest
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(256)]
    public string? Description { get; set; }
}
