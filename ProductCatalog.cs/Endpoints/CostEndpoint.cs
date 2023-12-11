using ProductCatalog.API.Models.Cost;
using ProductCatalog.Application.Application.Commands.Cost.CreateCost;

namespace ProductCatalog.API.Endpoints
{
    public static class CostEndpoint
    {
        public static void Map(WebApplication app)
        {
            var versionSet = app.NewApiVersionSet()
                .HasApiVersion(1.0)
                .Build();

            app.MapPost("api/v{version:apiVersion}/price",
                async (HttpContext context, CreateCostDto entity, IMapper mapper, ISender sender) =>
                {
                    var apiVersion = context.GetRequestedApiVersion();
                    var command = mapper.Map<CreateCostCommand>(entity);
                    var id = await sender.Send(command);
                    return Results.Ok();
                })
                .AddEndpointFilter<ValidationFilter<CreateCostDto>>()
                .WithApiVersionSet(versionSet)
                .MapToApiVersion(1.0)
                .WithSummary("Create a Price")
                .WithDescription("Create a Price object")
                .WithOpenApi();

        }
    }
}
