using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using UsersService.Application.Dtos;
using UsersService.Application.Interfaces;
using UsersService.Domain.Entities;

namespace UsersService.Application.Services
{
    /// <summary>
    /// Servicio que implementa la lógica para el registro de usuarios.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IValidator<RegisterUserRequest> _validator;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="UserService"/>.
        /// </summary>
        /// <param name="validator">Validador para la solicitud de registro.</param>
        /// <param name="mapper">AutoMapper para conversión entre DTO y entidad.</param>
        /// <param name="userRepository">Repositorio para operaciones con usuarios.</param>
        public UserService(IValidator<RegisterUserRequest> validator, IMapper mapper, IUserRepository userRepository)
        {
            _validator = validator;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Registra un nuevo usuario validando la solicitud y verificando si el correo ya existe.
        /// </summary>
        /// <param name="model">Modelo con los datos para registrar al usuario.</param>
        /// <returns>Respuesta con el resultado de la operación.</returns>
        public async Task<BaseResponse> RegisterUserAsync(RegisterUserRequest model)
        {
            BaseResponse response = new();
            try
            {
                ValidationResult result = await _validator!.ValidateAsync(model);
                if (!result.IsValid)
                {
                    response.Message = "La solicitud no es válida. Verifique los datos enviados.";
                    response.Errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                    response.StatusCode = 400;
                    return response;
                }

                BaseDBResponse<bool> dBCheckEmail = await _userRepository.CheckEmailExistsAsync(model.Email);
                if (!dBCheckEmail.Success)
                {
                    response.Message = "Se produjo un error al intentar registrar al usuario.";
                    response.Errors.Add(dBCheckEmail.Message);
                    response.StatusCode = 400;
                    return response;
                }

                if (dBCheckEmail.Data)
                {
                    response.Message = "Se produjo un error al intentar registrar al usuario.";
                    response.Errors.Add("El usuario ya se encuentra registrado.");
                    response.StatusCode = 400;
                    return response;
                }

                User userModel = _mapper!.Map<User>(model);
                BaseDBResponse<int> dBRegisterUser = await _userRepository.RegisterUserAsync(userModel);
                if (!dBRegisterUser.Success)
                {
                    response.Message = "Se produjo un error al intentar registrar al usuario.";
                    response.Errors.Add(dBRegisterUser.Message);
                    response.StatusCode = 400;
                    return response;
                }

                if (dBRegisterUser.Data < 0)
                {
                    response.Message = "Se produjo un error al intentar registrar al usuario.";
                    response.Errors.Add(dBRegisterUser.Message);
                    response.StatusCode = 400;
                    return response;
                }

                response.Success = true;
                response.StatusCode = 200;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Ocurrió un error al procesar la información.";
                response.Errors?.Add(ex.Message);
                return response;
            }
        }
    }
}