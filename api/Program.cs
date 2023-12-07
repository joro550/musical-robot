using api;
using Dapper;
using Npgsql;

var connectionString = "User ID=postgres;Password=postgres123;Host=data;Port=5432;Database=musicalrobot;";
using (var connection = new NpgsqlConnection("User ID=postgres;Password=postgres123;Host=data;Port=5432;"))
{
    connection.Open();
    connection.Execute(@"SELECT 'CREATE DATABASE musicalrobot'
WHERE NOT EXISTS (SELECT FROM pg_database WHERE datname = 'musicalrobot')");
}


Migrator.Migrate(connectionString);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ConnectionString>(_ => new ConnectionString(connectionString));

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
