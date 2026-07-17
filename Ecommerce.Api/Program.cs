using Ecommerce.Api.Extensions;
using Ecommerce.Application;
using Ecommerce.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddSerilogLogging();

builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UsePresentation();

app.Run();
