using FluentValidation;
using Standard.ToList.Application.Commands.WatcherCommands;
using Standard.ToList.Model.Constants;

namespace Standard.ToList.Application.Validators.WatcherValidations
{
    public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
    {
		public UpdateCommandValidator()
		{
            RuleFor(it => it.Desired)
                .GreaterThan(0)
                .WithMessage(Messages.ValueGreater);
        }
	}
}

