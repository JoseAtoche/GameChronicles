using Service;
    
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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


// Rutas públicas
var publicPaths = new List<string>
{
 
};



// Middleware de seguridad (Validación de la clave de API)
app.Use((context, next) =>
{
    var gameService = context.RequestServices.GetRequiredService<GameService>();

    if (publicPaths.Contains(context.Request.Path))
    {
        return next();
    }

    var apiKey = context.Request.Headers["ApiKey"];
    var expectedApiKey = builder.Configuration["ApiSettings:ApiKey"];

    if (apiKey != expectedApiKey)
    {
        context.Response.StatusCode = 401; // Unauthorized
        return context.Response.WriteAsync("INVALID API key");
    }

    return next();
});



// Middleware para manejar las operaciones de la capa de servicio
app.Use((context, next) =>
{
    var gameService = context.RequestServices.GetRequiredService<GameService>();

    //Aquí podemos insertar algun Middleware de la capa de servicio

    return next();
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
