using Consul;
using Cordillera.Distribuidas.Discovery.Consul;
using Cordillera.Distribuidas.Discovery.Fabio;
using Cordillera.Distribuidas.Discovery.Mvc;
using Cordillera.Distribuidas.Event;
using MediatR;
using Microsoft.EntityFrameworkCore;
using multritrabajos_withdrawal.Data;
using multritrabajos_withdrawal.Messages.CommandHandlers;
using multritrabajos_withdrawal.Messages.Commands;
using multritrabajos_withdrawal.Repositories;
using multritrabajos_withdrawal.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ContextDatabase>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IServiceWithdrawal, ServiceWithdrawal>();
builder.Services.AddSingleton<IHttpClient, CustomHttpClient>();
builder.Services.AddScoped<IServiceAccount, ServiceAccount>();
builder.Services.AddScoped<IServiceNotification, ServiceNotification>();
//RabbitMQ
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddRabbitMQ();
builder.Services.AddTransient<IRequestHandler<TransactionCreateCommand, bool>, TransactionCommandHandler>();

// Configuración de Consul
builder.Services.AddSingleton<IServiceId, ServiceId>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddConsul();

// Configuración de Fabio (asume que AddFabio es un método de extensión personalizado)
builder.Services.AddFabio();
builder.Services.AddControllers();

var app = builder.Build();

// Inicialización de la base de datos
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ContextDatabase>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}
// Configuración de Consul para el registro y desregistro del servicio
var serviceId = app.UseConsul();
IHostApplicationLifetime applicationLifetime = app.Lifetime;
var consulClient = app.Services.GetRequiredService<IConsulClient>();
applicationLifetime.ApplicationStopped.Register(() =>
{
    consulClient.Agent.ServiceDeregister(serviceId);
});
app.UseAuthorization();

app.MapControllers();

app.Run();
