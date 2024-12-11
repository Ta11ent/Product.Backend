using ProductCatalog.Persistence.Migrations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IProductDbContext).Assembly));
});
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddApiVersioning(opt => opt.ApiVersionReader = new UrlSegmentApiVersionReader());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigration();
}

CategoryEndpoint.Map(app);
SubCategoryEndpoint.Map(app);
ProductEndpoint.Map(app);
CostEndpoint.Map(app);
CurrecnyEndpoint.Map(app);
ManufacturerEndpoint.Map(app);
ROEEndpoint.Map(app);

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.Run();
