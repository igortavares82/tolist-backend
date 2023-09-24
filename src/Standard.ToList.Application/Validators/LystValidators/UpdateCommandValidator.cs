using System;
using FluentValidation;
using Standard.ToList.Application.Commands.LystCommands;
using Standard.ToList.Model.Constants;

namespace Standard.ToList.Application.Validators.LystValidators
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

            RuleFor(it => it.Items)
                .Must(it => it?.Count > 0)
                .WithMessage(Messages.LystItems);
        }
	}
}

