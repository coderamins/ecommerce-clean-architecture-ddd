using Ecommerce.Api.Extensions;
using Ecommerce.Application;
using Ecommerce.Infrastructure.DependencyInjection;
using Ecommerce.Infrastructure.Messaging.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddSerilogLogging();

builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UsePresentation();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider
        .GetRequiredService<IRabbitMqInitializer>();

    await initializer.InitializeAsync();
}

app.Run();
