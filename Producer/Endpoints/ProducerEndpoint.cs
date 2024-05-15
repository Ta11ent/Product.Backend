using Asp.Versioning.Conventions;

namespace Producer.API.Endpoints
{
    public static class ProducerEndpoint
    {
        public static void Map(WebApplication app)
        {
            var versionSet = app.NewApiVersionSet()
               .HasApiVersion(1.0)
               .Build();

            app.MapPost("api/v{version:apiVersion}/SendMessage",
                (object message, IRabbitMqMessageProducer producer) =>
                {
                    producer.SendMessage(message);
                })
                .WithApiVersionSet(versionSet)
                .MapToApiVersion(1.0)
                .WithOpenApi();
        }
    }
}
