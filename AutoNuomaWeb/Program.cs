using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoNuoma.Core.Contracts;

using Serilog;
using AutoNuoma.Core.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(builder.Configuration.GetConnectionString("MongoDb")));

//builder.Services.AddSingleton<AutomobilisService>();
//builder.Services.AddTransient<IKlientasRepository>(x => new KlientasRepository("Server=localhost;Database=C#mokymai;Trusted_Connection=True;TrustServerCertificate=true;"));


//builder.Services.AddTransient<IDarbuotojasRepository>(x => new DarbuotojasRepository("Server=localhost;Database=C#mokymai;Trusted_Connection=True;TrustServerCertificate=true;"));

//builder.Services.AddTransient<INuomosUzsakymasRepository>(x => new NuomosUzsakymasRepository("Server=localhost;Database=C#mokymai;Trusted_Connection=True;TrustServerCertificate=true;"));

//builder.Services.AddTransient<DataBackupService>();

//builder.Services.AddSingleton<IRecieptRepository>(provider => new RecieptRepository("C:\\path\\to\\receipts", provider.GetRequiredService<IAutomobilisRepository>()));
//builder.Services.AddSingleton<RentalService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();




if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); 

var log = new LoggerConfiguration().MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/AutoNuoma.txt", rollingInterval: RollingInterval.Day).CreateLogger();
Log.Logger = log;

app.Run();
