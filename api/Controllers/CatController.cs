using Dapper;
using Npgsql;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class CatController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly ConnectionString connectionString;

    public CatController(ILogger<WeatherForecastController> logger, ConnectionString connectionString)
    {
        _logger = logger;
        this.connectionString = connectionString;
    }

    [HttpGet()]
    public async Task<List<Cat>> Get()
    {
        using var connection = new NpgsqlConnection(connectionString.Value);
        await connection.OpenAsync();

        var cats = await connection.QueryAsync<Cat>("SELECT * FROM \"Cat\"");
        return cats.ToList();
    }

    [HttpPost]
    public async Task<List<Cat>> Create(CatRequest request)
    {
        using var connection = new NpgsqlConnection(connectionString.Value);
        await connection.OpenAsync();

        var queryArgs = new
        {
            name = request.Name
        };

        await connection.ExecuteAsync("INSERT INTO \"Cat\" (\"Name\") VALUES (@name)", queryArgs);
        return await Get();
    }

}

public class Cat
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class CatRequest
{
    public string Name { get; set; } = string.Empty;
}
