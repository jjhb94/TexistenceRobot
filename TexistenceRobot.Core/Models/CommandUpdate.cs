namespace TexistenceRobot.Core.Models;

public record CommandUpdate(
    Dictionary<string, object>? Parameters,
    string? Priority
);