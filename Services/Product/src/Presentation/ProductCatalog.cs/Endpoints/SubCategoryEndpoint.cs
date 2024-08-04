using ProductCatalog.API.Models.SubCategory;
using ProductCatalog.Application.Application.Commands.SubCategory.CreateSubCategory;
using ProductCatalog.Application.Application.Commands.SubCategory.DeleteSubCategory;
using ProductCatalog.Application.Application.Commands.SubCategory.UpdateSubCategory;
using ProductCatalog.Application.Application.Queries.SubCategory.GetSubCategoryDetails;
using ProductCatalog.Application.Application.Queries.SubCategory.GetSubCategoryList;

namespace ProductCatalog.API.Endpoints
{
    public static class SubCategoryEndpoint
    {
        public static void Map(WebApplication app)
        {
            var versionSet = app.NewApiVersionSet()
                .HasApiVersion(1.0)
                .Build();

            RouteGroupBuilder groupBuilder =
                app.MapGroup("api/v{version:apiVersion}/categories/{CategoryId}/")
                .WithApiVersionSet(versionSet)
                .MapToApiVersion(1.0)
                .WithOpenApi();

            groupBuilder.MapGet("subcategories",
                async ([AsParameters] GetSubCategoryListDto parameters, IMapper mapper, ISender sender) =>
                {
                    var query = mapper.Map<GetSubCategoryListQuery>(parameters);
                    return await sender.Send(query);
                })
                .WithSummary("Get the list of SubCategory")
                .WithDescription("JSON object containing SubCategory information");

            groupBuilder.MapGet("subcategories/{Id}",
               async (Guid Id, Guid CategoryId, IMapper mapper, ISender sender) =>
               {
                   return await sender.Send(new GetSubCategoryDetailsQuery() { 
                       CategoryId = CategoryId,
                       SubCategoryId = Id 
                   }) is var response
                       ? Results.Ok(response)
                       : Results.NotFound();
               })
               .WithName("GetSubCategoryById")
               .WithSummary("Get the SubCategory by Id")
               .WithDescription("JSON object containing SubCategory information");

            groupBuilder.MapPost("subcategories",
               async (CreateSubCategoryDto data, Guid CategoryId, IMapper mapper, ISender sender) =>
               {
                   data.CategoryId = CategoryId;
                   var command = mapper.Map<CreateSubCategoryCommand>(data);
                   var id = await sender.Send(command);
                   return Results.CreatedAtRoute("GetSubCategoryById", new { id });
               })
                .AddEndpointFilter<ValidationFilter<CreateSubCategoryDto>>()
               .WithSummary("Create a SubCategory")
               .WithDescription("Create a SubCategory object");

            groupBuilder.MapPut("subcategories/{Id}",
               async (Guid Id, Guid CategoryId, UpdateSubCategoryDto data, IMapper mapper, ISender sender) =>
               {
                   data.CategoryId = CategoryId;
                   data.SubCategoryId = Id;
                   var command = mapper.Map<UpdateSubCategoryCommand>(data);
                   await sender.Send(command);

                   return Results.NoContent();
               })
               .AddEndpointFilter<ValidationFilter<UpdateSubCategoryDto>>()
               .WithSummary("Update the SubCategory")
               .WithDescription("Update the SubCategory object");

            groupBuilder.MapDelete("subcategories/{Id}",
            async (Guid Id, Guid CategoryId, ISender sender) =>
            {
                await sender.Send(new DeleteSubCategoryCommand { CategoryId = CategoryId, SubCategoryId = Id });
                return Results.NoContent();

            })
            .WithSummary("Delete the SubCategory")
            .WithDescription("Delete the SubCategory object");
        }
    }
}
