using CarDealerDemo.Models;

namespace CarDealerDemo.Services;

public interface ICarService
{
    Task<IEnumerable<Car>> GetAllCarsAsync(int dealerId);
    Task<IEnumerable<Car>> GetCarByMakeAndModel(string make, string model, int dealerId);

    Task<Car> GetStockLevelByIdAsync(int carId);
    Task<bool> AddCarAsync(Car car);
    Task<bool> IsCarExistAsync(Car car);
    Task<bool> UpdateCarAsync(Car car);
    Task<bool> DeleteCarAsync(int id);
    Task<Car> GetCarByIdAsync(int id);
}
