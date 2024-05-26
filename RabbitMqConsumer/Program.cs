using RabbitMqConsumer.Connection;
using RabbitMqConsumer.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Configuration>(builder.Configuration.GetSection(nameof(Configuration)));
builder.Services.AddSingleton<IMqConnection, MqConnection>();

var app = builder.Build();

app.Run();
