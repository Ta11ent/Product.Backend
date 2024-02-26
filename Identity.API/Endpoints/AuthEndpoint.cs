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

            RouteGroupBuilder groupBuilder = app.MapGroup("api/v{version:apiVersion}").WithApiVersionSet(versionSet);

            groupBuilder.MapPost("login",
                async (HttpContext context, IAccessService service, IMapper mapper, [FromBody]LoginDto entity) =>
                {
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
                .MapToApiVersion(1.0)
                .WithSummary("Login")
                .WithDescription("JSON object")
                .WithOpenApi();

            groupBuilder.MapPost("RefreshToken", 
                async (HttpContext context, IAccessService service, IMapper mapper, [FromBody]RefreshTokenDto entity) =>
                {
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
                .MapToApiVersion(1.0)
                .WithSummary("Refresh Token")
                .WithDescription("JSON object")
                .WithOpenApi();

            groupBuilder.MapPost("Logout",
               async (HttpContext context, IAccessService service, IMapper mapper, string? refreshToken) =>
               {
                   var apiVersion = context.GetRequestedApiVersion();

                   if (refreshToken is null && !context.Request.Cookies.ContainsKey("refreshToken"))
                       return Results.BadRequest();
                   refreshToken = refreshToken ?? context.Request.Cookies["refreshToken"];

                   return await service.LogoutUserAsync(refreshToken!) is var response
                    ? Results.Ok()
                    : Results.BadRequest();
               })
               .MapToApiVersion(1.0)
               .WithSummary("Logout")
               .WithOpenApi();


        }
    }
}
