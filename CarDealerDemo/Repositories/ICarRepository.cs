using CarDealerDemo.Models;

public interface ICarRepository
{
    Task<IEnumerable<Car>> GetAllCarsAsync(int dealerId);
    Task<Car> GetCarByIdAsync(int carId);
    Task<Car> GetStockLevelByIdAsync(int carId);
    Task<bool> AddCarAsync(Car car);
    Task<bool> UpdateCarAsync(Car car);
    Task<bool> DeleteCarAsync(int carId);
    Task<bool> IsCarExistAsync(Car car);
    Task<IEnumerable<Car>> GetCarByMakeAndModel(string make, string model,int dealerId);
}
