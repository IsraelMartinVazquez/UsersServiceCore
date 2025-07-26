namespace UsersService.Application.Dtos
{
    /// <summary>
    /// DTO para la respuesta de login con información básica del usuario.
    /// </summary>
    public class LoginResponse
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
        /// Número telefónico del usuario.
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Token.
        /// </summary>
        public string Token { get; set; } = string.Empty;
    }
}