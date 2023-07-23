using Microsoft.Extensions.DependencyInjection;
using Standard.ToList.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

var cfgBuilder = new ConfigurationBuilder().SetBasePath(builder.Environment.ContentRootPath)
                                           .AddJsonFile("appsettings.json", true)
                                           .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true)
                                           .AddEnvironmentVariables()
                                           .Build();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureDependencies(cfgBuilder);
builder.Services.ConfigureOptions(cfgBuilder);
builder.Services.ConfigureWorker();

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
