using FluentValidation;
using Standard.ToLyst.Application.Commands.UserCommands;
using Standard.ToLyst.Model.Constants;

namespace Standard.ToLyst.Application.Validators.UserValidators
{
    public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
	{
		public UpdateCommandValidator()
		{
			RuleFor(it => it.Name)
				.NotNull()
                .WithMessage(Messages.Required)
                .NotEmpty()
                .WithMessage(Messages.Required);

			RuleFor(it => it.Password)
				.NotNull()
				.WithMessage(Messages.Required)
				.NotEmpty()
				.WithMessage(Messages.Required);

			RuleFor(it => false)
				.NotEqual(it => it.IsOperationAllowed())
				.WithMessage(Messages.OperationNotAllowed);
		}
	}
}

