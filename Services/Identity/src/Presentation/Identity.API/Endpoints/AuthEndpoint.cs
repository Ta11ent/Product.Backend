﻿using Asp.Versioning.Conventions;
using AutoMapper;
using Identity.API.Models;
using Identity.API.Validation;
using Microsoft.AspNetCore.Mvc;
using Identity.API.Cookies;
using Identity.Application.Common.Models.Access.Login;
using Identity.Application.Common.Models.Access;
using Identity.src.Core.Application.Common.Abstractions;

namespace Identity.API.Endpoints
{
    public static class AuthEndpoint
    {
        public static void Map(WebApplication app)
        {
            var versionSet = app.NewApiVersionSet()
                .HasApiVersion(1.0)
                .Build();

            RouteGroupBuilder groupBuilder =
                app.MapGroup("api/v{version:apiVersion}")
                .WithApiVersionSet(versionSet)
                .MapToApiVersion(1.0)
                .WithOpenApi();

            groupBuilder.MapPost("Login",
                async (HttpContext context, IAccessService service, IMapper mapper, [FromBody] LoginDto entity) =>
                {
                    var command = mapper.Map<LoginCommand>(entity);
                    var response = await service.LoginUserAsync(command);

                    if (response.isSuccess)
                        new Cookie(context).SetRefreshToken(response.data.RefreshToken, response.data.RefreshTokenExp);

                    return response.isSuccess
                         ? Results.Ok(response)
                         : Results.Unauthorized();
                })
                .AddEndpointFilter<ValidationFilter<LoginDto>>()
                .WithSummary("Login")
                .WithDescription("JSON object");

            groupBuilder.MapPost("RefreshToken",
                async (HttpContext context, IAccessService service, IMapper mapper, [FromBody] RefreshTokenDto entity) =>
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
                        : Results.Unauthorized();
                })
                .AddEndpointFilter<ValidationFilter<RefreshTokenDto>>()
                .WithSummary("Refresh Token")
                .WithDescription("JSON object");

            groupBuilder.MapPost("Logout",
               async (HttpContext context, IAccessService service, IMapper mapper, string? refreshToken) =>
               {
                   if (refreshToken is null && !context.Request.Cookies.ContainsKey("refreshToken"))
                       return Results.BadRequest();

                   refreshToken = refreshToken ?? context.Request.Cookies["refreshToken"];

                   return await service.LogoutUserAsync(refreshToken!) is var response
                    ? Results.Ok()
                    : Results.BadRequest();
               })
               .WithSummary("Logout")
               .WithDescription("JSON object");
        }
    }
}
