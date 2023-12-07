
using System.Data;
using Npgsql;

namespace api;

public interface IRepository
{
    Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> action);
    Task WithConnection(Func<IDbConnection, Task> action);

}

public class Repository : IRepository
{
    private readonly ConnectionString _connectionString;

    public Repository(ConnectionString connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> action)
    {
        using var connection = new NpgsqlConnection(_connectionString.Value);
        await connection.OpenAsync();
        return await action(connection);
    }

    public async Task WithConnection(Func<IDbConnection, Task> action)
    {
        using var connection = new NpgsqlConnection(_connectionString.Value);
        await connection.OpenAsync();
        await action(connection);
    }
}


