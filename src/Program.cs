using Microsoft.AspNetCore.Builder;
using Bicep.Local.Extension.Host.Extensions;
using Microsoft.Extensions.DependencyInjection;
using PassWordGenerator.BicepLocalExtension.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Register the Bicep extension host
builder.AddBicepExtensionHost(args);

// Configure and add your local extension
builder.Services
    .AddBicepExtension(
        name: "PassWordGenerator",
        version: "0.0.1",
        isSingleton: true,
        typeAssembly: typeof(Program).Assembly)
    .WithResourceHandler<PassWordGeneratorHandler>();

var app = builder.Build();

// Map the extension endpoints
app.MapBicepExtension();

await app.RunAsync();