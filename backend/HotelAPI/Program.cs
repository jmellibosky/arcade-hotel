using HotelAPI.Managers;
using HotelAPI.Models;
using HotelAPI.Services.Implementations;
using HotelAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

#region Databases and Cache
builder.Services.AddDbContext<ArcadeHotelContext>(options =>
    options.UseSqlServer(Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? "Data Source=192.168.100.155;Initial Catalog=ArcadeHotel;User ID=hotel;Password=SNEPfluffy85!;Trust Server Certificate=True")
);

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(Environment.GetEnvironmentVariable("REDIS_STRING") ?? "localhost:6379"));

// Configurar Redis como almacenamiento de sesiones
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = Environment.GetEnvironmentVariable("REDIS_STRING") ?? "localhost:6379";
});

// Agrega configuraciones de la sesión al contenedor de servicios
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(7);
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
});
#endregion

#region Services and Managers
builder.Services.AddSingleton<MqttPublisher>();
builder.Services.AddScoped<IDrinksService, DrinksService>();
builder.Services.AddScoped<IGamesService, GamesService>();
builder.Services.AddScoped<IRoomsService, RoomsService>();
builder.Services.AddScoped<IMovementsService, MovementsService>();
#endregion

builder.Services.AddOpenApi();

#region URL and CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://0.0.0.0:3000")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.WebHost.UseUrls("http://0.0.0.0:15000", "http://0.0.0.0:15001");
#endregion

var app = builder.Build();

app.UseSession();
app.UseCors("AllowAll"); // A ESTO HAY QUE CAMBIARLO LUEGO
// PELIGRO
// ESTO PERMITE RECIBIR SOLICITUDES DESDE CUALQUIER ORIGEN

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();