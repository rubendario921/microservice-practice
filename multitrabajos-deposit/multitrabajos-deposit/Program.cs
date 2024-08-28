using Consul;
using Cordillera.Distribuidas.Discovery.Consul;
using Cordillera.Distribuidas.Discovery.Fabio;
using Cordillera.Distribuidas.Discovery.Mvc;
using Cordillera.Distribuidas.Event;
using MediatR;
using Microsoft.EntityFrameworkCore;
using multitrabajos_deposit.Data;
using multitrabajos_deposit.Messages.CommandHandlers;
using multitrabajos_deposit.Messages.Commands;
using multitrabajos_deposit.Repositories;
using multitrabajos_deposit.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ContextDatabase>(options =>
  options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//Services
builder.Services.AddHttpClient();
builder.Services.AddScoped<IServicesTransaction, ServicesTransaction>();
builder.Services.AddSingleton<IHttpClient, CustomHttpClient>();
builder.Services.AddScoped<IServicesAccount, ServicesAccount>();
builder.Services.AddScoped<IServiceNotification, ServiceNotification>();

//RabbitMQ
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddRabbitMQ();
builder.Services.AddTransient<IRequestHandler<TransactionCreateCommand, bool>, TransactionCommandHandler>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Configuración de Consul
builder.Services.AddSingleton<IServiceId, ServiceId>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddConsul();

// Configuración de Fabio (asume que AddFabio es un método de extensión personalizado)
builder.Services.AddFabio();

var app = builder.Build();

//Create Database

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ContextDatabase>();
        //Funcion static crea usuarios
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
