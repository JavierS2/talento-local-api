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
    options.UseSqlServer(builder.Configuration.GetConnectionString("AzureSqlConnection")));

// ----------------------
//   Registro de servicios
// ----------------------
builder.Services.AddScoped<IEvaluationService, EvaluationService>();
builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.AddScoped<IOfferCategoryService, OfferCategoryService>();
builder.Services.AddScoped<IPostulationService, PostulationService>();
builder.Services.AddScoped<IPostulationStatusService, PostulationStatusService>();

// ----------------------
//   Registro de repositorios
// ----------------------

builder.Services.AddScoped<IEvaluationRepository, EvaluationRepository>(); // get, getById, 
builder.Services.AddScoped<IOfferRepository, OfferRepository>(); // get, getById, 
builder.Services.AddScoped<IOfferCategoryRepository, OfferCategoryRepository>(); // CORRECTO TODOS
builder.Services.AddScoped<IPostulationRepository, PostulationRepository>(); // get, getById, 
builder.Services.AddScoped<IPostulationStatusRepository, PostulationStatusRepository>(); // Malos: update

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
