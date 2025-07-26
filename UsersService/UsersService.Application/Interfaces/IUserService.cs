using UsersService.Application.Dtos;

namespace UsersService.Application.Interfaces
{
    /// <summary>
    /// Servicio para la lógica de negocio relacionada con usuarios.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Registra un nuevo usuario a partir de los datos proporcionados.
        /// </summary>
        /// <param name="model">Modelo con los datos para registrar el usuario.</param>
        /// <returns>Respuesta con el resultado de la operación.</returns>
        Task<BaseResponse> RegisterUserAsync(RegisterUserRequest model);
    }
}