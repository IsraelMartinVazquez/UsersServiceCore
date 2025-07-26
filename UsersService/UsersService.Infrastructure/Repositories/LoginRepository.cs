using Microsoft.Data.SqlClient;
using System.Data;
using UsersService.Application.Dtos;
using UsersService.Infrastructure.DataAccess;
using UsersService.Domain.Entities;
using UsersService.Application.Interfaces;

namespace UsersService.Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio que implementa operaciones de login y control de intentos fallidos.
    /// </summary>
    public class LoginRepository : ILoginRepository
    {
        private readonly DBConnection? _dBConnection;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="LoginRepository"/>.
        /// </summary>
        /// <param name="dBConnection">Instancia para conexión y ejecución de base de datos.</param>
        public LoginRepository(DBConnection dBConnection)
        {
            _dBConnection = dBConnection;
        }

        /// <summary>
        /// Obtiene un usuario por su correo electrónico.
        /// </summary>
        /// <param name="email">Correo electrónico del usuario.</param>
        /// <returns>Respuesta con los datos del usuario o mensaje de error.</returns>
        public async Task<BaseDBResponse<User>> GetUserByEmailAsync(string email)
        {
            BaseDBResponse<User> dBResponse = new();
            try
            {
                List<SqlParameter> parameters = new()
                {
                    new ("@Email", email),
                };

                var resultDB = await _dBConnection!.ExecuteSPAsync("[User].[SP_GetUserByEmail]", parameters);
                if (!resultDB.Success)
                {
                    dBResponse.Message = resultDB.Message;
                    return dBResponse;
                }

                dBResponse.Success = true;
                if (resultDB.Ds.Tables.Count == 0 || resultDB.Ds.Tables[0].Rows.Count == 0)
                {
                    dBResponse.Data = null;
                    return dBResponse;
                }

                DataRow row = resultDB.Ds.Tables[0].Rows[0];
                dBResponse.Data = new()
                {
                    UserId = row["UserId"] != DBNull.Value ? Convert.ToInt32(row["UserId"]) : 0,
                    FirstName = row["FirstName"] != DBNull.Value ? row["FirstName"].ToString()! : string.Empty,
                    LastName = row["LastName"] != DBNull.Value ? row["LastName"].ToString()! : string.Empty,
                    Email = row["Email"] != DBNull.Value ? row["Email"].ToString()! : string.Empty,
                    PhoneNumber = row["PhoneNumber"] != DBNull.Value ? row["PhoneNumber"].ToString()! : string.Empty,
                    PasswordHash = row["PasswordHash"] != DBNull.Value ? row["PasswordHash"].ToString()! : string.Empty
                };

                return dBResponse;
            }
            catch (Exception ex)
            {
                dBResponse.Message = ex.Message;
                return dBResponse;
            }
        }

        /// <summary>
        /// Obtiene la cantidad de intentos fallidos de login para un usuario.
        /// </summary>
        /// <param name="userId">Identificador del usuario.</param>
        /// <returns>Respuesta con la cantidad de intentos o mensaje de error.</returns>
        public async Task<BaseDBResponse<int>> GetFailedLoginAttemptsByUser(int userId)
        {
            BaseDBResponse<int> dBResponse = new();
            try
            {
                List<SqlParameter> parameters = new()
                {
                    new ("@UserId", userId)
                };

                var resultDB = await _dBConnection!.ExecuteSPAsync("[Session].[SP_GetFailedLoginAttemptsByUser]", parameters);
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
        /// Inserta un registro de intento fallido de login para un usuario.
        /// </summary>
        /// <param name="model">Entidad que contiene el usuario y hash de contraseña del intento fallido.</param>
        /// <returns>Respuesta con resultado de la inserción o mensaje de error.</returns>
        public async Task<BaseDBResponse<int>> InsertFailedLoginAttemptAsync(FailedAttempt model)
        {
            BaseDBResponse<int> dBResponse = new();
            try
            {
                List<SqlParameter> parameters = new()
                {
                    new ("@UserId", model.UserId),
                    new ("@PasswordHash", model.PasswordHash),
                };

                var resultDB = await _dBConnection!.ExecuteSPAsync("[Session].[SP_InsertFailedLoginAttempt]", parameters);
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
    }
}