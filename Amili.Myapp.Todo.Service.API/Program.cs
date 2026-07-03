using Microsoft.EntityFrameworkCore;
using Amili.Myapp.Todo.Service.Core.Services;
using Amili.Myapp.Todo.Service.Implementation.Data;
using Amili.Myapp.Todo.Service.Implementation.Services;
using Amili.Myapp.Todo.Service.Implementation.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddControllers();

// AutoMapper: scans MapperProfile for CreateMap<> rules and registers IMapper for DI.
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MapperProfile>());

// EF Core: register TodoDbContext backed by PostgreSQL using the connection string.
var connectionString = builder.Configuration.GetConnectionString("TodoDb");
builder.Services.AddDbContext<TodoDbContext>(options => options.UseNpgsql(connectionString));

// Constructor DI: whenever a controller asks for ITodoService, provide TodoService.
builder.Services.AddScoped<ITodoService, TodoService>();

var app = builder.Build();

// Apply any pending EF Core migrations automatically on startup.
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
