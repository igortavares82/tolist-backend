using FluentValidation;
using Standard.ToList.Application.Commands.UserCommands;
using Standard.ToList.Model.Constants;

namespace Standard.ToList.Application.Validators.UserValidators
{
    public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
	{
		public UpdateCommandValidator()
		{
			RuleFor(it => it.Name)
				.NotNull()
                .WithMessage(Validations.Required)
                .NotEmpty()
                .WithMessage(Validations.Required);

			RuleFor(it => it.Password)
				.NotNull()
				.WithMessage(Validations.Required)
				.NotEmpty()
				.WithMessage(Validations.Required);

			RuleFor(it => false)
				.NotEqual(it => it.IsOperationAllowed())
				.WithMessage(Validations.OperationNotAllowed);
		}
	}
}

