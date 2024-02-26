using AutoMapper;
using Identity.API.Models;
using Identity.API.Validation;
using Identity.Application.Common.Models.User.Create;
using Asp.Versioning.Conventions;
using Identity.Application.Common.Models.User.Password;

namespace Identity.API.Endpoints
{
    public static class UserEndpoint
    {
        public static void Map(WebApplication app)
        {
            var versionSet = app.NewApiVersionSet()
                .HasApiVersion(1.0)
                .Build();

            RouteGroupBuilder groupBuilder = app.MapGroup("api/v{version:apiVersion}").WithApiVersionSet(versionSet);

            groupBuilder.MapGet("users/{Id}",
                async (string Id, IUserService userService) =>
                {
                    var result = await userService.GetUserByIdAsync(Id);
                    
                    return !result.isSuccess && result.errors.Any(x => x.Code == "404")
                        ? Results.NotFound(result)
                        : Results.Ok(result);
                })
                .WithName("GetUserById")
                .MapToApiVersion(1.0)
                .WithSummary("Get the User by Id")
                .WithDescription("JSON object containing User information")
                .WithOpenApi();

            groupBuilder.MapGet("users",
               async (IUserService userService) =>
               {
                   return await userService.GetUsersAsync();
               })
               .MapToApiVersion(1.0)
               .WithSummary("Get Users")
               .WithDescription("JSON object containing Users information")
               .WithOpenApi();

            groupBuilder.MapPost("users", 
            async (IUserService userService, IMapper mapper, CreateUserDto entity) => 
            {
                var command = mapper.Map<CreateUserCommand>(entity);
                var user = await userService.CreateUserAsync(command);
                return user.isSuccess 
                    ? Results.CreatedAtRoute("GetUserById", new { user.data.Id }, null)
                    : Results.BadRequest(user);
            })
            .AddEndpointFilter<ValidationFilter<CreateUserDto>>()
            .MapToApiVersion(1.0)
            .WithSummary("Create User")
            .WithDescription("JSON object")
            .WithOpenApi();

            groupBuilder.MapPut("users/{Id}/disable",
             async (string Id, IUserService service) =>
             {
                 await service.DisableUserAsync(Id);
                 return Results.NoContent();
             })
             .MapToApiVersion(1.0)
             .WithSummary("Disable the User")
             .WithDescription("Update the User object")
             .WithOpenApi();

            groupBuilder.MapPut("users/{Id}/enable",
             async (string Id, IUserService service) =>
             {
                 await service.EnableUserAsync(Id);
                 return Results.NoContent();
             })
             .MapToApiVersion(1.0)
             .WithSummary("Enable the User")
             .WithDescription("Update the User object")
             .WithOpenApi();

            groupBuilder.MapPut("users/{Id}/rp",
             async (string Id, ResetPasswordDto entity,
                    IUserService service, IMapper mapper) =>
             {
                 var command = mapper.Map<ResetPasswordCommand>(entity);
                 command.Id = Id;
                 await service.ResetPasswordAsync(command);
                 return Results.NoContent();
             })
             .AddEndpointFilter<ValidationFilter<ResetPasswordDto>>()
             .MapToApiVersion(1.0)
             .WithSummary("Reset user password")
             .WithDescription("Update the User object")
             .WithOpenApi();
        }
    }
}
