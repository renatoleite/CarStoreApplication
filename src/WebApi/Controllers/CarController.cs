using Application.UseCases.InsertCar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Mappers;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ILogger<CarController> _logger;
        private readonly IInsertCarUseCase _insertCarUseCase;

        public CarController(
            ILogger<CarController> logger,
            IInsertCarUseCase insertCarUseCase)
        {
            _logger = logger;
            _insertCarUseCase = insertCarUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InsertCarInput input, CancellationToken cancellationToken)
        {
            try
            {
                var output = await _insertCarUseCase.ExecuteAsync(input.MapToApplication(), cancellationToken);

                if (output.IsValid)
                    return Ok(output.Result);

                return BadRequest(output);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return BadRequest();
            }
        }
    }
}