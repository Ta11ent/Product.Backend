using Asp.Versioning.Conventions;
using AutoMapper;
using Identity.API.Models;
using Identity.API.Validation;
using Microsoft.AspNetCore.Mvc;
using Identity.API.Cookies;
using Identity.Application.Common.Models.Access.Login;
using Identity.Application.Common.Models.Access;

namespace Identity.API.Endpoints
{
    public static class AuthEndpoint
    {
        public static void Map(WebApplication app)
        {
            var versionSet = app.NewApiVersionSet()
                .HasApiVersion(1.0)
                .Build();
            
            app.MapPost("api/v{version:apiVersion}/login",
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

            app.MapPost("api/v{version:apiVersion}/RefreshToken", 
                async (HttpContext context, IAccessService service, IMapper mapper, [FromBody]RefreshTokenDto entity) =>
                {
                    var apiVersion = context.GetRequestedApiVersion();

                    if (entity.RefreshToken is null && !context.Request.Cookies.ContainsKey("refreshToken"))
                        return Results.BadRequest();
                    entity.RefreshToken = entity.RefreshToken ?? context.Request.Cookies["refreshToken"];

                    var command = mapper.Map<RefreshCommand>(entity);
                    var response = await service.RefreshUserAsync(command);

                    if (response.isSuccess)
                        new Cookie(context).SetRefreshToken(response.data.RefreshToken, response.data.RefreshTokenExp);

                    return response.isSuccess
                        ? Results.Ok(response)
                        : !response.isSuccess && response.errors.Any(x => x.Code == "403")
                            ? Results.Forbid()
                            : Results.Unauthorized();
                })
                .AddEndpointFilter<ValidationFilter<RefreshTokenDto>>()
                .WithApiVersionSet(versionSet)
                .MapToApiVersion(1.0)
                .WithSummary("Refresh Token")
                .WithDescription("JSON object")
                .WithOpenApi();

        }
    }
}
