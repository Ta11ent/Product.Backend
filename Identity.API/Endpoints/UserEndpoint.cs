using AutoMapper;
using Identity.API.Models;
using Identity.API.Validation;
using Identity.Application.Common.Models.User.Create;
using Asp.Versioning.Conventions;

namespace Identity.API.Endpoints
{
    public static class UserEndpoint
    {
        public static void Map(WebApplication app)
        {
            var versionSet = app.NewApiVersionSet()
                .HasApiVersion(1.0)
                .Build();

            app.MapPost("api/createTest", 
            async (HttpContext context, IUserService userService, IMapper mapper, CreateUserDto user) => 
            {

                var apiVersion = context.GetRequestedApiVersion();
                var command = mapper.Map<CreateUserCommand>(user);
                return await userService.CreateUserAsync(command) is var response
                    ? Results.Ok(response)
                    : Results.BadRequest(response.Errors);
            })
            .AddEndpointFilter<ValidationFilter<CreateUserDto>>()
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1.0)
            .WithSummary("Create User")
            .WithDescription("JSON object")
            .WithOpenApi();
        }
    }
}
