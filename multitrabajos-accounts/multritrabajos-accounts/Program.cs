using Consul;
using Cordillera.Distribuidas.Discovery.Consul;
using Cordillera.Distribuidas.Discovery.Fabio;
using Cordillera.Distribuidas.Discovery.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using multritrabajos_accounts.Data;
using multritrabajos_accounts.Repository;
using multritrabajos_accounts.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Service datacontext sql server

builder.Services.AddDbContext<ContextDatabase>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Servicios
builder.Services.AddScoped<IServicesAccount, ServicesAccount>();

builder.Services.AddControllers();
// Configuración de Consul
builder.Services.AddSingleton<IServiceId, ServiceId>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddConsul();

// Configuración de Fabio (asume que AddFabio es un método de extensión personalizado)
builder.Services.AddFabio();

var app = builder.Build();

//Instance create database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ContextDatabase>();
        //Funcion static crea usuarios
        DataInitializer.Initialize(context);
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
