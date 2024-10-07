using CarDealerDemo.Attributes;
using CarDealerDemo.Dto;
using CarDealerDemo.Models;
using CarDealerDemo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarDealerDemo.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        #region Fields

        private readonly ICarService _carService;
        private readonly IDealerService _dealerService;
        #endregion

        #region Ctor
        public CarsController(ICarService carService,
               IDealerService dealerService)
        {
            _carService = carService;
            _dealerService = dealerService;
        }

        #endregion

        #region Utilities
        protected int GetDealerId()
        {
            var dealerId = _dealerService.GetDealerIdByClaim(User);
            if (dealerId <= 0)
            {
                throw new UnauthorizedAccessException("Invalid dealer ID from JWT.");
            }
            return dealerId;
        }

        #endregion

        #region EndPoints

        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            int dealerId = GetDealerId();

            var cars = await _carService.GetAllCarsAsync(dealerId);

            return Ok(cars);
        }

        [HttpGet("search")]
        [ValidateModel]
        public async Task<IActionResult> SearchCarByMakeAndModel([FromQuery] SearchCarByMakeAdModelDto dto)
        {
            int dealerId = GetDealerId();

            var cars = await _carService.GetCarByMakeAndModel(dto.Make, dto.Model, dealerId);

            if (cars == null || !cars.Any())
                return Ok(Array.Empty<Car>());

            return Ok(cars);
        }

        [HttpGet("get-stock-level/{id}")]
        [ValidateModel]
        public async Task<IActionResult> GetStockLevelForCarById(int id)
        {
            int dealerId = GetDealerId();

            var car = await _carService.GetStockLevelByIdAsync(id);
            if (car == null || car.DealerId != dealerId)
                return BadRequest("Car is not exist Or You don't have permission to change the stock");


            return Ok($"The Stock level for the car is {car.StockLevel} ");
        }


        [HttpPost("add")]
        [ValidateModel]
        public async Task<IActionResult> AddCar([FromBody] AddCarDto addCarDto)
        {
            int dealerId = GetDealerId();

            var car = new Car
            {
                Make = addCarDto.Make,
                Model = addCarDto.Model,
                Year = addCarDto.Year,
                StockLevel = addCarDto.StockLevel,
                DealerId = dealerId,
            };
            bool isCarExist = await _carService.IsCarExistAsync(car);
            if (isCarExist)
            {
                return BadRequest("Car already exists");
            }
            bool isCreated = await _carService.AddCarAsync(car);
            if (!isCreated)
                return BadRequest("Can not add the car");
            return Ok("Car is added successfully");
        }

        [HttpPut("update-stock-level")]
        [ValidateModel]
        public async Task<IActionResult> UpdateStockLevelCar([FromQuery] UpdateStockLevelDto updateStockLevelDto)
        {
            int dealerId = GetDealerId();

            var car = await _carService.GetCarByIdAsync(updateStockLevelDto.ID);
            if (car == null || car.DealerId != dealerId)
                return BadRequest("Car is not exist Or You don't have permission to access");

            car.StockLevel = updateStockLevelDto.StockLevel;
            bool isUpdated = await _carService.UpdateCarAsync(car);
            if (!isUpdated)
                return BadRequest("Can not update the stock");
            return Ok("Stock updated successfully");
        }

        [HttpDelete("{id}")]
        [ValidateModel]
        public async Task<IActionResult> DeleteCar(int id)
        {
            int dealerId = GetDealerId();

            var car = await _carService.GetCarByIdAsync(id);
            if (car == null || car.DealerId != dealerId)
                return BadRequest("Car is not exist");

            bool isDeleted = await _carService.DeleteCarAsync(id);
            if (!isDeleted)
                return BadRequest("Can not delete the car");
            return Ok("Car deleted successfully");
        }


        #endregion
    }

}
