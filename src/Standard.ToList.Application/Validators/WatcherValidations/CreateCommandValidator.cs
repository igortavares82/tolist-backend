using FluentValidation;
using Standard.ToList.Application.Commands.WatcherCommands;

namespace Standard.ToList.Application.Validators.WatcherValidations
{
    public class CreateCommandValidator : AbstractValidator<CreateCommand>
    {
		public CreateCommandValidator()
		{
            RuleFor(it => it.Current)
                .GreaterThan(0)
                .WithMessage("Current value must be greater then 0.")
                .GreaterThanOrEqualTo(it => it.Desired)
                .WithMessage("Desired value can not be greater than or equal to current one.");
        }
	}
}

