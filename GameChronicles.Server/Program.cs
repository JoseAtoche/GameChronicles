using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories;
using Service;

var builder = WebApplication.CreateBuilder(args);

// Configure the app configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false);

// Add services to the container.
builder.Services.AddDbContext<GameChroniclesDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IUserGameService, UserGameService>();
builder.Services.AddScoped<IUserGameRepository, UserGameRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<GameChroniclesDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
