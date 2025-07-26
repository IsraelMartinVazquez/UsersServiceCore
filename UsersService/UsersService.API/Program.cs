using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using UsersService.API.Middlewares;
using UsersService.Application.Dtos;
using UsersService.Application.Interfaces;
using UsersService.Application.Mappings;
using UsersService.Application.Services;
using UsersService.Application.Validators;
using UsersService.Infrastructure.DataAccess;
using UsersService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add Controllers
builder.Services.AddControllers();

// ---------- Database Configuration ----------
builder.Services.AddScoped(_ =>
    new DBConnection(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("La cadena de conexión 'DefaultConnection' no está configurada.")
    )
);

// ---------- AutoMapper Configuration ----------
builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);
builder.Services.AddAutoMapper(typeof(LoginRequestProfile).Assembly);
builder.Services.AddAutoMapper(typeof(LoginResponseProfile).Assembly);

// ---------- Validators ----------
builder.Services.AddScoped<IValidator<RegisterUserRequest>, RegisterUserValidator>();
builder.Services.AddScoped<IValidator<LoginRequest>, LoginValidator>();

// ---------- Services & Repositories ----------
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<LogRepository>();
builder.Services.AddScoped<IJwtRepository, JwtRepository>();

// ---------- JWT Configuration ----------
var jwtSection = builder.Configuration.GetSection("Jwt");
builder.Services.Configure<JwtOptions>(jwtSection);

var jwtOptions = jwtSection.Get<JwtOptions>()
    ?? throw new InvalidOperationException("La configuración de JWT no está definida correctamente.");

var key = Encoding.UTF8.GetBytes(jwtOptions.Key);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtOptions.Issuer,
        ValidAudience = jwtOptions.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

// ---------- Swagger ----------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ---------- Middleware ----------
app.UseMiddleware<RequestResponseMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();