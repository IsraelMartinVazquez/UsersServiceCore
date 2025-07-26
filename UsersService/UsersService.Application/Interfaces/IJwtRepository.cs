using UsersService.Domain.Entities;

namespace UsersService.Application.Interfaces
{
    /// <summary>
    /// Define las operaciones relacionadas con la generación de tokens JWT.
    /// </summary>
    public interface IJwtRepository
    {
        /// <summary>
        /// Genera un token JWT para el usuario autenticado.
        /// </summary>
        /// <param name="user">Entidad de usuario para la cual se generará el token.</param>
        /// <returns>Cadena que representa el token JWT generado.</returns>
        string GenerateToken(User user);
    }
}