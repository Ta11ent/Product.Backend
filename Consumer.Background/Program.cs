using MessageService.Models.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RabbitMqConfig>(builder.Configuration.GetSection(nameof(RabbitMqConfig)));
builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection(nameof(EmailConfig)));

builder.Services.AddSenderService();
builder.Services.AddHostedService<RabbitMqConsumer>();

var app = builder.Build();

app.Run();
