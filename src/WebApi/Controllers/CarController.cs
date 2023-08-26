using Application.UseCases.DeleteCar;
using Application.UseCases.DeleteCar.Commands;
using Application.UseCases.InsertCar;
using Application.UseCases.SearchCar;
using Application.UseCases.SearchCar.Commands;
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
        private readonly ISearchCarUseCase _searchCarUseCase;
        private readonly IDeleteCarUseCase _deleteCarUseCase;

        public CarController(
            ILogger<CarController> logger,
            IInsertCarUseCase insertCarUseCase,
            ISearchCarUseCase searchCarUseCase,
            IDeleteCarUseCase deleteCarUseCase)
        {
            _logger = logger;
            _insertCarUseCase = insertCarUseCase;
            _searchCarUseCase = searchCarUseCase;
            _deleteCarUseCase = deleteCarUseCase;
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

        [HttpGet("{term}")]
        public async Task<IActionResult> Get(string term, CancellationToken cancellationToken)
        {
            try
            {
                var command = new SearchCarCommand() { Term = term };
                var output = await _searchCarUseCase.ExecuteAsync(command, cancellationToken);

                if (output.IsValid)
                    return Ok(output.Result);

                return BadRequest(output);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                return BadRequest();
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                var command = new DeleteCarCommand() { Id = id };
                var output = await _deleteCarUseCase.ExecuteAsync(command, cancellationToken);

                if (output.IsValid)
                    return Ok(output.Result);

                return BadRequest(output);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                return BadRequest();
            }
        }
    }
}