namespace TexistenceRobot.Core.Services;

using System.Threading.Tasks;

public interface IMqttRobotService
{
    Task StartMqttBrokerAsync();
    Task PublishCommandAsync(string robotId, string command);
     Task StopMqttBrokerAsync();
}
