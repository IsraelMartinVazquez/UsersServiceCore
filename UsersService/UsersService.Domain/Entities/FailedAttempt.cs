namespace UsersService.Domain.Entities
{
    /// <summary>
    /// Representa un intento fallido de autenticación de un usuario.
    /// </summary>
    public class FailedAttempt
    {
        /// <summary>
        /// Identificador del usuario que realizó el intento fallido.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Hash de la contraseña utilizada en el intento (no debe contener texto plano).
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;
    }
}