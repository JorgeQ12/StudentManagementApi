using Application.Interfaces.IRepository.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RepoDb;
using System.Data;

namespace Infrastructure.Repository.Common;

public class GenericRepository : IGenericRepository
{
    private readonly IConfiguration _config;
    private readonly string _databaseConnectionString;

    public GenericRepository(IConfiguration configuration)
    {
        _config = configuration;
        _databaseConnectionString = _config.GetConnectionString("DatabaseConnectionString") ?? throw new Exception("Connection string error");
    }

    public async Task<IEnumerable<T>> GetProcedureAsync<T>(string procedureName, object parameters, int? commandTimeout = null) where T : class
    {
        try
        {
            using var connection = new SqlConnection(_databaseConnectionString);
            await connection.OpenAsync();
            return await connection.ExecuteQueryAsync<T>(procedureName, parameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching data: {ex.Message}", ex);
        }
    }

    public async Task<T?> GetProcedureSingleAsync<T>(string procedureName, object parameters, int? commandTimeout = null) where T : class
    {
        try
        {
            using var connection = new SqlConnection(_databaseConnectionString);
            await connection.OpenAsync();

            return (await connection.ExecuteQueryAsync<T>(procedureName, parameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout)).FirstOrDefault();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching data: {ex.Message}", ex);
        }
    }

    public async Task ExecuteProcedureAsync(string procedureName, object parameters, int? commandTimeout = null)
    {
        try
        {
            using var connection = new SqlConnection(_databaseConnectionString);
            await connection.OpenAsync();
            await connection.ExecuteQueryAsync(procedureName, parameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching data: {ex.Message}", ex);
        }
    }
}
