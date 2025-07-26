namespace UsersService.Application.Dtos
{
    /// <summary>
    /// Representa la configuración necesaria para generar y validar tokens JWT.
    /// </summary>
    public class JwtOptions
    {
        /// <summary>
        /// Clave secreta utilizada para firmar el token JWT.
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Emisor del token, normalmente el nombre o dominio de la aplicación.
        /// </summary>
        public string Issuer { get; set; } = string.Empty;

        /// <summary>
        /// Audiencia permitida que puede consumir el token.
        /// </summary>
        public string Audience { get; set; } = string.Empty;

        /// <summary>
        /// Tiempo de expiración del token en minutos.
        /// </summary>
        public int ExpiresInMinutes { get; set; }
    }
}