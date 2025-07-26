using AutoMapper;
using UsersService.Application.Dtos;
using UsersService.Domain.Entities;

namespace UsersService.Application.Mappings
{
    /// <summary>
    /// Perfil de AutoMapper para mapear entre <see cref="Login"/> y <see cref="LoginRequest"/>.
    /// </summary>
    public class LoginRequestProfile : Profile
    {
        /// <summary>
        /// Configura los mapeos bidireccionales entre <see cref="Login"/> y <see cref="LoginRequest"/>.
        /// </summary>
        public LoginRequestProfile()
        {
            CreateMap<Login, LoginRequest>();
            CreateMap<LoginRequest, Login>();
        }
    }
}