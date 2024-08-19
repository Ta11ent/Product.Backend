using JwtAuthenticationManager.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

var testId = "D4CF3E6E-6F6D-4F3C-A640-962CBBEF2E99";

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddJwtAuthenticationConfiguration();
builder.Services.AddAuthorizationConfiguration();

var app = builder.Build();


app.MapControllers();
await app.UseOcelot();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
