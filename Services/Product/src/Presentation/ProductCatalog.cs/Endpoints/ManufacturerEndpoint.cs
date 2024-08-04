using ProductCatalog.API.Models.Manufacturer;
using ProductCatalog.Application.Application.Commands.Manufacturer.CreateManufacturer;
using ProductCatalog.Application.Application.Commands.Manufacturer.DeleteManufacturer;
using ProductCatalog.Application.Application.Commands.Manufacturer.UpdateManufacturer;
using ProductCatalog.Application.Application.Queries.Manufacturer.GetManufacturerDetails;
using ProductCatalog.Application.Application.Queries.Manufacturer.GetManufacturerList;

namespace ProductCatalog.API.Endpoints
{
    public static class ManufacturerEndpoint
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

            groupBuilder.MapGet("manufacturers",
               async ([AsParameters] GetManufacturerListDto parameters, IMapper mapper, ISender sender) =>
               {
                   var query = mapper.Map<GetManufacturerListQuery>(parameters);
                   return await sender.Send(query);
               })
               .WithSummary("Get the list of Manufacturer")
               .WithDescription("JSON object containing Manufacturer information");

            groupBuilder.MapGet("manufacturers/{Id}",
               async (Guid Id, IMapper mapper, ISender sender) =>
               {
                   return await sender.Send(new GetManufacturerDetailsQuery() { ManufacturerId = Id }) is var response
                       ? Results.Ok(response)
                       : Results.NotFound();
               })
               .WithName("GetManufacturerById")
               .WithSummary("Get the Manufacturer by Id")
               .WithDescription("JSON object containing Manufacturer information");

            groupBuilder.MapPost("manufacturers",
             async (CreateManufacturerDto data, IMapper mapper, ISender sender) =>
             {
                 var command = mapper.Map<CreateManufacturerCommand>(data);
                 var id = await sender.Send(command);
                 return Results.CreatedAtRoute("GetManufacturerById", new { id });
             })
              .AddEndpointFilter<ValidationFilter<CreateManufacturerDto>>()
             .WithSummary("Create a Manufacturer")
             .WithDescription("Create a Manufacturer object");

            groupBuilder.MapPut("manufacturers/{Id}",
              async (Guid Id, UpdateManufacturerDto data,
                  IMapper mapper, ISender sender) =>
              {
                  data.ManufacturerId = Id;
                  var command = mapper.Map<UpdateManufacturerCommand>(data);
                  await sender.Send(command);

                  return Results.NoContent();
              })
              .AddEndpointFilter<ValidationFilter<UpdateManufacturerDto>>()
              .WithSummary("Update the Manufacturer")
              .WithDescription("Update the Manufacturer object");

            groupBuilder.MapDelete("manufacturers/{Id}",
             async (Guid Id, ISender sender) =>
             {
                 await sender.Send(new DeleteManufacturerCommand { ManufacturerId = Id });
                 return Results.NoContent();

             })
             .WithSummary("Delete the Manufacturer")
             .WithDescription("Delete the Manufacturer object");
        }
    }
}
