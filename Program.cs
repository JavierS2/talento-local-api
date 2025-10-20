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
builder.Services.AddScoped<TalentoLocal.Repositories.IOfferRepository, TalentoLocal.Repositories.OfferRepository>();
builder.Services.AddScoped<TalentoLocal.Repositories.Interfaces.IEvaluationRepository, TalentoLocal.Repositories.Implementations.EvaluationRepository>();
builder.Services.AddScoped<TalentoLocal.Repositories.Interfaces.IConvocationRepository, TalentoLocal.Repositories.Implementations.ConvocationRepository>();
builder.Services.AddScoped<TalentoLocal.Repositories.Interfaces.IPublishingEntityRepository, TalentoLocal.Repositories.Implementations.PublishingEntityRepository>();
builder.Services.AddScoped<TalentoLocal.Repositories.Interfaces.IHistoryRepository, TalentoLocal.Repositories.Implementations.HistoryRepository>();

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
