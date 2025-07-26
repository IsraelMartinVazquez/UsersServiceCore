using FluentValidation;
using UsersService.API.Middlewares;
using UsersService.Application.Dtos;
using UsersService.Application.Interfaces;
using UsersService.Application.Mappings;
using UsersService.Application.Services;
using UsersService.Application.Validators;
using UsersService.Infrastructure.DataAccess;
using UsersService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

//DB configuration
builder.Services.AddScoped(_ =>
    new DBConnection(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("La cadena de conexión 'DefaultConnection' no está configurada.")
    )
);

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);
builder.Services.AddAutoMapper(typeof(LoginRequestProfile).Assembly);
builder.Services.AddAutoMapper(typeof(LoginResponseProfile).Assembly);

// Add Validator
builder.Services.AddScoped<IValidator<RegisterUserRequest>, RegisterUserValidator>();
builder.Services.AddScoped<IValidator<LoginRequest>, LoginValidator>();

// Register services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<LogRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMiddleware<RequestResponseMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

