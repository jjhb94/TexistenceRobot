using System.Text.Json;
using Dapper;
using Npgsql;
using TexistenceRobot.Core.Models;
using Microsoft.Extensions.Configuration;

namespace TexistenceRobot.Infra.Repositories;

public class RobotCommandRepository
{
     // should I reduce to using minimal constructor?
    private readonly string _connectionString;

    public RobotCommandRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("Postgres");
    }

    public async Task AddCommand(Command command)
    {
        await using var _connection = new NpgsqlConnection(_connectionString);
        await _connection.ExecuteAsync(
            @"INSERT INTO commands (id, robot_id, command_type, params, user_id) 
              VALUES (@Id, @RobotId, @CommandType, @Params::jsonb, @UserId)",
            new { 
                Id = Guid.NewGuid(),
                command.RobotId,
                command.CommantType,
                Params = JsonSerializer.Serialize(command.Parameters),
                command.UserId
            });
    }
}