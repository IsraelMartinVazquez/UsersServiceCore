namespace UsersService.Application.Security
{
    /// <summary>
    /// Clase estática para comparar hashes de contraseñas.
    /// </summary>
    public static class PasswordComparer
    {
        /// <summary>
        /// Compara dos hashes de contraseñas de forma segura e insensible a mayúsculas.
        /// </summary>
        /// <param name="requestPasswordHash">Hash de la contraseña proporcionada por el usuario.</param>
        /// <param name="dBUserPasswordHash">Hash de la contraseña almacenada en la base de datos.</param>
        /// <returns>Verdadero si ambos hashes coinciden; de lo contrario, falso.</returns>
        public static bool AreHashesEqual(string requestPasswordHash, string dBUserPasswordHash)
        {
            return string.Equals(requestPasswordHash, dBUserPasswordHash, StringComparison.OrdinalIgnoreCase);
        }
    }
}