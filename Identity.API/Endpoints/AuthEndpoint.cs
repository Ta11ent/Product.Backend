using Asp.Versioning.Conventions;
using AutoMapper;
using Identity.API.Models;
using Identity.API.Validation;
using Identity.Application.Common.Models.Access.Login;
using Microsoft.AspNetCore.Mvc;
using Identity.API.Cookies;

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
                async (HttpContext context, IAccessService service, IMapper mapper, [FromBody]LoginDto entity) =>
                {
                    var apiVersion = context.GetRequestedApiVersion();
                    var command = mapper.Map<LoginCommand>(entity);
                    var response = await service.LoginUserAsync(command);

                    if(response.isSuccess)
                        new Cookie(context).SetRefreshToken(response.data.RefreshToken, response.data.RefreshTokenExp);

                    return response.isSuccess
                         ? Results.Ok(response)
                         : !response.isSuccess && response.errors.Any(x => x.Code == "404") 
                             ? Results.NotFound()
                             : Results.Unauthorized();
                })
                .AddEndpointFilter<ValidationFilter<LoginDto>>()
                .WithApiVersionSet(versionSet)
                .MapToApiVersion(1.0)
                .WithSummary("Login")
                .WithDescription("JSON object")
                .WithOpenApi();
        }
    }
}
