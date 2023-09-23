using FluentValidation;
using Standard.ToList.Application.Commands.UserCommands;
using Standard.ToList.Model.Constants;

namespace Standard.ToList.Application.Validators.UserValidators
{
    public class CreateCommandValidator : AbstractValidator<CreateCommand>
    {
		public CreateCommandValidator()
		{
            RuleFor(it => it.Name)
                .NotNull()
                .WithMessage(Validations.Required)
                .NotEmpty()
                .WithMessage(Validations.Required);

            RuleFor(it => it.Email)
                .NotNull()
                .WithMessage(Validations.Required)
                .NotEmpty()
                .WithMessage(Validations.Required)
                .EmailAddress()
                .WithMessage(Validations.InvalidValue);

            RuleFor(it => it.Password)
                .NotNull()
                .WithMessage(Validations.Required)
                .NotEmpty()
                .WithMessage(Validations.Required)
                .MinimumLength(8)
                .WithMessage(Validations.PasswordLength)
                .MaximumLength(15)
                .WithMessage(Validations.PasswordLength);  
        }
	}
}

