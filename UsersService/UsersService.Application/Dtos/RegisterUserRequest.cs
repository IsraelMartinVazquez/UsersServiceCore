namespace UsersService.Application.Dtos
{
    /// <summary>
    /// Modelo para la solicitud de registro de un nuevo usuario.
    /// </summary>
    public class RegisterUserRequest
    {
        /// <summary>
        /// Nombre(s) del usuario.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Apellido(s) del usuario.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Número telefónico del usuario.
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña del usuario en formato hash.
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;
    }
}