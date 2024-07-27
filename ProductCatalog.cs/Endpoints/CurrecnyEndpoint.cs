using ProductCatalog.API.Models.Currecny;
using ProductCatalog.Application.Application.Commands.Currency.CreateCurrency;
using ProductCatalog.Application.Application.Commands.Currency.DeleteCurrency;
using ProductCatalog.Application.Application.Commands.Currency.UpdateCurrency;
using ProductCatalog.Application.Application.Queries.Currency.GetCurreencyDetails;
using ProductCatalog.Application.Application.Queries.Currency.GetCurrencyList;

namespace ProductCatalog.API.Endpoints
{
    public static class CurrecnyEndpoint
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

            groupBuilder.MapGet("currencies",
                async([AsParameters] GetCurrencyListDto parameters, IMapper mapper, ISender sender) =>
                {
                    var query = mapper.Map<GetCurrencyListQuery>(parameters);
                    return await sender.Send(query);
                })
                .WithSummary("Get the list of Currecny")
                .WithDescription("JSON object containing Currecny information");

            groupBuilder.MapGet("currencies/{Id}",
                async (Guid Id, IMapper mapper, ISender sender) =>
                {
                    return await sender.Send(new GetCurrencyDetailsQuery() { CurrencyId = Id }) is var response
                        ? Results.Ok(response)
                        : Results.NotFound();
                })
                .WithName("GetCurrencyById")
                .WithSummary("Get the Currency by Id")
                .WithDescription("JSON object containing Currency information");

            groupBuilder.MapPost("currencies",
                async(CreateCurrencyDto data, IMapper mapper, ISender sender) =>
                {
                    var command = mapper.Map<CreateCurrencyCommand>(data);
                    var id = await sender.Send(command);
                    return Results.CreatedAtRoute("GetCurrencyById", new { id });
                })
                 .AddEndpointFilter<ValidationFilter<CreateCurrencyDto>>()
                .WithSummary("Create a Category")
                .WithDescription("Create a Category object");

            groupBuilder.MapPut("currencies/{Id}",
               async (Guid Id, UpdateCurrencyDto data,
                   IMapper mapper, ISender sender) =>
               {
                   data.CurrencyId = Id;
                   var command = mapper.Map<UpdateCurrecnyCommand>(data);
                   await sender.Send(command);

                   return Results.NoContent();
               })
               .AddEndpointFilter<ValidationFilter<UpdateCurrencyDto>>()
               .WithSummary("Update the Currency")
               .WithDescription("Update the Currency object");

            groupBuilder.MapDelete("currencies/{Id}",
               async (Guid Id, ISender sender) =>
               {
                   await sender.Send(new DeleteCurrencyCommand { CurrencyId = Id });
                   return Results.NoContent();

               })
               .WithSummary("Delete the Currecny")
               .WithDescription("Delete the Currecny object");
        }
    }
}
