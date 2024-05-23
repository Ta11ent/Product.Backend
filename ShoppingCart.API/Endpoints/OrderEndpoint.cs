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

            RouteGroupBuilder routeGroup = 
                app.MapGroup("api/v{version:apiVersion}/")
                .WithApiVersionSet(versionSet)
                .MapToApiVersion(1.0)
                .WithOpenApi();

            routeGroup.MapGet("orders/{Id}",
               async (Guid Id, IOrderReppository repos) =>
               {
                   return await repos.GetOrderDetailsAsync(Id) is var response
                    ? Results.Ok(response)
                    : Results.NotFound();
               })
               .WithName("GetOrderById")
               .WithSummary("Get the Order by Id")
               .WithDescription("JSON object containing Order information");

            routeGroup.MapGet("orders",
                async ([AsParameters] OrderListQueryDto request, IMapper mapper, IOrderReppository repos) =>
               {
                   var query = mapper.Map<OrderListQuery>(request);
                   return await repos.GetOrderListAsync(query);
               })
               .WithSummary("Get the list of Orders")
               .WithDescription("JSON object containing Order information");

            routeGroup.MapPut("orders/{Id}",
              async (Guid Id, UpdateOrderDto entity,
               IMapper mapper, IOrderReppository repos, IRabbitMqProducerService producer, IUserService user) =>
              {
                  entity.OrderId = Id;
                  var command = mapper.Map<UpdateOrderCommand>(entity);
                  await repos.UpdateOrderAsync(command);
                  await repos.SaveAsync();

                  var response = await repos.GetOrderDetailsAsync(Id);
                  ///<summary>
                  ///Broker message
                  ///</summary>
                  await producer.SendProducerMessage(response.data);

                  return Results.NoContent();
              })
              .AddEndpointFilter<ValidationFilter<UpdateOrderDto>>()
              .WithSummary("Update the Order")
              .WithDescription("Update the Order object");

            routeGroup.MapDelete("orders/{Id}",
              async (Guid Id, IOrderReppository repos) =>
              {
                  await repos.DeleteOrderAsync(Id);
                  await repos.SaveAsync();
                  return Results.NoContent();
              })
              .WithSummary("Delete the Order")
              .WithDescription("Delete the Order object");
        }
    }
}
