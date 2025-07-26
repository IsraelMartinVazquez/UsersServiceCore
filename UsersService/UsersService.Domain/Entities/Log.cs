namespace UsersService.Domain.Entities
{
    /// <summary>
    /// Representa un registro de log para solicitudes y respuestas HTTP.
    /// </summary>
    public class Log
    {
        /// <summary>
        /// Dominio de la solicitud HTTP.
        /// </summary>
        public string Domain { get; set; } = string.Empty;

        /// <summary>
        /// Nombre del host o servidor donde se ejecuta la aplicación.
        /// </summary>
        public string Host { get; set; } = string.Empty;

        /// <summary>
        /// Método HTTP de la solicitud (GET, POST, etc.).
        /// </summary>
        public string Method { get; set; } = string.Empty;

        /// <summary>
        /// Ruta o endpoint solicitado.
        /// </summary>
        public string Route { get; set; } = string.Empty;

        /// <summary>
        /// Encabezados HTTP de la solicitud en formato JSON.
        /// </summary>
        public string JsonHeaders { get; set; } = string.Empty;

        /// <summary>
        /// Cuerpo de la solicitud HTTP en formato JSON.
        /// </summary>
        public string JsonRequest { get; set; } = string.Empty;

        /// <summary>
        /// Cuerpo de la respuesta HTTP en formato JSON.
        /// </summary>
        public string JsonResponse { get; set; } = string.Empty;
    }
}