var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IProductDbContext).Assembly));
});
builder.Services.AddApplication();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddApiVersioning(opt => opt.ApiVersionReader = new UrlSegmentApiVersionReader());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

CategoryEndopoint.Map(app);

app.UseHttpsRedirection();

app.Run();
