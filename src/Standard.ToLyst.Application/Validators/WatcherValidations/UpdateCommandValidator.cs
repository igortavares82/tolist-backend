using FluentValidation;
using Standard.ToLyst.Application.Commands.WatcherCommands;
using Standard.ToLyst.Model.Constants;

namespace Standard.ToLyst.Application.Validators.WatcherValidations
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

