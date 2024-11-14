using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoNuoma.Core.Contracts;
using AutoNuoma.Core.Repo;

var builder = WebApplication.CreateBuilder(args);



// Automobiliai repo
builder.Services.AddTransient<IAutomobilisRepository>(x => new AutomobilisRepository("Server=localhost;Database=C#mokymai;Trusted_Connection=True;TrustServerCertificate=true;"));

// Klientai repo
builder.Services.AddTransient<IKlientasRepository>(x => new KlientasRepository("Server=localhost;Database=C#mokymai;Trusted_Connection=True;TrustServerCertificate=true;"));

// Nuomos u�sakymai repo

builder.Services.AddTransient<IDarbuotojasRepository>(x => new DarbuotojasRepository("Server=localhost;Database=C#mokymai;Trusted_Connection=True;TrustServerCertificate=true;"));

builder.Services.AddTransient<INuomosUzsakymasRepository>(x => new NuomosUzsakymasRepository("Server=localhost;Database=C#mokymai;Trusted_Connection=True;TrustServerCertificate=true;"));

// Pridedame MVC arba API paslaugas
builder.Services.AddControllers();

// Swagger/OpenAPI konfig�racija
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Konfig�ruojame HTTP u�klaus� apdorojimo sek�
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); // Mar�rut� registracija

app.Run();
