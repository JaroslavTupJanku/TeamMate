using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Serilog;
using TeamMateApi.Data;
using TeamMateApi.Models;
using TeamMateApi.Models.Managers;
using TeamMateServer.Data;
using TeamMateServer.Data.Entities;
using TeamMateServer.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(AutomapperConfigurationProfile));
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddScoped<PlayerManager>();
builder.Services.AddScoped<TeamManager>();

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});


builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});


builder.Services.AddScoped<IRepository<PlayerEntity>, PlayerRepository>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    var mongoClient = sp.GetRequiredService<IMongoClient>();
    return new PlayerRepository(settings.DatabaseName, mongoClient);
});


builder.Services.AddScoped<IRepository<TeamEntity>, TeamRepository>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    var mongoClient = sp.GetRequiredService<IMongoClient>();
    return new TeamRepository(settings.DatabaseName, mongoClient);
});

builder.Services.AddTransient(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    var mongoClient = sp.GetRequiredService<IMongoClient>();
    var logger = sp.GetRequiredService<ILogger<MongoDBInitializer>>();
    return new MongoDBInitializer(mongoClient, settings.DatabaseName, logger);
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<MongoDBInitializer>();
    await initializer.InitializeDatabaseAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
