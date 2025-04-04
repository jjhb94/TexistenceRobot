using TexistenceRobot.Core.Services;

namespace TexistenceRobot.Infra.Services;

public class StatusService : IStatusService
{
    public object GetStatus(string robotId)
    {
        return new { History = "a history of commands this session", Position = "0,0"}; // not sure about this one? 
    }
}