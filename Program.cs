using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using TalentoLocal.Models;
using TalentoLocal.Repositories;
using TalentoLocal.Repositories.Implementations;
using TalentoLocal.Repositories.Interfaces;
using TalentoLocal.Services.Implementations;
using TalentoLocal.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {

        opts.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<DbDevopsContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AzureSqlConnection"));
    // Ver consultas SQL ejecutadas en tiempo real
    options.LogTo(Console.WriteLine, LogLevel.Information);
    //Muestra los valores reales de los parámetros
    options.EnableSensitiveDataLogging();
    //Muestra errores internos detallados
    options.EnableDetailedErrors();
});

// ----------------------
//   Registro de servicios
// ----------------------
builder.Services.AddScoped<IEvaluationService, EvaluationService>();
builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.AddScoped<IOfferCategoryService, OfferCategoryService>(); // (X)
builder.Services.AddScoped<IPostulationService, PostulationService>();
builder.Services.AddScoped<IPostulationStatusService, PostulationStatusService>(); // (X)
builder.Services.AddScoped<IBlobStorageService, AzureBlobStorageService>();
builder.Services.AddScoped<IFavoriteService, FavoriteService>();


// ----------------------
//   Registro de repositorios
// ----------------------

builder.Services.AddScoped<IEvaluationRepository, EvaluationRepository>(); 
builder.Services.AddScoped<IOfferRepository, OfferRepository>(); 
builder.Services.AddScoped<IOfferCategoryRepository, OfferCategoryRepository>();
builder.Services.AddScoped<IPostulationRepository, PostulationRepository>();  
builder.Services.AddScoped<IPostulationStatusRepository, PostulationStatusRepository>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();

builder.Services.AddHealthChecks()
    .AddSqlServer(builder.Configuration.GetConnectionString("AzureSqlConnection")!);

// ---- HealthChecks UI ----
builder.Services.AddHealthChecksUI(options =>
{
    options.SetEvaluationTimeInSeconds(10); // cada 10s revisa el health
    options.MaximumHistoryEntriesPerEndpoint(60); // historial
    options.AddHealthCheckEndpoint("API TalentoLocal", "/health"); // apunta a tu health
})
.AddInMemoryStorage();



var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI();




app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapHealthChecksUI(options =>
{
    options.UIPath = "/health-ui";           // URL del dashboard
    options.ApiPath = "/health-ui-api";      // API interna de la UI
});

app.Run();
