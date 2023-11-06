var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProductData(builder.Configuration);
//add repository or somethink else
builder.Services.AddCustomAutoMapper();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
