using Consumer.Connection;
using Consumer.Consumer;
using Consumer.Models;
using Consumer.Recipient;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var config = builder.GetSection(nameof(RabbitMqConfig)).Get<RabbitMqConfig>()!;

IRabbitMqConnection opt = new RabbitMqConnection(config);
ISender sender = new SendByEmail();
IRabbitMqConsumer consumer = new RabbitMqConsumer(config, opt.Connection, sender);
consumer.HearChannel();
