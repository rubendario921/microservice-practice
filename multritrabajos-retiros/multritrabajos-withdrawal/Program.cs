using Microsoft.EntityFrameworkCore;
using multritrabajos_withdrawal.Data;
using multritrabajos_withdrawal.Repositories;
using multritrabajos_withdrawal.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ContextDatabase>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IServiceWithdrawal, ServiceWithdrawal>();
builder.Services.AddScoped<IServiceAccount, ServiceAccount>();
builder.Services.AddSingleton<IHttpClient, CustomHttpClient>();
builder.Services.AddScoped<IServiceNotification, ServiceNotification>();


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

app.UseAuthorization();

app.MapControllers();

app.Run();
