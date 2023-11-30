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

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<ProductDbContext>();
        DbInitialize.Initialize(context);
    }
    catch (Exception exception)
    {
        var loger = serviceProvider.GetRequiredService<ILogger<Program>>();
        loger.LogError(exception, "Ann error ocurred while app initialization");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

CategoryEndpoint.Map(app);
ProductEndpoint.Map(app);

app.UseHttpsRedirection();

app.Run();
