using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UsersService.Application.Dtos;
using UsersService.Application.Interfaces;
using UsersService.Domain.Entities;

namespace UsersService.Infrastructure.Repositories
{
    /// <summary>
    /// Implementación de IJwtRepository que se encarga de generar tokens JWT.
    /// </summary>
    public class JwtRepository : IJwtRepository
    {
        private readonly JwtOptions _jwtOptions;

        /// <summary>
        /// Constructor que recibe las opciones configuradas para JWT.
        /// </summary>
        /// <param name="jwtOptions">Opciones de configuración de JWT.</param>
        public JwtRepository(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        /// <summary>
        /// Genera un token JWT firmado con los datos del usuario.
        /// </summary>
        /// <param name="user">Entidad de usuario para generar el token.</param>
        /// <returns>Token JWT como cadena.</returns>
        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("UserId", user.UserId.ToString()),
                new Claim("UserEmail", user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiresInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}