using CarDealerDemo.Attributes;
using CarDealerDemo.Dto;
using CarDealerDemo.Helper;
using CarDealerDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarDealerDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealerController : ControllerBase
    {
        #region Fields

        private readonly IDealerService _dealerService;
        private readonly IJWTService _jwtService;

        #endregion

        #region Ctor
        public DealerController(IDealerService dealerService,
            IJWTService jwtService)
        {
            _dealerService = dealerService;
            _jwtService = jwtService;
        }
        #endregion

        #region Utilities
        private async Task<IActionResult> GenerateAndSaveToken(int dealerId)
        {
            var token = await _jwtService.GenerateTokenAsync(dealerId);
            if (token == null)
            {
                return Unauthorized(Result<string>.Failure("Invalid Attempt."));
            }

            var isUpdated = await _dealerService.UpdateToken(dealerId, token);
            if (!isUpdated)
            {
                return BadRequest(Result<string>.Failure("Cannot generate token"));
            }

            return Ok(Result<string>.Success(data: $"Token={token}", message: "Success"));
        }
        #endregion
        #region EndPoints

        [HttpPost]
        [Route("register")]
        [ValidateModel]
        public async Task<IActionResult> RegisterAsync(RegisterDealerDto dto)
        {

            var result = await _dealerService.AddDealerAsync(dto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return await GenerateAndSaveToken(result.Data);

        }

        [HttpPost]
        [Route("signin")]
        [ValidateModel]
        public async Task<IActionResult> SignInAsync(LoginDealerDto dto)
        {

            var dealer = await _dealerService.GetDealerAsync(dto.Name);

            if (dealer is null)
            {
                return Unauthorized(Result.Failure("Invalid name or password..."));
            }

            bool verifyPassword = _dealerService.IsPasswordValid(dto.Password, dealer.PasswordHash);
            if (!verifyPassword)
            {
                return Unauthorized(Result.Failure("Invalid name or password..."));
            }

            return await GenerateAndSaveToken(dealer.ID);
        }

        #endregion



    }
}
