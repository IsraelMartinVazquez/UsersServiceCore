namespace UsersService.Application.Dtos
{
    /// <summary>
    /// Respuesta base estándar con estado, mensaje, lista de errores y código de estado.
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// Indica si la operación fue exitosa.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Mensaje descriptivo o informativo sobre la operación.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Lista de errores relacionados con la operación.
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>();

        /// <summary>
        /// Código de estado HTTP o personalizado.
        /// </summary>
        public int StatusCode { get; set; }
    }

    /// <summary>
    /// Respuesta base genérica que incluye datos de tipo <typeparamref name="T"/>.
    /// </summary>
    public class BaseResponse<T> : BaseResponse
    {
        /// <summary>
        /// Datos retornados en la respuesta.
        /// </summary>
        public T? Data { get; set; }
    }
}