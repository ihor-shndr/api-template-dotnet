using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

builder.AddDbModule();
builder.Services.AddBooksModule();
builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFeatureManagement();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UsePathBase(new PathString("/api"));
app.MapControllers();
app.UseStatusCodePages();
app.MapHealthChecks("/health");

app.Run();