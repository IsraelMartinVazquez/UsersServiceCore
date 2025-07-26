namespace UsersService.Domain.Entities
{
    /// <summary>
    /// Representa un usuario en el sistema con sus datos básicos.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Identificador único del usuario.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Nombre(s) del usuario.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Apellido(s) del usuario.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Dirección de correo electrónico del usuario.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Número telefónico del usuario.
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Hash de la contraseña del usuario (no debe incluir la contraseña en texto plano).
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;
    }
}