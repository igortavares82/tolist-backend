using FluentValidation;
using Standard.ToLyst.Application.Commands.UserCommands;
using Standard.ToLyst.Model.Constants;

namespace Standard.ToLyst.Application.Validators.UserValidators
{
    public class DeleteCommandValidator : AbstractValidator<DeleteCommand>
    {
		public DeleteCommandValidator()
		{
            RuleFor(it => false)
                .NotEqual(it => it.IsOperationAllowed())
                .WithMessage(Messages.OperationNotAllowed);
        }
	}
}

