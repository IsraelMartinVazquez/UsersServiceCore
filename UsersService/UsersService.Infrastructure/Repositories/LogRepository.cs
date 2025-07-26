using Microsoft.Data.SqlClient;
using UsersService.Domain.Entities;
using UsersService.Infrastructure.DataAccess;

namespace UsersService.Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio para manejar operaciones de registro de logs en la base de datos.
    /// </summary>
    public class LogRepository
    {
        private readonly DBConnection? _dBConnection;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="LogRepository"/>.
        /// </summary>
        /// <param name="dBConnection">Instancia para conexión y ejecución de base de datos.</param>
        public LogRepository(DBConnection dBConnection)
        {
            _dBConnection = dBConnection;
        }

        /// <summary>
        /// Inserta un registro de log con información de la solicitud y respuesta HTTP.
        /// </summary>
        /// <param name="model">Entidad que contiene los datos del log a insertar.</param>
        /// <returns>Tarea asincrónica que representa la operación de inserción.</returns>
        public async Task InsertLogAsync(Log model)
        {
            try
            {
                List<SqlParameter> parameters = new()
                {
                    new("@Domain", model.Domain),
                    new("@Host", model.Host),
                    new("@Method", model.Method),
                    new("@Route", model.Route),
                    new("@JsonHeaders", model.JsonHeaders),
                    new("@JsonRequest", model.JsonRequest),
                    new("@JsonResponse", model.JsonResponse)
                };

                await _dBConnection!.ExecuteSPAsync("[Log].[SP_InsertRequestHistory]", parameters);
            }
            catch
            {
                // Se ignoran errores para no afectar la ejecución principal
            }
        }
    }
}