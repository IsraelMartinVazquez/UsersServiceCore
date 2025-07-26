using Microsoft.AspNetCore.Mvc;
using UsersService.Application.Dtos;
using UsersService.Application.Interfaces;

namespace UsersService.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILoginService _loginService;

    public UsersController(IUserService userService, ILoginService loginService)
    {
        _userService = userService;
        _loginService = loginService;
    }

    [HttpPost("RegisterUserAsync")]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK, "application/json")]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest, "application/json")]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError, "application/json")]
    public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserRequest model)
    {
        BaseResponse response = new();
        try
        {
            response = await _userService.RegisterUserAsync(model);
            return StatusCode(response.StatusCode, response);
        }
        catch (Exception ex)
        {
            response.Message = "Ocurrió un error inesperado en el servidor.";
            response.Errors?.Add(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    [HttpPost("LoginUserAsync")]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK, "application/json")]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest, "application/json")]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError, "application/json")]
    public async Task<IActionResult> LoginUserAsync([FromBody] LoginRequest model)
    {
        BaseResponse<LoginResponse> response = new();
        try
        {
            response = await _loginService.LoginUserAsync(model);
            return StatusCode(response.StatusCode, response);
        }
        catch (Exception ex)
        {
            response.Message = "Ocurrió un error inesperado en el servidor.";
            response.Errors?.Add(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}