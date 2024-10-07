using CarDealerDemo.Models;

namespace CarDealerDemo.Services;
public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;

    public CarService(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task<bool> AddCarAsync(Car car)
    {
        return await _carRepository.AddCarAsync(car);
    }

    public async Task<bool> DeleteCarAsync(int id)
    {
        return await _carRepository.DeleteCarAsync(id);
    }

    public async Task<Car> GetCarByIdAsync(int id)
    {
        return await _carRepository.GetCarByIdAsync(id);
    }

    public async Task<bool> UpdateCarAsync(Car car)
    {
        return await _carRepository.UpdateCarAsync(car);
    }

    public async Task<bool> IsCarExistAsync(Car car)
    {
        return await _carRepository.IsCarExistAsync(car);
    }

    public async Task<IEnumerable<Car>> GetAllCarsAsync(int dealerId)
    {
        return await _carRepository.GetAllCarsAsync(dealerId);
    }

    public async Task<Car> GetStockLevelByIdAsync(int carId)
    {
        return await _carRepository.GetStockLevelByIdAsync(carId);
    }

    public async Task<IEnumerable<Car>> GetCarByMakeAndModel(string make, string model, int dealerId)
    {
        return await _carRepository.GetCarByMakeAndModel(make, model, dealerId);
    }
}
