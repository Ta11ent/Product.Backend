using Asp.Versioning.Conventions;
using AutoMapper;
using Identity.API.Models;
using Identity.API.Validation;
using Identity.Application.Common.Models.Access.Login;

namespace Identity.API.Endpoints
{
    public static class AuthEndpoint
    {
        public static void Map(WebApplication app)
        {
            var versionSet = app.NewApiVersionSet()
                .HasApiVersion(1.0)
                .Build();

            app.MapGet("api/v{version:apiVersion}/login",
                async (HttpContext context, LoginDto entity, IAccessService service, IMapper mapper) =>
                {
                    var apiVersion = context.GetRequestedApiVersion();
                    var command = mapper.Map<UserLoginCommand>(entity);
                    var result = await service.LoginUserAsync(command);

                    return result.isSuccess
                        ? Results.Ok()
                        : !result.isSuccess && result.errors.Any(x => x.Code == "404") 
                            ? Results.NotFound()
                            : Results.Unauthorized();
                })
                .AddEndpointFilter<ValidationFilter<UserLoginDto>>()
                .WithApiVersionSet(versionSet)
                .MapToApiVersion(1.0)
                .WithSummary("Login")
                .WithDescription("JSON object")
                .WithOpenApi();
        }
    }
}
