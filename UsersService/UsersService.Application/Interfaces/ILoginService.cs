using UsersService.Application.Dtos;

namespace UsersService.Application.Interfaces
{
    /// <summary>
    /// Servicio para gestionar la lógica de autenticación de usuarios.
    /// </summary>
    public interface ILoginService
    {
        /// <summary>
        /// Realiza el proceso de autenticación de un usuario.
        /// </summary>
        /// <param name="model">Modelo con los datos de login (email y contraseña).</param>
        /// <returns>Respuesta con información del usuario autenticado o error.</returns>
        Task<BaseResponse<LoginResponse>> LoginUserAsync(LoginRequest model);
    }
}