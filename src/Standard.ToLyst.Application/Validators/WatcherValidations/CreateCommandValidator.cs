using FluentValidation;
using Standard.ToLyst.Application.Commands.WatcherCommands;
using Standard.ToLyst.Model.Constants;

namespace Standard.ToLyst.Application.Validators.WatcherValidations
{
    public class CreateCommandValidator : AbstractValidator<CreateCommand>
    {
		public CreateCommandValidator()
		{
            RuleFor(it => it.Current)
                .GreaterThan(0)
                .WithMessage(Messages.ValueGreater)
                .GreaterThanOrEqualTo(it => it.Desired)
                .WithMessage(Messages.DesiredValue);
        }
	}
}

