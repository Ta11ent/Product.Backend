var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(config =>
{
    config.SetKebabCaseEndpointNameFormatter();

    config.AddConsumer<OrderPaidConsumer>();

    config.UsingRabbitMq((context, cfg) =>
    {
       var mqSettings = builder.Configuration.GetSection(nameof(RabbitMqConfig)).Get<RabbitMqConfig>();

        cfg.Host(new Uri(mqSettings.Host), c =>
        {
            c.Username(mqSettings.Username);
            c.Password(mqSettings.Password);
        });
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddEmailSender(config =>
{
    var emailSettings = builder.Configuration.GetSection(nameof(EmailConfig)).Get<EmailConfig>();

    config.From = emailSettings.From;
    config.SmtpServer = emailSettings.SmtpServer;
    config.Port = emailSettings.Port;
    config.UserName = emailSettings.Username;
    config.Password = emailSettings.Password;
})
.AddEmailBuilder();

var app = builder.Build();

app.Run();
