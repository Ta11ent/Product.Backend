using AutoMapper;
using Identity.API.Models;
using Identity.API.Validation;
using Identity.Application.Common.Models.User.Create;
using Asp.Versioning.Conventions;
using Identity.Application.Common.Models.User.Password;
using Microsoft.AspNetCore.Hosting.Server;

namespace Identity.API.Endpoints
{
    public static class UserEndpoint
    {
        public static void Map(WebApplication app)
        {
            var versionSet = app.NewApiVersionSet()
                .HasApiVersion(1.0)
                .Build();


            app.MapGet("api/v{version:apiVersion}/users/{Id}",
                async (HttpContext context, string Id, IUserService userService) =>
                {
                    var apiVersion = context.GetRequestedApiVersion();
                    return await userService.GetUserAsync(Id) is var response 
                        ? Results.Ok(response)
                        : Results.NotFound();
                })
                .WithName("GetUserById")
                .WithApiVersionSet(versionSet)
                .MapToApiVersion(1.0)
                .WithSummary("Get the User by Id")
                .WithDescription("JSON object containing User information")
                .WithOpenApi();

            app.MapGet("api/v{version:apiVersion}/users",
               async (HttpContext context, string Id, IUserService userService) =>
               {
                   var apiVersion = context.GetRequestedApiVersion();
                   return await userService.GetUsersAsync();
               })
               .WithApiVersionSet(versionSet)
               .MapToApiVersion(1.0)
               .WithSummary("Get Users")
               .WithDescription("JSON object containing Users information")
               .WithOpenApi();

            app.MapPost("pi/v{version:apiVersion}/users", 
            async (HttpContext context, IUserService userService, IMapper mapper, CreateUserDto entity) => 
            {

                var apiVersion = context.GetRequestedApiVersion();
                var command = mapper.Map<CreateUserCommand>(entity);
                var user = await userService.CreateUserAsync(command);
                return Results.CreatedAtRoute("GetUserById", new { user.data.UserId });
            })
            .AddEndpointFilter<ValidationFilter<CreateUserDto>>()
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1.0)
            .WithSummary("Create User")
            .WithDescription("JSON object")
            .WithOpenApi();

            app.MapPut("api/v{version:apiVersion}/users/{Id}/disable",
             async (HttpContext context, string Id, IUserService service) =>
             {
                 var apiVersion = context.GetRequestedApiVersion();
                 await service.DisableUserAsync(Id);
                 return Results.NoContent();
             })
             .WithApiVersionSet(versionSet)
             .MapToApiVersion(1.0)
             .WithSummary("Disable the User")
             .WithDescription("Update the User object")
             .WithOpenApi();

            app.MapPut("api/v{version:apiVersion}/users/{Id}/enable",
             async (HttpContext context, string Id, IUserService service) =>
             {
                 var apiVersion = context.GetRequestedApiVersion();
                 await service.EnableUserAsync(Id);
                 return Results.NoContent();
             })
             .WithApiVersionSet(versionSet)
             .MapToApiVersion(1.0)
             .WithSummary("Enable the User")
             .WithDescription("Update the User object")
             .WithOpenApi();

            app.MapPut("api/v{version:apiVersion}/users/{Id}/resetPassword",
             async (HttpContext context, string Id, ResetPasswordDto entity,
                    IUserService service, IMapper mapper) =>
             {
                 var apiVersion = context.GetRequestedApiVersion();
                 var command = mapper.Map<ResetPasswordCommand>(entity);
                 command.Id = Id;
                 await service.ResetPassword(command);
                 return Results.NoContent();
             })
             .AddEndpointFilter<ValidationFilter<ResetPasswordDto>>()
             .WithApiVersionSet(versionSet)
             .MapToApiVersion(1.0)
             .WithSummary("Reset user password")
             .WithDescription("Update the User object")
             .WithOpenApi();
        }
    }
}
