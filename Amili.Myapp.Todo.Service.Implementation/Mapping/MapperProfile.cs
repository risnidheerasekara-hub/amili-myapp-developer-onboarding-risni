using AutoMapper;

using Amili.Myapp.Todo.Service.Core.DataModels;
using Amili.Myapp.Todo.Service.Core.Models.Request; 
using Amili.Myapp.Todo.Service.Core.Models.Response;


namespace Amili.Myapp.Todo.Service.Implementation.Mapping;
public class MapperProfile : Profile
{
    public MapperProfile()
    {
        // CreateMap<TSource, TDestination>() 
        CreateMap<TodoItem, TodoItemResponse>();
        CreateMap<CreateTodoItemRequest, TodoItem>();
    }
}