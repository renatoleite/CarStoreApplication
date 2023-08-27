﻿using Application.UseCases.ChangeUserPermission;
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
        private readonly IChangeUserPermissionUseCase _changeUserPermissionUseCase;

        public LoginController(
            ILogger<LoginController> logger,
            IInsertUserUseCase insertUserUseCase,
            IChangeUserPermissionUseCase changeUserPermissionUseCase)
        {
            _logger = logger;
            _insertUserUseCase = insertUserUseCase;
            _changeUserPermissionUseCase = changeUserPermissionUseCase;
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

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, [FromBody] UpdatePermissionInput input, CancellationToken cancellationToken)
        {
            try
            {
                var output = await _changeUserPermissionUseCase.ExecuteAsync(input.MapToApplication(id), cancellationToken);

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