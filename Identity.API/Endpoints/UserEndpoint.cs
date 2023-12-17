using AutoMapper;
using Identity.API.Models;
using Identity.Application.Common.Models.User.Create;

namespace Identity.API.Endpoints
{
    public static class UserEndpoint
    {
        public static void Map(WebApplication app)
        {
            //var versionSet = app.NewApiVersionSet()
            //    .HasApiVersion(1.0)
            //    .Build();

            app.MapPost("api/createTest", 
                async (IUserService userService, IMapper mapper, CreateUserDto user) =>
            {
                var command = mapper.Map<CreateUserCommand>(user);
                return await userService.CreateUserAsync(command) is var response
                    ? Results.Ok(response)
                    : Results.BadRequest(response.Errors);
            });

            //app.MapGet("api/login",
            //   async (HttpContext context, 
            //    LoginDto model,
            //    ITokenService tokenService,
            //    UserManager<IdentityUser> userManager) => {
            //       // var apiVersion = context.GetRequestedApiVersion();
            //       //return await sender.Send(new GetCategoryDetailsQuery { CategoryId = Id }) is var response
            //       //    ? Results.Ok(response)
            //       //    : Results.NotFound();
            //       var user = await userManager.FindByNameAsync(model.UserName);
            //       if (user == null)
            //           return Results.Unauthorized();
            //       var succeeded = await userManager.CheckPasswordAsync(user, model.Password);
            //       if (succeeded)
            //       {
            //            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            //       }

            //        return Results.Ok();



            //   })
            //.WithName("GetCategoryById")
            // .WithApiVersionSet(versionSet)
            //.MapToApiVersion(1.0)
            //  .WithSummary("Get the Category by Id")
            // .WithDescription("JSON object containing Category information")
            // .WithOpenApi();
        }
    }
}
