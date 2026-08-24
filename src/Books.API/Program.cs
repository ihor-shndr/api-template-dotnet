using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddDbModule();
builder.Services.AddBooksModule();
builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddHealthChecks();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UsePathBase(new PathString("/api"));
app.MapControllers();
app.UseStatusCodePages();
app.MapHealthChecks("/health");

app.Run();
