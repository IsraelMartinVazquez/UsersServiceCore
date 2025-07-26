using FluentValidation;
using UsersService.Application.Dtos;
namespace UsersService.Application.Validators;

/// <summary>
/// Validador para la solicitud de registro de usuario.
/// </summary>
public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
{
    /// <summary>
    /// Constructor que define las reglas de validación para el registro de usuario.
    /// </summary>
    public RegisterUserValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre no puede superar los 50 caracteres.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("El apellido es obligatorio.")
            .MaximumLength(100).WithMessage("El apellido no puede superar los 50 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El correo es obligatorio.")
            .EmailAddress().WithMessage("El correo no tiene un formato válido.")
            .MaximumLength(150).WithMessage("El correo no puede superar los 100 caracteres.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("El número telefónico es obligatorio.")
            .Matches(@"^\+?[0-9]{10,15}$")
            .WithMessage("El número telefónico no es válido. Ejemplo: +521234567890 o 1234567890.");

        RuleFor(x => x.PasswordHash)
            .NotEmpty().WithMessage("La contraseña es obligatoria.")
            .MinimumLength(64).WithMessage("La contraseña debe estar hasheada (mínimo 64 caracteres para SHA-256).");
    }
}