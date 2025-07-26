using UsersService.Application.Dtos;
using UsersService.Domain.Entities;

namespace UsersService.Application.Interfaces
{
    /// <summary>
    /// Repositorio para operaciones relacionadas con el login y gestión de intentos fallidos.
    /// </summary>
    public interface ILoginRepository
    {
        /// <summary>
        /// Obtiene un usuario por su correo electrónico.
        /// </summary>
        /// <param name="email">Correo electrónico del usuario.</param>
        /// <returns>Respuesta con el usuario encontrado o error.</returns>
        Task<BaseDBResponse<User>> GetUserByEmailAsync(string email);

        /// <summary>
        /// Obtiene la cantidad de intentos fallidos de login de un usuario.
        /// </summary>
        /// <param name="userId">Identificador del usuario.</param>
        /// <returns>Respuesta con el número de intentos fallidos.</returns>
        Task<BaseDBResponse<int>> GetFailedLoginAttemptsByUser(int userId);

        /// <summary>
        /// Inserta un nuevo registro de intento fallido de login.
        /// </summary>
        /// <param name="model">Entidad que representa el intento fallido.</param>
        /// <returns>Respuesta con el identificador del intento insertado o estado de la operación.</returns>
        Task<BaseDBResponse<int>> InsertFailedLoginAttemptAsync(FailedAttempt model);
    }
}