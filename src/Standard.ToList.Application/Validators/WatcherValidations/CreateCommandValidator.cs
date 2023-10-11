using FluentValidation;
using Standard.ToList.Application.Commands.WatcherCommands;
using Standard.ToList.Model.Constants;

namespace Standard.ToList.Application.Validators.WatcherValidations
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

