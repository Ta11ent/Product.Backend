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

            RouteGroupBuilder groupBuilder =
                app.MapGroup("api/v{version:apiVersion}/")
                .WithApiVersionSet(versionSet)
                .MapToApiVersion(1.0)
                .WithOpenApi();

            groupBuilder.MapGet("categories/{Id}",
                async (Guid Id, ISender sender) =>
                {
                    return await sender.Send(new GetCategoryDetailsQuery { CategoryId = Id }) is var response
                        ? Results.Ok(response)
                        : Results.NotFound();
                })
                .WithName("GetCategoryById")
                .WithSummary("Get the Category by Id")
                .WithDescription("JSON object containing Category information");

            groupBuilder.MapGet("categories",
                async ([AsParameters] GetCategoryListDto entity, IMapper mapper, ISender sender) =>
                {
                    var query = mapper.Map<GetCategoryListQuery>(entity);
                    return await sender.Send(query);
                })
                .WithSummary("Get the list of Category")
                .WithDescription("JSON object containing category information");

            groupBuilder.MapPost("categories",
                async (CreateCategoryDto entity, IMapper mapper, ISender sender) =>
                {
                    var command = mapper.Map<CreateCategoryCommand>(entity);
                    var id = await sender.Send(command);
                    return Results.CreatedAtRoute("GetCategoryById", new { id });
                })
                .AddEndpointFilter<ValidationFilter<CreateCategoryDto>>()
                .WithSummary("Create a Category")
                .WithDescription("Create a Category object");

            groupBuilder.MapPut("categories/{Id}",
                async (Guid Id, UpdateCategoryDto entity, IMapper mapper, ISender sender) =>
                {
                    entity.CategoryId = Id;
                    var command = mapper.Map<UpdateCategoryCommand>(entity);
                    await sender.Send(command);
                    return Results.NoContent();
                })
                .AddEndpointFilter<ValidationFilter<UpdateCategoryDto>>()
                .WithSummary("Update the Category")
                .WithDescription("Update the Category object");

            groupBuilder.MapDelete("categories/{Id}",
                async (Guid Id, ISender sender) =>
                {
                    await sender.Send(new DeleteCategoryCommand { CategoryId = Id });
                    return Results.NoContent();

                })
                .WithSummary("Delete the Category")
                .WithDescription("Delete the Category object");
        }
    }
}
