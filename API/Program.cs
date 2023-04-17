using System.Text.Json;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://127.0.0.1:0");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Start();

var server = app.Services.GetService(typeof(IServer)) as IServer;
var port = (server?.Features.Get<IServerAddressesFeature>()?.Addresses?.FirstOrDefault() ?? throw new NullReferenceException("There is no address available")).Split(":").Last();
object portObject = new { PortNumber = port };

var portSerializerOptions = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
};
var json = JsonSerializer.Serialize(portObject, portSerializerOptions);
await File.WriteAllTextAsync("port.json", json);

app.WaitForShutdown();