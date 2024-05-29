using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RabbitMqConfig>(builder.Configuration.GetSection(nameof(RabbitMqConfig)));

builder.Services.AddHostedService<RabbitMqConsumer>();

var app = builder.Build();

app.Run();
