using JwtAuthenticationManager.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

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
