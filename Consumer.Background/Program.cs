var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RabbitMqConfig>(builder.Configuration.GetSection(nameof(RabbitMqConfig)));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<RabbitMqConfig>>().Value);

builder.Services.AddMassTransit(config =>
{
    config.SetKebabCaseEndpointNameFormatter();

    config.AddConsumer<OrderPaidConsumer>();

    config.UsingRabbitMq((context, cfg) =>
    {
        var mqSettings = context.GetRequiredService<RabbitMqConfig>();

        cfg.Host(new Uri(mqSettings.Host), c =>
        {
            c.Username(mqSettings.Username);
            c.Password(mqSettings.Password);
        });

        //cfg.ClearSerialization();
        //cfg.UseRawJsonSerializer();
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddSenderService();

var app = builder.Build();

app.Run();
