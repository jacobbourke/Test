using Accounting.Platform.Core.Configuration;
using Accounting.Platform.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDeployment();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/health", (DeploymentContext deployment) =>
{
    return Results.Ok(new
    {
        status = "healthy",
        deployment = deployment.Mode.ToString()
    });
});

app.MapGet("/deployment/profile", (DeploymentContext deployment) =>
{
    return Results.Ok(new
    {
        deployment.Profile.DisplayName,
        deployment.Profile.Description,
        deployment.Profile.KeyCapabilities
    });
});

app.MapGet("/deployment/options", (IOptions<DeploymentRuntimeOptions> options) =>
{
    return Results.Ok(options.Value);
});

app.Run();
