using TexistenceRobot.Core.Models;
using TexistenceRobot.Core.Services;

namespace TexistenceRobot.Infra.Services;

public class CommandService : ICommandService
{
    public Task EnqueueCommand(Command command)
    {
        return Task.CompletedTask;
    }

    public Task UpdateCommand(string robotId, string commandId, CommandUpdate update)
    {
        return Task.CompletedTask;
    }
}