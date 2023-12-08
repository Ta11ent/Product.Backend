using ProductCatalog.API.Models.Cost;
using ProductCatalog.Application.Application.Commands.Cost.CreateCost;
using ProductCatalog.Application.Application.Commands.Product.CreateProduct;
using ProductCatalog.Application.Application.Commands.Product.DeleteProduct;
using ProductCatalog.Application.Application.Commands.Product.UpdateProduct;
using ProductCatalog.Application.Application.Queries.Product.GetProductDetails;
using ProductCatalog.Application.Application.Queries.Product.GetProductList;
using ProductCatalog.cs.Models.Product;

namespace ProductCatalog.APIcs.Endpoints
{
    public static class ProductEndpoint
    {
        public static void Map(WebApplication app)
        {
            var versionSet = app.NewApiVersionSet()
                .HasApiVersion(1.0)
                .Build();

            app.MapGet("api/v{version:apiVersion}/product/{Id}",
                async(HttpContext context, Guid Id, ISender sender) =>
                {
                    var apiVersion = context.GetRequestedApiVersion();
                    return await sender.Send(new GetProductDetailsQuery() { ProductId = Id }) is var response
                        ? Results.Ok(response)
                        : Results.NotFound();
                })
                .WithName("GetProductById")
                .WithApiVersionSet(versionSet)
                .MapToApiVersion(1.0)
                .WithSummary("Get the Product by Id")
                .WithDescription("JSON object containing Product information")
                .WithOpenApi();

            app.MapGet("api/v{version:apiVersion}/product",
                async(HttpContext context, [AsParameters] GetProductListDto entity, IMapper mapper, ISender sender) => 
                {
                    var apiVersion = context.GetRequestedApiVersion();
                    var query = mapper.Map<GetProductListQuery>(entity);
                    return await sender.Send(query);
                })
                .WithApiVersionSet(versionSet)
                .MapToApiVersion(1.0)
                .WithSummary("Get the list of Product")
                .WithDescription("JSON object containing Product information")
                .WithOpenApi();

            app.MapPost("api/v{version:apiVersion}/product", 
                async(HttpContext context, CreateProductDto entity, IMapper mapper, ISender sender) =>
                {
                    var apiVersion = context.GetRequestedApiVersion();
                    var productCommand = mapper.Map<CreateProductCommand>(entity);
                    var id = await sender.Send(productCommand);
                    var costCommand =
                        mapper.Map<CreateCostCommand>(new CreateCostDto() { Price = entity.Price, ProductId = id });
                    await sender.Send(costCommand);
                    return Results.CreatedAtRoute("GetProductById", new { id });
                })
                .WithApiVersionSet(versionSet)
                .MapToApiVersion(1.0)
                .WithSummary("Create a Category")
                .WithDescription("Create a Category object")
                .WithOpenApi();

            app.MapPut("api/v{version:apiVersion}/product/{Id}",
                async(HttpContext context, Guid Id, UpdateProductDto entity, IMapper mapper, ISender sender) =>
                {
                    var apiVersion = context.GetRequestedApiVersion();
                    entity.ProductId = Id;
                    var command = mapper.Map<UpdateProductCommand>(entity);
                    await sender.Send(command);
                    return Results.NoContent();
                })
                .AddEndpointFilter<ValidationFilter<UpdateProductDto>>()
                .WithApiVersionSet(versionSet)
                .MapToApiVersion(1.0)
                .WithSummary("Update the Product")
                .WithDescription("Update the Product object")
                .WithOpenApi();

            app.MapDelete("api/v{version:apiVersion}/product/{Id}", 
                async(HttpContext context, Guid Id, ISender sender) =>
                {
                    var apiVersion = context.GetRequestedApiVersion();
                    await sender.Send(new DeleteProductCommand { ProductId = Id });
                    return Results.NoContent();
                })
                .WithApiVersionSet(versionSet)
                .MapToApiVersion(1.0)
                .WithSummary("Delete the Product")
                .WithDescription("Delete the Product object")
                .WithOpenApi();
        }
    }
}
