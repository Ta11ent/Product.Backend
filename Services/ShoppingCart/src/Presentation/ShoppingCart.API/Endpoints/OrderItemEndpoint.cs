using Asp.Versioning.Conventions;
using ShoppingCart.API.Models.ProductRange;
using ShoppingCart.Application.Application.Commands.Order.UpdateOrder;
using ShoppingCart.Application.Application.Commands.ProductRange.CreateProductRange;
using ShoppingCart.Application.Application.Commands.ProductRange.DeleteProductRange;
using ShoppingCart.Application.Application.Commands.ProductRange.UpdateProductRange;

namespace ShoppingCart.API.Endpoints
{
    public static class OrderItemEndpoint
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

            groupBuilder.MapPost("OrderItems",
                async(CreateOrderItemDto data, ISender sender, IMapper mapper ) =>
                {
                    var command = mapper.Map<CreateOrderItemCommand>(data);
                    var id = await sender.Send(command);
                    return Results.CreatedAtRoute("GetOrderById", new { id });
                })
                .AddEndpointFilter<ValidationFilter<CreateOrderItemDto>>()
                .WithSummary("Create a OrderItem")
                .WithDescription("Create a OrderItem object");

            groupBuilder.MapPut("OrderItems/{Id}",
                async (UpdateOrderItemDto data, Guid Id, IMapper mapper, ISender sender) => 
                {
                    data.OrderItemId = Id;
                    var command = mapper.Map<UpdateOrderItemCommand>(data);
                    await sender.Send(command);
                    return Results.NoContent();
                })
                .AddEndpointFilter<ValidationFilter<UpdateOrderItemDto>>()
                .WithSummary("Update the OrderItem")
                .WithDescription("Update the OrderItem object");

            groupBuilder.MapDelete("OrderItems/{Id}",
                async (Guid Id, ISender sender) =>
                {
                    await sender.Send(new DeleteOrderItemCommand() { OrderItemId = Id});
                    return Results.NoContent();
                })
                .WithSummary("Delete the OrderItem")
                .WithDescription("Delete the OrderItem object");
        }
    }
}
