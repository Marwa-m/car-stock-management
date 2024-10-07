using CarDealerDemo.Factory;
using CarDealerDemo.Models;
using Dapper;

public class CarRepository : ICarRepository
{
    private readonly ISqliteConnectionFactory _connectionFactory;

    public CarRepository(ISqliteConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Car>> GetAllCarsAsync(int dealerId)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Car>("SELECT * FROM Cars WHERE DealerId=@DealerId", new {@DealerId= dealerId});
    }

    public async Task<Car> GetCarByIdAsync(int carId)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Car>("SELECT * FROM Cars WHERE ID = @CarId", new { CarId = carId });
    }

    public async Task<Car> GetStockLevelByIdAsync(int carId)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Car>("SELECT StockLevel,DealerID FROM Cars WHERE ID = @CarId", new { CarId = carId });
    }
    public async Task<bool> AddCarAsync(Car car)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "INSERT INTO Cars (Make, Model, Year,StockLevel,DealerId) VALUES (@Make, @Model, @Year,@StockLevel,@DealerId)";
        int rowsAffected = await connection.ExecuteAsync(sql, car);
        return rowsAffected > 0;
    }

    public async Task<bool> UpdateCarAsync(Car car)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "UPDATE Cars SET StockLevel = @StockLevel WHERE ID = @ID";
      int rowAffected=  await connection.ExecuteAsync(sql, car);
        return rowAffected > 0;
    }

    public async Task<bool> DeleteCarAsync(int carId)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "DELETE FROM Cars WHERE ID = @CarId";
        int rowsAffected = await connection.ExecuteAsync(sql, new { CarId = carId });
   return rowsAffected > 0;
    }

    public async Task<bool> IsCarExistAsync(Car car)
    {
        using var connection = _connectionFactory.CreateConnection();
        var result= await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Cars WHERE LOWER(Make) = LOWER(@Make) AND LOWER(Model) = LOWER(@Model) and Year=@Year and DealerId=@DealerId",
            new { car.Make,car.Model,car.Year,car.DealerId});
        return result > 0;
    }

    public async Task<IEnumerable<Car>> GetCarByMakeAndModel(string make, string model,int dealerId)
    {
        using var connection = _connectionFactory.CreateConnection();
        var result= await connection.QueryAsync<Car>("SELECT * FROM Cars WHERE Make LIKE @make AND Model LIKE @model AND DealerId=@dealerId",
           new { make = "%" + make + "%", model = "%" + model + "%", dealerId });
        return result;
    }
}
