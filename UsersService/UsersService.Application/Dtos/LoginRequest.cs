namespace UsersService.Application.Dtos
{
    /// <summary>
    /// DTO para la solicitud de login, que contiene el correo electrónico y la contraseña en hash.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña del usuario en formato hash.
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;
    }
}