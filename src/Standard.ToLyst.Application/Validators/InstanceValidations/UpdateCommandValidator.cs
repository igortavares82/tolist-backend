using FluentValidation;
using Standard.ToLyst.Application.Commands.InstanceCommands;
using Standard.ToLyst.Model.Constants;

namespace Standard.ToLyst.Application.Validators.InstanceValidations
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

        }
    }
}

