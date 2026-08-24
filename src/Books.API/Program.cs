using Scalar.AspNetCore;

const string DevCorsPolicy = "DevCors";

var builder = WebApplication.CreateBuilder(args);

builder.AddDbModule();
builder.Services.AddBooksModule();
builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddHealthChecks();
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddPolicy(DevCorsPolicy, policy =>
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.UseCors(DevCorsPolicy);
}

app.UseHttpsRedirection();
app.UsePathBase(new PathString("/api/v1"));
app.MapControllers();
app.UseStatusCodePages();
app.MapHealthChecks("/health");

app.Run();
