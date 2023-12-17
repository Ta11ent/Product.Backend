using Identity.Application.Common.Abstractions;

namespace Identity.API.Endpoints
{
    public static class UserEndpoint
    {
        public static void Map(WebApplication app)
        {
            //var versionSet = app.NewApiVersionSet()
            //    .HasApiVersion(1.0)
            //    .Build();

            app.MapPost("api/createTest", async (IUserService userService) =>
            {
                return await userService.CreateUserAsync(new Application.Common.Models.User.Create.CreateUserCommand()
                {
                    UserName = "Admin",
                    Email = "Admin@gmail.com",
                    PhoneNumber = "",
                    Password = "_P@ssw0rd",
                    Roles = new[] { "Admin", "User" }
                }) is var response
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
