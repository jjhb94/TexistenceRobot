using System.Text;
using TexistenceRobot.Core.Services;
using MQTTnet;
using MQTTnet.Server;
using MQTTnet.Protocol;

namespace TexistenceRobot.Infra.Services;

public class MqttRobotService : IMqttRobotService, IDisposable
{
    private MqttServer? _mqttServer;

    public async Task StartMqttBrokerAsync()
    {
        var options = new MqttServerOptionsBuilder()
            .WithDefaultEndpoint()  // Port 1883
            .Build();

        _mqttServer = new MqttFactory().CreateMqttServer(options);
        
        // Subscribe to intercepted messages
        _mqttServer.InterceptingPublishAsync += args =>
        {
            if (args.ApplicationMessage.Topic.StartsWith("robots/") && 
                args.ApplicationMessage.Topic.EndsWith("/commands"))
            {
                var payload = Encoding.UTF8.GetString(args.ApplicationMessage.Payload);
                // Process command (e.g., "move_forward")
            }
            return Task.CompletedTask;
        };

        await _mqttServer.StartAsync();
    }

    public async Task PublishCommandAsync(string robotId, string command)
    {
        if (_mqttServer == null)
            throw new InvalidOperationException("MQTT server not started.");
        
        var mqttClient = new MqttFactory().CreateMqttClient();
        var message = new MqttApplicationMessageBuilder()
            .WithTopic($"robots/{robotId}/commands")
            .WithPayload(Encoding.UTF8.GetBytes(command))
            .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
            .Build();

        await mqttClient.PublishAsync(message);
    }

    public async Task StopMqttBrokerAsync()
    {
        if (_mqttServer != null)
        {
            await _mqttServer.StopAsync();
        }
    }

    public void Dispose()
    {
        _mqttServer?.Dispose();
        GC.SuppressFinalize(this);
    }
}