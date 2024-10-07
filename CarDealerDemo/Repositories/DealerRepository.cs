using CarDealerDemo.Factory;
using CarDealerDemo.Models;
using Dapper;

public class DealerRepository : IDealerRepository
{
    #region Fields

    private readonly ISqliteConnectionFactory _connectionFactory;

    #endregion

    #region Ctor
    public DealerRepository(ISqliteConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    #endregion

    #region Methods
    public async Task<int> AddDealerAsync(Dealer dealer)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "INSERT INTO Dealers (Name, Email, PasswordHash,Token) VALUES (@Name, @Email, @PasswordHash,@Token)";
        return await connection.ExecuteAsync(sql, dealer);
    }
    public async Task<Dealer> GetDealerByEmailAsync(string email)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Dealer>("SELECT * FROM Dealers WHERE Email = @email", new { Email = email });

    }
    public async Task<bool> IsEmailOrNameExistsAsync(string email, string name)
    {
        using var connection = _connectionFactory.CreateConnection();
        var exist = await connection.ExecuteScalarAsync<int>("SELECT exists(SELECT 1 FROM Dealers WHERE Email = @email OR Name=@name)", new { email = email, name = name });
        return exist == 1;

    }
    public async Task<Dealer> GetDealerAsync(string name)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Dealer>("SELECT * FROM Dealers WHERE Name = @name OR Email=@name", new { name });

    }

    public async Task<Dealer> FindDealerByIdAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Dealer>("SELECT * FROM Dealers WHERE ID = @id", new { id });

    }

    public async Task<bool> UpdateToken(int id, string token)
    {
        using var connection = _connectionFactory.CreateConnection();
        int rowsAffected = await connection.ExecuteAsync("UPDATE Dealers SET Token=@token WHERE ID = @id", new { token, id });

        return rowsAffected > 0;
    }

    #endregion
}
