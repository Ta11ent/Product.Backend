using Asp.Versioning.Conventions;
using ShoppingCart.API.Models.ProductRange;
using ShoppingCart.Application.Application.Commands.ProductRange.CreateProductRange;
using ShoppingCart.Application.Application.Commands.ProductRange.DeleteProductRange;
using ShoppingCart.Application.Application.Commands.ProductRange.UpdateProductRange;
using ShoppingCart.Application.Application.Queries.ProductRange.GetProductRangeDetails;

namespace ShoppingCart.API.Endpoints
{
    public static class ProductRangeEndpoint
    {
        public static void Map(WebApplication app)
        {
            var versionSet = app.NewApiVersionSet()
              .HasApiVersion(1.0)
              .Build();

                app.MapGet("api/v{version:apiVersion}/productRange/{Id}",
                  async (HttpContext context, Guid Id, ISender sender) =>
                  {
                      var apiVersion = context.GetRequestedApiVersion();
                      return await sender.Send(new GerProductRangeQuery() { Id = Id } ) is var response
                       ? Results.Ok(response)
                       : Results.NotFound();
                  })
                  .WithName("GetProductRangeById")
                  .WithApiVersionSet(versionSet)
                  .MapToApiVersion(1.0)
                  .WithSummary("Get the Product Range by Id")
                  .WithDescription("JSON object containing Product Range information")
                  .WithOpenApi();

                app.MapPost("api/v{version:apiVersion}/productRange",
                    async(HttpContext context, CreateProductRangeDto data, ISender sender, 
                        IMapper mapper) =>
                    {
                        var apiVersion = context.GetRequestedApiVersion();
                        var command = mapper.Map<CreateProductRangeCommand>(data);
                        var id = await sender.Send(command);
                        return Results.CreatedAtRoute("GetProductRangeById", new { id }); 
                    })
                    .AddEndpointFilter<ValidationFilter<CreateProductRangeDto>>()
                    .WithApiVersionSet(versionSet)
                    .MapToApiVersion(1.0)
                    .WithSummary("Adding an item to the shopping cart")
                    .WithDescription("Create a Product range item")
                    .WithOpenApi();

                app.MapPut("api/v{version:apiVersion}/productRange/{Id}",
                   async (HttpContext context, Guid Id, UpdateProductRnageDto entity, 
                    IMapper mapper, ISender sender) =>
                   {
                       var apiVersion = context.GetRequestedApiVersion();
                       entity.ProductRangeId = Id;
                       var command = mapper.Map<UpdateProductRangeCommand>(entity);
                       await sender.Send(command);
                       return Results.NoContent();
                   })
                   .AddEndpointFilter<ValidationFilter<UpdateProductRnageDto>>()
                   .WithApiVersionSet(versionSet)
                   .MapToApiVersion(1.0)
                   .WithSummary("Update the Product Range")
                   .WithDescription("Update the Product Range object")
                   .WithOpenApi();

            app.MapDelete("api/v{version:apiVersion}/productRange/{Id}",
               async (HttpContext context, Guid Id, ISender sender) =>
               {
                   var apiVersion = context.GetRequestedApiVersion();
                   await sender.Send(new DeleteProductRangeCommand() { ProductRangeId = Id });
                   return Results.NoContent();
               })
               .WithApiVersionSet(versionSet)
               .MapToApiVersion(1.0)
               .WithSummary("Delete the Product Range")
               .WithDescription("Delete the Product Range object")
               .WithOpenApi();
        }
    }
}
