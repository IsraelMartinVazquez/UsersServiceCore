using FluentValidation;
using UsersService.Application.Dtos;

namespace UsersService.Application.Validators;

public class LoginValidator : AbstractValidator<LoginRequest>
{
	public LoginValidator()
	{
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El correo es obligatorio.")
            .EmailAddress().WithMessage("El correo no tiene un formato válido.")
            .MaximumLength(150).WithMessage("El correo no puede superar los 100 caracteres.");

        RuleFor(x => x.PasswordHash)
            .NotEmpty().WithMessage("La contraseña es obligatoria.")
            .MinimumLength(64).WithMessage("La contraseña debe estar hasheada (mínimo 64 caracteres para SHA-256).");
    }
}