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

            RouteGroupBuilder groupBuilder = 
                app.MapGroup("api/v{version:apiVersion}/")
                .WithApiVersionSet(versionSet)
                .MapToApiVersion(1.0)
                .WithOpenApi();

            groupBuilder.MapGet("products/{Id}",
                async (Guid Id, ISender sender) =>
                {
                    return await sender.Send(new GetProductDetailsQuery() { ProductId = Id }) is var response
                        ? Results.Ok(response)
                        : Results.NotFound();
                })
                .WithName("GetProductById")
                .WithSummary("Get the Product by Id")
                .WithDescription("JSON object containing Product information");


            groupBuilder.MapGet("products",
                async ([AsParameters] GetProductListDto entity, IMapper mapper, ISender sender) =>
                {
                    var query = mapper.Map<GetProductListQuery>(entity);
                    return await sender.Send(query);
                })
                .WithSummary("Get the list of Product")
                .WithDescription("JSON object containing Product information");

            groupBuilder.MapPost("products",
                async (CreateProductDto entity, IMapper mapper, ISender sender) =>
                {
                    var productCommand = mapper.Map<CreateProductCommand>(entity);
                    var id = await sender.Send(productCommand);
                    var costCommand = new CreateCostCommand() { Price = entity.Price, ProductId = id };
                    await sender.Send(costCommand);
                    return Results.CreatedAtRoute("GetProductById", new { id });
                })
                .AddEndpointFilter<ValidationFilter<CreateProductDto>>()
                .WithSummary("Create a Category")
                .WithDescription("Create a Category object");

            groupBuilder.MapPut("products/{Id}",
                async (Guid Id, UpdateProductDto entity, IMapper mapper, ISender sender) =>
                {
                    entity.ProductId = Id;
                    var command = mapper.Map<UpdateProductCommand>(entity);
                    await sender.Send(command);
                    return Results.NoContent();
                })
                .AddEndpointFilter<ValidationFilter<UpdateProductDto>>()
                .WithSummary("Update the Product")
                .WithDescription("Update the Product object");

            groupBuilder.MapDelete("products/{Id}",
                async (Guid Id, ISender sender) =>
                {
                    await sender.Send(new DeleteProductCommand { ProductId = Id });
                    return Results.NoContent();
                })
                .WithSummary("Delete the Product")
                .WithDescription("Delete the Product object");
        }
    }
}
