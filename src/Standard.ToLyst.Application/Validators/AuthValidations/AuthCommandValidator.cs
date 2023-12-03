using FluentValidation;
using Standard.ToLyst.Application.Commands.AuthCommands;
using Standard.ToLyst.Model.Constants;

namespace Standard.ToLyst.Application.Validators.AuthValidations
{
    public class AuthCommandValidator : AbstractValidator<AuthCommand>
    {
		public AuthCommandValidator()
		{
            RuleFor(it => it.Email)
                .NotNull()
                .WithMessage(Messages.Required)
                .NotEmpty()
                .WithMessage(Messages.Required)
                .EmailAddress()
                .WithMessage(Messages.InvalidValue);

            RuleFor(it => it.Password)
                .NotNull()
                .WithMessage(Messages.Required)
                .NotEmpty()
                .WithMessage(Messages.Required)
                .MinimumLength(8)
                .WithMessage(Messages.PasswordLength)
                .MaximumLength(15)
                .WithMessage(Messages.PasswordLength);
        }
    }
}

