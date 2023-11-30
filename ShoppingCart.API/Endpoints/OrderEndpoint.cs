using Asp.Versioning.Conventions;
using AutoMapper;
using ShoppingCart.API.Models.Order;
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

            app.MapGet("api/v{version:apiVersion}/order/{Id}",
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

            app.MapGet("api/v{version:apiVersion}/order",
                async(HttpContext context, [AsParameters]OrderListQueryDto request, IMapper mapper, IOrderReppository repos) =>
               {
                   var apiVersion = context.GetRequestedApiVersion();
                   var query = mapper.Map<OrderListQuery>(request);
                   return await repos.GetOrderListAsync(query);
                    //return await sender.Send(query);
                })
               .WithApiVersionSet(versionSet)
               .MapToApiVersion(1.0)
               .WithSummary("Get the list of Orders")
               .WithDescription("JSON object containing Order information")
               .WithOpenApi();
        }
    }
}
