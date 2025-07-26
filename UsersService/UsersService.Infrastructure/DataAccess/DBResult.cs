using System.Data;

namespace UsersService.Infrastructure.DataAccess
{
    /// <summary>
    /// Representa el resultado de la ejecución de una operación en base de datos.
    /// </summary>
    public class DBResult
    {
        /// <summary>
        /// Indica si la operación fue exitosa.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Mensaje descriptivo del resultado o error.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Conjunto de datos devuelto por la operación (puede estar vacío).
        /// </summary>
        public DataSet Ds { get; set; } = new();
    }
}