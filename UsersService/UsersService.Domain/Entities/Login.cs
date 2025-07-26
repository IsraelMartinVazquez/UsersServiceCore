namespace UsersService.Domain.Entities
{
    /// <summary>
    /// Representa la entidad de login con credenciales de usuario.
    /// </summary>
    public class Login
    {
        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Hash de la contraseña del usuario.
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;
    }
}