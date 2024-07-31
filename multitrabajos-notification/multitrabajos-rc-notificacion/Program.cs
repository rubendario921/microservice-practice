using Microsoft.EntityFrameworkCore;
using multitrabajos_rc_notificacion.Data;
using multitrabajos_rc_notificacion.Repositories;
using multitrabajos_rc_notificacion.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Datacontext
builder.Services.AddDbContext<ContextDatabase>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 21))
    )
);

//Servicios
builder.Services.AddScoped<IServiceNotification, ServiceNotification>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Create Database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ContextDatabase>();
        DbInitializer.Initializer(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error en crear la base de datos.");
    }
}

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthorization();

    app.MapControllers();

    app.Run();