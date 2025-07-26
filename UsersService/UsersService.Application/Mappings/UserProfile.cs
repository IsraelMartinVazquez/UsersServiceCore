using AutoMapper;
using UsersService.Application.Dtos;
using UsersService.Domain.Entities;

namespace UsersService.Application.Mappings
{
    /// <summary>
    /// Perfil de AutoMapper para mapear entre <see cref="User"/> y <see cref="RegisterUserRequest"/>.
    /// </summary>
    public class UserProfile : Profile
    {
        /// <summary>
        /// Configura los mapeos bidireccionales entre <see cref="User"/> y <see cref="RegisterUserRequest"/>.
        /// </summary>
        public UserProfile()
        {
            CreateMap<User, RegisterUserRequest>();
            CreateMap<RegisterUserRequest, User>();
        }
    }
}