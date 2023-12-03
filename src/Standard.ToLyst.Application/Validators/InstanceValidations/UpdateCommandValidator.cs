using FluentValidation;
using Standard.ToList.Application.Commands.InstanceCommands;
using Standard.ToList.Model.Constants;

namespace Standard.ToList.Application.Validators.InstanceValidations
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

