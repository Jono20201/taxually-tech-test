using System.Reflection;
using MediatR;
using Taxually.TechnicalTest.Application.CommandHandler;
using Taxually.TechnicalTest.Application.Commands;
using Taxually.TechnicalTest.Application.CountryRegistrators;
using Taxually.TechnicalTest.Application.ServiceProviders;
using Taxually.TechnicalTest.Infrastucture;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddSingleton<IServiceProvider<ICountryRegistrator>, CountryRegistratorServiceProvider>();

builder.Services.AddTransient<DeCountryRegistrator>();
builder.Services.AddTransient<GbCountryRegistrator>();
builder.Services.AddTransient<FrCountryRegistrator>();

builder.Services.AddScoped<IRequestHandler<RegisterCompanyForVatCommand, Unit>, RegisterCompanyForVatCommandHandler>();

builder.Services.AddTransient<IHttpClient, TaxuallyHttpClient>();
builder.Services.AddTransient<IQueueClient, TaxuallyQueueClient>();

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
