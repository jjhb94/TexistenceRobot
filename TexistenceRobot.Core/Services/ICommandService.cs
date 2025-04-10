using TexistenceRobot.Core.Models;

namespace TexistenceRobot.Core.Services;

public interface ICommandService
{
    Task EnqueueCommand(Command command);
    Task UpdateCommand(string robotId, string commandId, CommandUpdate update);
}
