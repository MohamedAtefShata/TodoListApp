using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TodoList.API.Middleware;
using TodoList.Core.Services.Auth;
using TodoList.Core.Services.Auth.Helpers.JwtTokenAuth;
using TodoList.Core.Services.Auth.Helpers.PasswordHasing;
using TodoList.Core.Services.TodoItem;
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

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Adjust this to match your Angular app URL
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Add repositories from Infrastructure layer
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();

// Add Utilities (JwtToken gen, password Hasher)
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
// Add Auth Service 
builder.Services.AddScoped<IAuthService, AuthService>();

// Add TodoService
builder.Services.AddScoped<ITodoService, TodoService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<ExceptionHandlingMiddlewareFactory>();
builder.Services.AddTransient<ExceptionHandlingMiddleware>(sp =>
    sp.GetRequiredService<ExceptionHandlingMiddlewareFactory>().Create(sp.GetRequiredService<RequestDelegate>()));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use CORS
app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();

app.Run();