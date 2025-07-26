namespace UsersService.Application.Dtos
{
    /// <summary>
    /// Representa la solicitud para obtener las cuentas asociadas a un usuario.
    /// </summary>
    public class AccountsRequest
    {
        /// <summary>
        /// Identificador único del usuario cuyas cuentas se desean consultar.
        /// </summary>
        public int UserId { get; set; }
    }
}