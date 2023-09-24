using FluentValidation;
using Standard.ToList.Application.Commands.UserCommands;
using Standard.ToList.Model.Constants;

namespace Standard.ToList.Application.Validators.UserValidators
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

