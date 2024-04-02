using ProductCatalog.Application.Application.Commands.Category.CreateCategory;
using ProductCatalog.Application.Application.Commands.Category.DeleteCategory;
using ProductCatalog.Application.Application.Commands.Category.UpdateCategory;
using ProductCatalog.Application.Application.Queries.Category.GetCategoryDetails;
using ProductCatalog.Application.Application.Queries.Category.GetCategoryList;

namespace ProductCatalog.APIcs.Endpoints
{
    public static class CategoryEndpoint
    {
        public static void Map(WebApplication app)
        {
            var versionSet = app.NewApiVersionSet()
                .HasApiVersion(1.0)
                .Build();

            app.MapGet("api/v{version:apiVersion}/categories/{Id}",
                async (HttpContext context, Guid Id, ISender sender) =>
                {
                    var apiVersion = context.GetRequestedApiVersion();
                    return await sender.Send(new GetCategoryDetailsQuery { CategoryId = Id }) is var response
                        ? Results.Ok(response)
                        : Results.NotFound();
                })
                .WithName("GetCategoryById")
                .WithApiVersionSet(versionSet)
                .MapToApiVersion(1.0)
                .WithSummary("Get the Category by Id")
                .WithDescription("JSON object containing Category information")
                .WithOpenApi();

            app.MapGet("api/v{version:apiVersion}/categories", 
                async (HttpContext context, [AsParameters] GetCategoryListDto entity, IMapper mapper, ISender sender) =>
                {
                    var apiVersion = context.GetRequestedApiVersion();
                    var query = mapper.Map<GetCategoryListQuery>(entity);
                    return await sender.Send(query);
                })
                .WithApiVersionSet(versionSet)
                .MapToApiVersion(1.0)
                .WithSummary("Get the list of Category")
                .WithDescription("JSON object containing category information")
                .WithOpenApi();

            app.MapPost("api/v{version:apiVersion}/categories", 
                async (HttpContext context, CreateCategoryDto entity, IMapper mapper, ISender sender) => 
                {
                    var apiVersion = context.GetRequestedApiVersion();
                    var command = mapper.Map<CreateCategoryCommand>(entity);
                    var id = await sender.Send(command);
                    return Results.CreatedAtRoute("GetCategoryById", new { id });
                })
                .AddEndpointFilter<ValidationFilter<CreateCategoryDto>>()
                .WithApiVersionSet(versionSet)
                .MapToApiVersion(1.0)
                .WithSummary("Create a Category")
                .WithDescription("Create a Category object")
                .WithOpenApi();

            app.MapPut("api/v{version:apiVersion}/categories/{Id}",
                async (HttpContext context, Guid Id, UpdateCategoryDto entity, IMapper mapper, ISender sender) =>
                {
                    var apiVersion = context.GetRequestedApiVersion();
                    entity.CategoryId = Id;
                    var command = mapper.Map<UpdateCategoryCommand>(entity);
                    await sender.Send(command);
                    return Results.NoContent();
                })
                .AddEndpointFilter<ValidationFilter<UpdateCategoryDto>>()
                .WithApiVersionSet(versionSet)
                .MapToApiVersion(1.0)
                .WithSummary("Update the Category")
                .WithDescription("Update the Category object")
                .WithOpenApi();

            app.MapDelete("api/v{version:apiVersion}/categories/{Id}",
                async (HttpContext context, Guid Id, ISender sender) =>
                {
                    var apiVersion = context.GetRequestedApiVersion();
                    await sender.Send(new DeleteCategoryCommand { CategoryId = Id });
                    return Results.NoContent();

                })
                .WithApiVersionSet(versionSet)
                .MapToApiVersion(1.0)
                .WithSummary("Delete the Category")
                .WithDescription("Delete the Category object")
                .WithOpenApi();
        }
    }
}
