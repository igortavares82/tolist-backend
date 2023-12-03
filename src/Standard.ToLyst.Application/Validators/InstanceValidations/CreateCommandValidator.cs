using FluentValidation;
using Standard.ToList.Application.Commands.InstanceCommands;
using Standard.ToList.Model.Constants;

namespace Standard.ToList.Application.Validators.InstanceValidations
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
        }
	}
}

