﻿using Asp.Versioning.Conventions;
using MassTransit;
using ShoppingCart.API.Models.Order;
using ShoppingCart.Application.Application.Commands.Order.DeleteOrder;
using ShoppingCart.Application.Application.Commands.Order.UpdateOrder;
using ShoppingCart.Application.Application.Queries.Order.GetOrderDetails;
using ShoppingCart.Application.Queries.Order.GetOrderList;

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
               async (Guid Id, [AsParameters]GetOrderDetailsDto param, IMapper mapper, ISender sender) =>
               {
                   param.OrderId = Id;
                   var query = mapper.Map<GetOrderDetailsQuery>(param);
                   return await sender.Send(query) is var response
                    ? Results.Ok(response)
                    : Results.NotFound();
               })
               .WithName("GetOrderById")
               .WithSummary("Get the Order by Id")
               .WithDescription("JSON object containing Order information");

            routeGroup.MapGet("orders",
                async ([AsParameters] OrderListQueryDto request, IMapper mapper, ISender sender) =>
               {
                   var query = mapper.Map<GetOrderListQuery>(request);
                   return await sender.Send(query);
               })
               .WithSummary("Get the list of Orders")
               .WithDescription("JSON object containing Order information");

            routeGroup.MapPut("orders/{Id}",
              async (Guid Id, UpdateOrderDto entity,
               IMapper mapper, ISender sender, IUserService user, IPublishEndpoint publishEndpoint) =>
              {
                  entity.OrderId = Id;
                  var command = mapper.Map<UpdateOrderCommand>(entity);
                  var answer = await sender.Send(command);
                  if(answer && entity.Status == "Paid") { 
                      var response = await sender.Send(new GetOrderDetailsQuery() { OrderId = Id, Ccy = "USD" });
                      var order = mapper.Map<OrderPaidDto>(response.data);
                      order.Currency = "USD";
                      await publishEndpoint.Publish(order);
                  }

                  return Results.NoContent();
              })
              .AddEndpointFilter<ValidationFilter<UpdateOrderDto>>()
              .WithSummary("Update the Order")
              .WithDescription("Update the Order object");

            routeGroup.MapDelete("orders/{Id}",
              async (Guid Id, ISender sender) =>
              {
                  await sender.Send(new DeleteOrderCommand() { OrderId = Id });
                  return Results.NoContent();
              })
              .WithSummary("Delete the Order")
              .WithDescription("Delete the Order object");
        }
    }
}
