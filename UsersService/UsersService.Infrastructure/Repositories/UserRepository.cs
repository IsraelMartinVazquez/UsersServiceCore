using System.Data;
using Microsoft.Data.SqlClient;
using UsersService.Application.Dtos;
using UsersService.Application.Interfaces;
using UsersService.Domain.Entities;
using UsersService.Infrastructure.DataAccess;

namespace UsersService.Infrastructure.Repositories
{
    /// <summary>
    /// Implementación del repositorio para operaciones relacionadas con usuarios.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly DBConnection? _dBConnection;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="UserRepository"/>.
        /// </summary>
        /// <param name="dBConnection">Instancia para conexión y ejecución de base de datos.</param>
        public UserRepository(DBConnection dBConnection)
        {
            _dBConnection = dBConnection;
        }

        /// <summary>
        /// Registra un nuevo usuario en la base de datos mediante un procedimiento almacenado.
        /// </summary>
        /// <param name="model">Entidad <see cref="User"/> con la información del usuario a registrar.</param>
        /// <returns>Respuesta con el resultado de la operación, incluyendo el ID generado.</returns>
        public async Task<BaseDBResponse<int>> RegisterUserAsync(User model)
        {
            BaseDBResponse<int> dBResponse = new();
            try
            {
                List<SqlParameter> parameters = new()
                {
                    new ("@FirstName", model.FirstName),
                    new ("@LastName", model.LastName),
                    new ("@Email", model.Email),
                    new ("@PhoneNumber", model.PhoneNumber),
                    new ("@PasswordHash", model.PasswordHash)
                };

                var resultDB = await _dBConnection!.ExecuteSPAsync("[User].[SP_RegisterUser]", parameters);
                if (!resultDB.Success)
                {
                    dBResponse.Message = resultDB.Message;
                    return dBResponse;
                }

                dBResponse.Success = true;
                if (resultDB.Ds.Tables.Count == 0 || resultDB.Ds.Tables[0].Rows.Count == 0)
                {
                    dBResponse.Message = "El procedimiento se ejecutó con éxito, pero no se encontraron resultados.";
                    return dBResponse;
                }

                DataRow row = resultDB.Ds.Tables[0].Rows[0];
                dBResponse.Data = row["Result"] != DBNull.Value ? Convert.ToInt32(row["Result"]) : 0;
                return dBResponse;
            }
            catch (Exception ex)
            {
                dBResponse.Message = ex.Message;
                return dBResponse;
            }
        }

        /// <summary>
        /// Verifica si un correo electrónico ya está registrado en la base de datos.
        /// </summary>
        /// <param name="email">Correo electrónico a verificar.</param>
        /// <returns>Respuesta con un valor booleano indicando si el email existe o no.</returns>
        public async Task<BaseDBResponse<bool>> CheckEmailExistsAsync(string email)
        {
            BaseDBResponse<bool> dBResponse = new();
            try
            {
                List<SqlParameter> parameters = new()
                {
                    new ("@Email", email),
                };

                var resultDB = await _dBConnection!.ExecuteSPAsync("[User].[SP_CheckEmailExists]", parameters);
                if (!resultDB.Success)
                {
                    dBResponse.Message = resultDB.Message;
                    return dBResponse;
                }

                dBResponse.Success = true;
                if (resultDB.Ds.Tables.Count == 0 || resultDB.Ds.Tables[0].Rows.Count == 0)
                {
                    dBResponse.Message = "El procedimiento se ejecutó con éxito, pero no se encontraron resultados.";
                    return dBResponse;
                }

                DataRow row = resultDB.Ds.Tables[0].Rows[0];
                dBResponse.Data = row["Result"] != DBNull.Value ? Convert.ToBoolean(row["Result"]) : false;
                return dBResponse;
            }
            catch (Exception ex)
            {
                dBResponse.Message = ex.Message;
                return dBResponse;
            }
        }
    }
}