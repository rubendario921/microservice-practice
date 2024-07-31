using multitrabajos_history;
using multitrabajos_history.Repositories;
using multitrabajos_history.Services;

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

app.UseAuthorization();

app.MapControllers();

app.Run();
