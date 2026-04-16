using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace AquarelaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExampleController : ControllerBase
{
    private readonly string? _mySqlConnectionString;

    public ExampleController(IConfiguration configuration)
    {
#if DEBUG
        _mySqlConnectionString = configuration.GetConnectionString("MYSQLCONNSTR_localdb");
#else
        _mySqlConnectionString = Environment.GetEnvironmentVariable("MYSQLCONNSTR_localdb");
#endif

    }

    [HttpGet("hello")]
    public IActionResult GetHello()
    {
        return Ok(new { message = "Hello from AquarelaApi" });
    }

    [HttpGet("values")]
    public IActionResult GetValues()
    {
        var values = new[] { "red", "blue", "green" };
        return Ok(values);
    }

    [HttpGet("mysql-test")]
    public async Task<IActionResult> TestMySqlConnection()
    {

        if (string.IsNullOrEmpty(_mySqlConnectionString))
            return StatusCode(500, new { success = false, message = "Connection string 'MYSQLCONNSTR_localdb' not found." });

        try
        {
            await using var connection = new MySqlConnection(_mySqlConnectionString);
            await connection.OpenAsync();
            await using var command = connection.CreateCommand();
            command.CommandText = "SELECT VERSION();";
            var version = await command.ExecuteScalarAsync();
            return Ok(new { success = true, server = connection.DataSource, database = connection.Database, version });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, message = ex.Message });
        }
    }
}
