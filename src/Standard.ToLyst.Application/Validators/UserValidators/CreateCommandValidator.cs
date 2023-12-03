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
                .WithMessage(Messages.Required)
                .NotEmpty()
                .WithMessage(Messages.Required);

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

