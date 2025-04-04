namespace TexistenceRobot.Core.Models;

public record Command(
    string RobotId,
    string CommantType,
    Dictionary<string, object> Parameters,
    string UserId,
    DateTime TimeStamp = default,
    string Priority = "normal",
    DateTime? Expiry = null
);