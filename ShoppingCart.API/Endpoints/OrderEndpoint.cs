using Asp.Versioning.Conventions;
using AutoMapper;
using ShoppingCart.API.Models.Order;
using ShoppingCart.API.Validation;
using ShoppingCart.Application.Common.Models.Order;


namespace ShoppingCart.API.Endpoints
{
    public static class OrderEndpoint
    {
        public static void Map(WebApplication app)
        {
            var versionSet = app.NewApiVersionSet()
               .HasApiVersion(1.0)
               .Build();

            app.MapGet("api/v{version:apiVersion}/orders/{Id}",
               async (HttpContext context, Guid Id, IOrderReppository repos) =>
               {
                   var apiVersion = context.GetRequestedApiVersion();
                   return await repos.GetOrderDetailsAsync(Id) is var response
                    ? Results.Ok(response)
                    : Results.NotFound();
               })
               .WithName("GetOrderById")
               .WithApiVersionSet(versionSet)
               .MapToApiVersion(1.0)
               .WithSummary("Get the Order by Id")
               .WithDescription("JSON object containing Order information")
               .WithOpenApi();

            app.MapGet("api/v{version:apiVersion}/orders",
                async(HttpContext context, [AsParameters]OrderListQueryDto request, IMapper mapper, IOrderReppository repos) =>
               {
                   var apiVersion = context.GetRequestedApiVersion();
                   var query = mapper.Map<OrderListQuery>(request);
                   return await repos.GetOrderListAsync(query);
                })
               .WithApiVersionSet(versionSet)
               .MapToApiVersion(1.0)
               .WithSummary("Get the list of Orders")
               .WithDescription("JSON object containing Order information")
               .WithOpenApi();

            app.MapPut("api/v{version:apiVersion}/orders/{Id}",
              async (HttpContext context, Guid Id, UpdateOrderDto entity,
               IMapper mapper, IOrderReppository repos, IRabbitMqProducerService producer) =>
              {
                  var apiVersion = context.GetRequestedApiVersion();
                  entity.OrderId = Id;
                  var command = mapper.Map<UpdateOrderCommand>(entity);
                  await repos.UpdateOrderAsync(command);
                  await repos.SaveAsync();

                  await producer.SendProducerMessage(Id);

                  return Results.NoContent();
              })
              .AddEndpointFilter<ValidationFilter<UpdateOrderDto>>()
              .WithApiVersionSet(versionSet)
              .MapToApiVersion(1.0)
              .WithSummary("Update the Order")
              .WithDescription("Update the Order object")
              .WithOpenApi();

            app.MapDelete("api/v{version:apiVersion}/orders/{Id}",
              async (HttpContext context, Guid Id, IOrderReppository repos) =>
              {
                  var apiVersion = context.GetRequestedApiVersion();
                  await repos.DeleteOrderAsync(Id);
                  await repos.SaveAsync();
                  return Results.NoContent();
              })
              .WithApiVersionSet(versionSet)
              .MapToApiVersion(1.0)
              .WithSummary("Delete the Order")
              .WithDescription("Delete the Order object")
              .WithOpenApi();
        }
    }
}
