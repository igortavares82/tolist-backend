using Microsoft.AspNetCore.Mvc;
using Standard.ToList.Api.ActionFilters;
using Standard.ToList.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

var cfgBuilder = new ConfigurationBuilder().SetBasePath(builder.Environment.ContentRootPath)
                                           .AddJsonFile("appsettings.json", false, true)
                                           .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", false, true)
                                           .AddEnvironmentVariables()
                                           .Build();

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureDependencies(cfgBuilder);
builder.Services.ConfigureOptions(cfgBuilder);
builder.Services.ConfigureWorker();
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressInferBindingSourcesForParameters = true);
builder.Services.AddMvc(options => options.Filters.Add(new RequestActionFilter()));
builder.Services.ConfigureAuth(cfgBuilder);
builder.Logging.ClearProviders();
builder.Logging.AddToLystLogger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseCors(it => it.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

app.Run();
