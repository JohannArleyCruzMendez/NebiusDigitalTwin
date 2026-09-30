using NebiusDigitalTwin.Application.Commands;
using NebiusDigitalTwin.Application.Interfaces;
using NebiusDigitalTwin.Infrastructure.Data;
using NebiusDigitalTwin.Infrastructure.Repositories;
using NebiusDigitalTwin.Infrastructure.Services;
using NebiusDigitalTwin.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuraciones
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.Configure<AiProvidersSettings>(builder.Configuration.GetSection("AiProviders"));

// 2. Base de Datos, Repositorios y HttpClient con Timeout extendido para la IA
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<INodoRepository, NodoRepository>();
builder.Services.AddHttpClient<INebiusSimulationService, NebiusSimulationService>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(60); // Evita cortes prematuros mientras la IA procesa el análisis profundo
});

// 3. MediatR (CQRS)
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CrearNodoCommand).Assembly));

// 4. Controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    // Esto permite que cualquier frontend se comunique con tu API
    options.AddPolicy("PermitirFrontend",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 1. Activar CORS (Debe ir ANTES de Authorization)
app.UseCors("PermitirFrontend");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();