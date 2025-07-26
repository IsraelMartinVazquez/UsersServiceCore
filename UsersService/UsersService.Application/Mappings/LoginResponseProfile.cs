using AutoMapper;
using UsersService.Application.Dtos;
using UsersService.Domain.Entities;

namespace UsersService.Application.Mappings
{
    /// <summary>
    /// Perfil de AutoMapper para mapear entre <see cref="User"/> y <see cref="LoginResponse"/>.
    /// </summary>
    public class LoginResponseProfile : Profile
    {
        /// <summary>
        /// Configura los mapeos bidireccionales entre <see cref="User"/> y <see cref="LoginResponse"/>.
        /// </summary>
        public LoginResponseProfile()
        {
            CreateMap<User, LoginResponse>();
            CreateMap<LoginResponse, User>();
        }
    }
}