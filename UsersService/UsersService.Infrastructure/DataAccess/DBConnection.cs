using System.Data;
using Microsoft.Data.SqlClient;

namespace UsersService.Infrastructure.DataAccess
{
    /// <summary>
    /// Maneja la conexión a la base de datos y la ejecución de procedimientos almacenados.
    /// </summary>
    public class DBConnection
    {
        private readonly string _connectionString;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="DBConnection"/> con la cadena de conexión especificada.
        /// </summary>
        /// <param name="connectionString">Cadena de conexión a la base de datos.</param>
        public DBConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado de forma asíncrona con los parámetros indicados.
        /// </summary>
        /// <param name="procedureName">Nombre del procedimiento almacenado.</param>
        /// <param name="parameters">Lista de parámetros SQL para el procedimiento.</param>
        /// <returns>Resultado de la ejecución con éxito, mensaje y conjunto de datos.</returns>
        public async Task<DBResult> ExecuteSPAsync(string procedureName, List<SqlParameter> parameters)
        {
            DBResult dBResult = new();
            try
            {
                using SqlConnection connection = new(_connectionString);
                using SqlCommand command = new(procedureName, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters.ToArray());

                using SqlDataAdapter adapter = new(command);
                DataSet ds = new();
                await Task.Run(() => adapter.Fill(ds));

                dBResult.Success = true;
                dBResult.Message = "Procedimiento ejecutado correctamente.";
                dBResult.Ds = ds;
                return dBResult;
            }
            catch (Exception ex)
            {
                dBResult.Message = ex.Message;
                dBResult.Ds = new DataSet();
                return dBResult;
            }
        }
    }
}