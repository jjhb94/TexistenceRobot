using System.Text;
using Microsoft.Extensions.Options;
using MQTTnet.AspNetCore;
using TexistenceRobot.Core.Models;
using TexistenceRobot.Infra.Services;
using TexistenceRobot.Core.Services;
using TexistenceRobot.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddConnections();
// TODO: use extensions for the Dependency Injection
// builder.Services.AddHostedMqttServer(mqtt => mqtt.WithDefaultEndpoint());
builder.Services
    .AddHostedMqttServer(mqttServer => mqttServer.WithDefaultEndpoint())  // Adds the MQTT server as a hosted service
    .AddMqttConnectionHandler();

// TODO: add status and command services
builder.Services.AddSingleton<IMqttRobotService, MqttRobotService>();
builder.Services.AddScoped<RobotCommandRepository>();
builder.Services.AddScoped<ICommandService, CommandService>();
builder.Services.AddScoped<IStatusService, StatusService>();

// TODO: add JWT auth
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationSchem)
//     .AddJwtBearer(options => 
//     {
//         Options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuerSigningKey = true,
//             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
//             ValidateIssuer = false,
//             ValidateAudience = false
//         };
//     });
 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseRouting();
// MQTT endpoint for real time controls
app.MapMqtt("/mqtt"); // this is why we added MQTTnet.AspNetCore

// configure min API endpoints
// TODO: use extensions to make cleaner
app.MapPost("/robots/{id}/commands", async (Command command, ICommandService service) 
    => await service.EnqueueCommand(command)); 
// TODO: add more options for auth on endpoints 
app.MapGet("/robots/{id}/status", (string id, IStatusService service)
    => service.GetStatus(id));

app.MapPut("/robots/{id}/commands/{cmdId}", async (string id, string cmdId, CommandUpdate update, ICommandService service)
    => await service.UpdateCommand(id, cmdId, update));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.Run();