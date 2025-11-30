using HotelAPI.Managers;
using HotelAPI.Models;
using HotelAPI.Services.Implementations;
using HotelAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

#region Databases and Caché
builder.Services.AddDbContext<ArcadeHotelContext>(options =>
    options.UseSqlServer(Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? "Data Source=LAPTOP-L2VQ7PBO;Initial Catalog=ArcadeHotel;Integrated Security=True;Trust Server Certificate=True")
);
#endregion

#region Services and Managers
builder.Services.AddSingleton<MqttPublisher>();
builder.Services.AddScoped<IDrinksService, DrinksService>();
builder.Services.AddScoped<IGamesService, GamesService>();
builder.Services.AddScoped<IRoomsService, RoomsService>();
builder.Services.AddScoped<IMovementsService, MovementsService>();
builder.Services.AddSingleton<IConnection>(sp =>
{
   var factory = new ConnectionFactory()
   {
       HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost",
       UserName = Environment.GetEnvironmentVariable("RABBITMQ_USER") ?? "admin",
       Password = Environment.GetEnvironmentVariable("RABBITMQ_PASS") ?? "admin",
   };
   return factory.CreateConnection();
});
#endregion

builder.Services.AddOpenApi();

#region URL and CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://192.168.100.155:3000")
        //policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.WebHost.UseUrls("http://0.0.0.0:15000", "http://0.0.0.0:15001");
#endregion

var app = builder.Build();

app.UseRouting();
app.UseCors("AllowReactApp");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();