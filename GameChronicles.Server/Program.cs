using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repository.Data;
using Repository.Repositories;
using Service.Factories;
using Service.Implementations;
using Service.Interfaces;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Configure logging
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// Configure the app configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false);

// Add services to the container
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Apply database migrations
ApplyMigrations(app);

// Configure the HTTP request pipeline
ConfigurePipeline(app);

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    // Database configuration
    services.AddDbContext<GameChroniclesDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("ConnectionString")));

    services.AddDatabaseDeveloperPageExceptionFilter();

    // Add controllers
    services.AddControllersWithViews();
    services.AddControllers();

    // Register repositories
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IGameRepository, GameRepository>();
    services.AddScoped<IUserGameRepository, UserGameRepository>();

    // Register services
    services.AddScoped<IGameService, GameService>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IUserGameService, UserGameService>();

    // Register service factory
    services.AddSingleton<IServiceFactory, ServiceFactory>();

    // Swagger configuration
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "GameChronicles", Version = "v1" });
    });
}

void ApplyMigrations(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<GameChroniclesDbContext>();
    db.Database.Migrate();
}

void ConfigurePipeline(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "GameChronicles V1");
            c.RoutePrefix = "swagger";
        });
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.UseSerilogRequestLogging(); // Add Serilog middleware for logging HTTP requests
    app.MapControllers();
}
