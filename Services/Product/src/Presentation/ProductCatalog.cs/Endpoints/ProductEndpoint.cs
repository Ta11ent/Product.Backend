using ProductCatalog.API.Models.Product;
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
                app.MapGroup("api/v{version:apiVersion}/Categories/{categoryId}/SubCategories/{SubCategoryId}/")
                .WithApiVersionSet(versionSet)
                .MapToApiVersion(1.0)
                .WithOpenApi();

            RouteGroupBuilder productBuilder = 
                app.MapGroup("api/v{version:apiVersion}/")
                .WithApiVersionSet(versionSet)
                .MapToApiVersion(1.0)
                .WithOpenApi();

            groupBuilder.MapGet("products/{Id}",
                async ([AsParameters] GetProductDto productDto, IMapper mapper, ISender sender) =>
                {
                    var query = mapper.Map<GetProductDetailsQuery>(productDto);
                    return await sender.Send(query) is var response
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

            productBuilder.MapGet("products",
               async ([AsParameters] GetProductListDto entity, IMapper mapper, ISender sender) =>
               {
                   var query = mapper.Map<GetProductListQuery>(entity);
                   return await sender.Send(query);
               })
               .WithSummary("Get the list of Product")
               .WithDescription("JSON object containing Product information");

            groupBuilder.MapPost("products",
                async ([AsParameters]ProductPath path, CreateProductDto entity, IMapper mapper, ISender sender) =>
                {
                    entity.CategoryId = path.CategoryId;
                    entity.SubCategoryId = path.SubCategoryId;
                    var productCommand = mapper.Map<CreateProductCommand>(entity);
                    var id = await sender.Send(productCommand);
                    return Results.CreatedAtRoute("GetProductById", new { entity.CategoryId, entity.SubCategoryId, id });
                })
                .AddEndpointFilter<ValidationFilter<CreateProductDto>>()
                .WithSummary("Create a Category")
                .WithDescription("Create a Category object");

            groupBuilder.MapPut("products/{Id}",
                async ([AsParameters] ProductPath path, Guid Id, UpdateProductDto entity,
                    IMapper mapper, ISender sender) =>
                {
                    entity.CategoryId = path.CategoryId;
                    entity.SubCategoryId = path.SubCategoryId;
                    entity.ProductId = Id;
                    var command = mapper.Map<UpdateProductCommand>(entity);
                    await sender.Send(command);

                    return Results.NoContent();
                })
                .AddEndpointFilter<ValidationFilter<UpdateProductDto>>()
                .WithSummary("Update the Product")
                .WithDescription("Update the Product object");

            groupBuilder.MapDelete("products/{Id}",
                async ([AsParameters] DeleteProductDto product, IMapper mapper, ISender sender) =>
                {
                    var command = mapper.Map<DeleteProductCommand>(product);
                    await sender.Send(command);
                    return Results.NoContent();
                })
                .WithSummary("Delete the Product")
                .WithDescription("Delete the Product object");
        }
    }
}
