using AutoMapper;

using Amili.Myapp.Todo.Service.Core.DataModels;
using Amili.Myapp.Todo.Service.Core.Models.Request; 
using Amili.Myapp.Todo.Service.Core.Models.Response;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        // CreateMap<TSource, TDestination>() 
        CreateMap<TodoItem, TodoItemResponse>();
        CreateMap<CreateTodoItemRequest, TodoItem>();
    }
}