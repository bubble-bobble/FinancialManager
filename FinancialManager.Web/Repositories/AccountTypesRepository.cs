using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using FinancialManager.Web.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace FinancialManager.Web.Repositories;

public interface IAccountTypesRepository
{
    Task InsertAccountType(AccountType accountType);

    Task<bool> SelectIfExistAccountType(string name, int userId);

    Task<IEnumerable<AccountType>> SelectAccountTypes(int userId);
    
    Task<AccountType> SelectAccountType(int id, int userId);

    Task UpdateAccountType(AccountType accountType);
}

public class AccountTypesRepository(IConfiguration configuration) : IAccountTypesRepository
{
    private readonly string _connectionString = configuration.GetConnectionString("FinancialManager");

    public async Task InsertAccountType(AccountType accountType)
    {
        await using var connection = new SqlConnection(_connectionString);
        const string query = "INSERT INTO AccountTypes (Name, Sequence, UserId) VALUES (@Name, @Sequence, @UserId);";
        await connection.ExecuteAsync(query, accountType);
    }

    public async Task<bool> SelectIfExistAccountType(string name, int userId)
    {
        await using var connection = new SqlConnection(_connectionString);
        const string query = "SELECT * FROM AccountTypes WHERE Name = @Name AND UserId = @UserId;";
        return await connection.ExecuteScalarAsync<bool>(query, new { Name = name, UserId = userId });
    }

    public async Task<IEnumerable<AccountType>> SelectAccountTypes(int userId)
    {
        await using var connection = new SqlConnection(_connectionString);
        const string query = "SELECT * FROM AccountTypes WHERE UserId = @UserId ORDER BY Sequence;";
        return await connection.QueryAsync<AccountType>(query, new { UserId = userId });
    }

    public async Task<AccountType> SelectAccountType(int id, int userId)
    {
        await using var connection = new SqlConnection(_connectionString);
        const string query = "SELECT Id, Name, Sequence FROM AccountTypes WHERE Id = @Id AND UserId = @UserId;";
        return await connection.QueryFirstOrDefaultAsync<AccountType>(query, new { Id = id, UserId = userId });
    }

    public async Task UpdateAccountType(AccountType accountType)
    {
        await using var connection = new SqlConnection(_connectionString);
        const string query = "UPDATE AccountTypes SET Name = @Name WHERE Id = @Id;";
        await connection.ExecuteAsync(query, accountType);
    }
}