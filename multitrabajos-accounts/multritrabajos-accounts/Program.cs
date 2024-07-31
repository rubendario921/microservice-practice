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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

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


app.UseAuthorization();

app.MapControllers();

app.Run();
