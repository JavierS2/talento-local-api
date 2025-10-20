using Microsoft.EntityFrameworkCore;
using TalentoLocal.Models;
using TalentoLocal.Services;
using TalentoLocal.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DbContext using InMemory for local development/tests
builder.Services.AddDbContext<DbDevopsContext>(options =>
    options.UseInMemoryDatabase("TalentoLocalInMemory"));

// Register services
builder.Services.AddScoped<IEvaluationService, EvaluationService>();
builder.Services.AddScoped<IConvocationService, ConvocationService>();
builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.AddScoped<IPostulationService, PostulationService>();
builder.Services.AddScoped<IHistoryService, HistoryService>();
builder.Services.AddScoped<IPublishingEntityService, PublishingEntityService>();

// Register repositories
builder.Services.AddScoped<TalentoLocal.Repositories.Interfaces.IPostulationRepository, TalentoLocal.Repositories.Implementations.PostulationRepository>();

var app = builder.Build();

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
