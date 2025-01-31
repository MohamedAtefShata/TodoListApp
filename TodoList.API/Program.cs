using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoList.API.Middleware;
using TodoList.Core.Services.Auth;
using TodoList.Core.Services.Auth.Helpers.JwtTokenAuth;
using TodoList.Core.Services.Auth.Helpers.PasswordHasing;
using TodoList.Infrastructure.Persistence;
using TodoList.Infrastructure.Repositories.TodoItemRepository;
using TodoList.Infrastructure.Repositories.UserRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Add repositories from Infrastructure layer
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();

// Add Utilities ( JwtToken gen ,password Hasher)
builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

// Add Auth Service 
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add global exception handling
app.UseMiddleware<ExceptionHandlingMiddleware>();


app.UseHttpsRedirection();

app.Run();