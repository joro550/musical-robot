using Dapper;
using Npgsql;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class CatController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IRepository _repository;

    public CatController(ILogger<WeatherForecastController> logger, IRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet()]
    public async Task<List<Cat>> Get()
    {
        var cats = await _repository.WithConnection(async (con) =>
        {
            return await con.QueryAsync<Cat>("SELECT * FROM cat");
        });

        return cats.ToList();
    }

    [HttpPost]
    public async Task<List<Cat>> Create(CatRequest request)
    {
        await _repository.WithConnection(async (con) =>
        {
            await con.ExecuteAsync("INSERT INTO cat (name) VALUES (@name)", new
            {
                name = request.Name
            });
        });

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
