using UsersService.Application.Dtos;
using UsersService.Domain.Entities;

namespace UsersService.Application.Interfaces
{
    /// <summary>
    /// Repositorio para operaciones relacionadas con usuarios.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Registra un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="model">Entidad usuario con los datos a registrar.</param>
        /// <returns>Respuesta con el ID del usuario registrado o estado de la operación.</returns>
        Task<BaseDBResponse<int>> RegisterUserAsync(User model);

        /// <summary>
        /// Verifica si un correo electrónico ya existe en la base de datos.
        /// </summary>
        /// <param name="email">Correo electrónico a verificar.</param>
        /// <returns>Respuesta indicando si el correo existe o no.</returns>
        Task<BaseDBResponse<bool>> CheckEmailExistsAsync(string email);
    }
}