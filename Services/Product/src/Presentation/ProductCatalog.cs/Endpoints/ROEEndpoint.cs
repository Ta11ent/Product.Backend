using ProductCatalog.API.Models.ROE;
using ProductCatalog.Application.Application.Commands.ROE.CreateROE;
using ProductCatalog.Application.Application.Commands.ROE.DeleteROE;
using ProductCatalog.Application.Application.Commands.ROE.UpdateROE;
using ProductCatalog.Application.Application.Queries.ROE.GetROEDetails;

namespace ProductCatalog.API.Endpoints
{
    public static class ROEEndpoint
    {
        public static void Map(WebApplication app)
        {
            var versionSet = app.NewApiVersionSet()
                .HasApiVersion(1.0)
                .Build();

            RouteGroupBuilder groupBuilder =
                app.MapGroup("api/v{version:apiVersion}/currencies/{CurrencyId}")
                .WithApiVersionSet(versionSet)
                .MapToApiVersion(1.0)
                .WithOpenApi();

            groupBuilder.MapGet("roe/{Id}",
               async (Guid Id, Guid CurrencyId, IMapper mapper, ISender sender) =>
               {
                   return await sender.Send(new GetROEDetailsQuery() { CurrencyId = CurrencyId, ROEId = Id }) is var response
                       ? Results.Ok(response)
                       : Results.NotFound();
               })
               .WithName("GetROEById")
               .WithSummary("Get the ROE by Id")
               .WithDescription("JSON object containing ROE information");

            groupBuilder.MapPost("roe",
             async (CreateROEDto data, Guid CurrencyId, IMapper mapper, ISender sender) =>
             {
                 data.CurrecnyId = CurrencyId;
                 var command = mapper.Map<CreateROECommand>(data);
                 var id = await sender.Send(command);
                 return Results.CreatedAtRoute("GetROEById", new { CurrencyId, id });
             })
              .AddEndpointFilter<ValidationFilter<CreateROEDto>>()
             .WithSummary("Create a ROE")
             .WithDescription("Create a ROE object");

            groupBuilder.MapPut("roe/{Id}",
                 async (Guid Id, Guid CurrencyId, UpdateROEDto data,
                     IMapper mapper, ISender sender) =>
                 {
                     data.CurrecnyId = CurrencyId;
                     data.ROEId = Id;
                     var command = mapper.Map<UpdateROECommand>(data);
                     await sender.Send(command);

                     return Results.NoContent();
                 })
                 .AddEndpointFilter<ValidationFilter<UpdateROEDto>>()
                 .WithSummary("Update the ROE")
                 .WithDescription("Update the ROE object");

            groupBuilder.MapDelete("roe/{Id}",
                 async (Guid CurrencyId, Guid Id, ISender sender) =>
                   {
                       await sender.Send(new DeleteROECommand { CurrencyId = Id, ROEId = Id });
                       return Results.NoContent();
                   })
                   .WithSummary("Delete the ROE")
                   .WithDescription("Delete the ROE object");
        }
    }
}
