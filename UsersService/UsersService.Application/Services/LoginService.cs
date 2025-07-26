using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using UsersService.Application.Dtos;
using UsersService.Application.Interfaces;
using UsersService.Application.Security;
using UsersService.Domain.Entities;

namespace UsersService.Application.Services
{
    /// <summary>
    /// Servicio que implementa la lógica de autenticación de usuarios.
    /// </summary>
    public class LoginService : ILoginService
    {
        private readonly IValidator<LoginRequest> _validator;
        private readonly IMapper _mapper;
        private readonly ILoginRepository _loginRepository;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="LoginService"/>.
        /// </summary>
        /// <param name="validator">Validador para la solicitud de login.</param>
        /// <param name="mapper">AutoMapper para conversiones entre entidades y DTOs.</param>
        /// <param name="loginRepository">Repositorio para operaciones de login.</param>
        public LoginService(IValidator<LoginRequest> validator, IMapper mapper, ILoginRepository loginRepository)
        {
            _validator = validator;
            _mapper = mapper;
            _loginRepository = loginRepository;
        }

        /// <summary>
        /// Realiza la autenticación del usuario con validación, verificación de intentos fallidos y comparación de contraseña.
        /// </summary>
        /// <param name="model">Modelo con los datos de login.</param>
        /// <returns>Respuesta con datos del usuario autenticado o errores.</returns>
        public async Task<BaseResponse<LoginResponse>> LoginUserAsync(LoginRequest model)
        {
            BaseResponse<LoginResponse> response = new();
            response.Data = new();
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

                BaseDBResponse<User> dBGetUser = await _loginRepository.GetUserByEmailAsync(model.Email);
                if (!dBGetUser.Success)
                {
                    response.Message = "Ocurrió un error al procesar la información.";
                    response.Errors.Add(dBGetUser.Message);
                    response.StatusCode = 400;
                    return response;
                }

                if (dBGetUser.Data == null)
                {
                    response.Message = "Ocurrió un error al procesar la información.";
                    response.Errors.Add("El usuario no existe.");
                    response.StatusCode = 400;
                    return response;
                }

                BaseDBResponse<int> dBGetFailedLogin = await _loginRepository.GetFailedLoginAttemptsByUser(dBGetUser.Data.UserId);
                if (!dBGetFailedLogin.Success)
                {
                    response.Message = "Ocurrió un error al procesar la información.";
                    response.Errors.Add(dBGetUser.Message);
                    response.StatusCode = 400;
                    return response;
                }

                if (dBGetFailedLogin.Data > 2)
                {
                    response.Message = "Ocurrió un error al procesar la información.";
                    response.Errors.Add("La cuenta está bloqueada por motivos de seguridad.");
                    response.StatusCode = 403;
                    return response;
                }

                if (!PasswordComparer.AreHashesEqual(model.PasswordHash, dBGetUser.Data.PasswordHash))
                {
                    FailedAttempt modelFailed = new() { UserId = dBGetUser.Data.UserId, PasswordHash = model.PasswordHash };
                    await _loginRepository.InsertFailedLoginAttemptAsync(modelFailed);
                    response.Message = "Ocurrió un error al procesar la información.";
                    response.Errors.Add("La contraseña ingresada es incorrecta.");
                    response.StatusCode = 400;
                    return response;
                }

                response.Success = true;
                response.Data = _mapper!.Map<LoginResponse>(dBGetUser.Data);
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