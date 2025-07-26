namespace UsersService.Application.Dtos
{
    /// <summary>
    /// Respuesta base para operaciones en base de datos, con estado y mensaje.
    /// </summary>
    public class BaseDBResponse
    {
        /// <summary>
        /// Indica si la operación fue exitosa.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Mensaje asociado a la respuesta.
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }

    /// <summary>
    /// Respuesta base genérica que incluye datos de tipo <typeparamref name="T"/>.
    /// </summary>
    public class BaseDBResponse<T> : BaseDBResponse
    {
        /// <summary>
        /// Datos retornados en la respuesta.
        /// </summary>
        public T? Data { get; set; }
    }
}