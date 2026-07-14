using Ecommerce.Api.Extensions;
using Ecommerce.Api.Middleware;
using Ecommerce.Application;
using Ecommerce.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddApplication();

builder.Host.UseSerilog((context,services,configuration)=>
{
    configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext();
});

builder.Services.AddInfrastructure(
    builder.Configuration
    .GetConnectionString("DefaultConnection")?? "");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();
app.UseGlobalExceptions();
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseSerilogRequestLogging();
app.MapControllers();

app.Run();
