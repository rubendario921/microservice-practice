using Consul;
using Cordillera.Distribuidas.Discovery.Consul;
using Cordillera.Distribuidas.Discovery.Fabio;
using Cordillera.Distribuidas.Discovery.Mvc;
using Cordillera.Distribuidas.Event;
using Cordillera.Distribuidas.Event.Bus;
using multitrabajos_history;
using multitrabajos_history.Messages.EventHandlers;
using multitrabajos_history.Messages.Events;
using multitrabajos_history.Repositories;
using multitrabajos_history.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<MongoSettings>(opt =>
{
    opt.Connection = builder.Configuration.GetSection("mongo:cn").Value;
    opt.DatabaseName = builder.Configuration.GetSection("mongo:database").Value;
});

//Servicios
builder.Services.AddScoped<IMongoBookDBContext, MongoDbContext>();
builder.Services.AddScoped<IServiceHistory, ServiceHistory>();

//RabbitMQ
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddRabbitMQ();
builder.Services.AddTransient<TransactionEventhandler>();
builder.Services.AddTransient<IEventHandler<TransactionCreatedEvent>, TransactionEventhandler>();

// Configuración de Consul
builder.Services.AddSingleton<IServiceId, ServiceId>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddConsul();

// Configuración de Fabio (asume que AddFabio es un método de extensión personalizado)
builder.Services.AddFabio();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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

//Configuracion del bus
var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.Subscribe<TransactionCreatedEvent, TransactionEventhandler>();

app.Run();
