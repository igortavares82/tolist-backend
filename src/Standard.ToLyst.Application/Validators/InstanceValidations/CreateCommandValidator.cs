using FluentValidation;
using Standard.ToLyst.Application.Commands.InstanceCommands;
using Standard.ToLyst.Model.Constants;

namespace Standard.ToLyst.Application.Validators.InstanceValidations
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

