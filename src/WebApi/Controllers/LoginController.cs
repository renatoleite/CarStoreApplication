using Application.UseCases.InsertUser;
using Microsoft.AspNetCore.Mvc;
using WebApi.Mappers;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IInsertUserUseCase _insertUserUseCase;

        public LoginController(
            ILogger<LoginController> logger,
            IInsertUserUseCase insertUserUseCase)
        {
            _logger = logger;
            _insertUserUseCase = insertUserUseCase;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] InsertUserInput input, CancellationToken cancellationToken)
        {
            try
            {
                var output = await _insertUserUseCase.ExecuteAsync(input.MapToApplication(), cancellationToken);

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