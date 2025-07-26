using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersService.Application.Dtos;
using UsersService.Application.Interfaces;

namespace UsersService.API.Controllers;

/// <summary>
/// Controlador para operaciones relacionadas con usuarios.
/// </summary>
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILoginService _loginService;

    /// <summary>
    /// Constructor del controlador de usuarios.
    /// </summary>
    /// <param name="userService">Servicio para operaciones de usuario.</param>
    /// <param name="loginService">Servicio para autenticación de usuarios.</param>
    public UsersController(IUserService userService, ILoginService loginService)
    {
        _userService = userService;
        _loginService = loginService;
    }

    /// <summary>
    /// Registra un nuevo usuario en el sistema.
    /// </summary>
    /// <param name="model">Datos del usuario a registrar.</param>
    /// <returns>Respuesta con el estado de la operación.</returns>
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

    /// <summary>
    /// Inicia sesión con las credenciales del usuario.
    /// </summary>
    /// <param name="model">Credenciales de inicio de sesión.</param>
    /// <returns>Token JWT y datos del usuario autenticado.</returns>
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

    /// <summary>
    /// Obtiene las cuentas asociadas al usuario autenticado.
    /// </summary>
    /// <param name="model">Datos de entrada para la búsqueda de cuentas.</param>
    /// <returns>Lista de cuentas del usuario.</returns>
    [HttpPost("GetUserAccountsAsync")]
    [Authorize]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK, "application/json")]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest, "application/json")]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError, "application/json")]
    public async Task<IActionResult> GetUserAccountsAsync([FromBody] AccountsRequest model)
    {
        BaseResponse response = new();
        try
        {
            // Simulación de respuesta: aquí debe ir la lógica para recuperar cuentas
            response.Success = true;
            response.StatusCode = 200;
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