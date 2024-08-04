var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection(nameof(JwtConfig)));
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IAuthDbContext).Assembly));
});
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddJwtAuthenticationConfiguration();
builder.Services.AddAuthorizationConfiguration();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddApiVersioning(opt => opt.ApiVersionReader = new UrlSegmentApiVersionReader());

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

UserEndpoint.Map(app);
AuthEndpoint.Map(app);

app.UseAuthentication();
app.UseAuthorization();
app.UseExceptionHandler();
app.UseHttpsRedirection();

app.Run();
