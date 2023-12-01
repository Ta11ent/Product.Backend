using Asp.Versioning.Conventions;
using AutoMapper;
using ShoppingCart.API.Models.ProductRange;
using ShoppingCart.API.Validation;
using ShoppingCart.Application.Common.Models.ProductRange;

namespace ShoppingCart.API.Endpoints
{
    public static class ProductRangeEndpoint
    {
        public static void Map(WebApplication app)
        {
            var versionSet = app.NewApiVersionSet()
              .HasApiVersion(1.0)
              .Build();

            app.MapPost("api/v{version:apiVersion}/productRange",
                async(HttpContext context, CreateProductRangeDto data, IProductRangeRepository repos, IMapper mapper) =>
                {
                    var apiVersion = context.GetRequestedApiVersion();
                    var command = mapper.Map<CreateProductRangeCommand>(data);
                    var id = await repos.CreateProductRangeAsync(command);
                    return Results.CreatedAtRoute("GetOrderById", new { id });
                })
                .AddEndpointFilter<ValidationFilter<CreateProductRangeDto>>()
                .WithApiVersionSet(versionSet)
                .MapToApiVersion(1.0)
                .WithSummary("Adding an item to the shopping cart")
                .WithDescription("Create a Product range item")
                .WithOpenApi();

        }
    }
}
