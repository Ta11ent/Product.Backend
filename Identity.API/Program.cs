using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));
builder.Services.AddJwtAuthenticationConfiguration(builder.Configuration);
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("api/v1.0/test", async () =>
{
    List<Claim> claims = new List<Claim>();
    claims.Add(new Claim(ClaimTypes.Sid, Guid.NewGuid().ToString()));
    claims.Add(new Claim("level", "123"));
    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("gNdGtiUpafqdzGlrMXIYhGftqmrtfWss"));

    var jwt = new JwtSecurityToken(
            issuer: "ApiToken",
            audience: "CC_Agent",
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(1)),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );
    return new JwtSecurityTokenHandler().WriteToken(jwt);
});



app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.Run();

