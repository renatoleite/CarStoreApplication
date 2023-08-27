using Application.UseCases.DeleteCar;
using Application.UseCases.DeleteCar.Commands;
using Application.UseCases.InsertCar;
using Application.UseCases.SearchCar;
using Application.UseCases.SearchCar.Commands;
using Application.UseCases.UpdateCar;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Consts;
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
        private readonly IUpdateCarUseCase _updateCarUseCase;

        public CarController(
            ILogger<CarController> logger,
            IInsertCarUseCase insertCarUseCase,
            ISearchCarUseCase searchCarUseCase,
            IDeleteCarUseCase deleteCarUseCase,
            IUpdateCarUseCase updateCarUseCase)
        {
            _logger = logger;
            _insertCarUseCase = insertCarUseCase;
            _searchCarUseCase = searchCarUseCase;
            _deleteCarUseCase = deleteCarUseCase;
            _updateCarUseCase = updateCarUseCase;
        }

        [HttpPost]
        [Authorize(Roles = Roles.Create, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Post([FromBody] InsertCarInput input, CancellationToken cancellationToken)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userId = identity?.FindFirst(ClaimTypes.NameIdentifier);
                var userName = identity?.FindFirst(ClaimTypes.Name);

                if (userId == null || userName == null)
                    return BadRequest();

                var output = await _insertCarUseCase.ExecuteAsync(input.MapToApplication(Convert.ToInt32(userId.Value), userName.Value), cancellationToken);

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
        [Authorize(Roles = Roles.Read, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(Roles = Roles.Delete, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        
        [HttpPatch("{id:int}")]
        [Authorize(Roles = Roles.Update, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Patch(int id, [FromBody] UpdateCarInput input, CancellationToken cancellationToken)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userId = identity?.FindFirst(ClaimTypes.NameIdentifier);

                if (userId == null)
                    return BadRequest();

                var output = await _updateCarUseCase.ExecuteAsync(input.MapToApplication(id, Convert.ToInt32(userId.Value)), cancellationToken);

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