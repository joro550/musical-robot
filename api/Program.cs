using api;
using Dapper;
using Npgsql;

using (var connection = new NpgsqlConnection("User ID=postgres;Password=postgres123;Host=data;Port=5432;"))
{
    connection.Open();

    var databaseNames = await connection.QueryAsync<string>("SELECT datname From pg_database");
    if (!databaseNames.Contains("musicalrobot"))
    {
        await connection.ExecuteAsync("CREATE DATABASE musicalrobot");

    }
}


var connectionString = "User ID=postgres;Password=postgres123;Host=data;Port=5432;Database=musicalrobot;";
Migrator.Migrate(connectionString);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ConnectionString>(_ => new ConnectionString(connectionString));
builder.Services.AddSingleton<IRepository, Repository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
