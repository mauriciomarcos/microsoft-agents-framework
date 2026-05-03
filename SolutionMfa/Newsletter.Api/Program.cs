using Newsletter.Api.Configs.DependencyInjections;
using Newsletter.Application;
using Newsletter.Application.Workers;
using Newsletter.Core.Configurations;
using Newsletter.Infra;

var builder = WebApplication.CreateBuilder(args);

Configuration.OpenAi.ApiKey = builder.Configuration.GetValue<string>("OpenAi:ApiKey") ??
    throw new Exception("Open AI Key not found in configuration.");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddAgents()
    .AddServicesApplication()
    .AddInfrastructure();

builder.Services.AddHostedService<NewslettersWorker>();

var app = builder.Build();
Configuration.RootPath = app.Environment.ContentRootPath;

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();