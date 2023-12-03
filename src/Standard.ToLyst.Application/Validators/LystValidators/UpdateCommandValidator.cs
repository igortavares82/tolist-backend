using System;
using FluentValidation;
using Standard.ToLyst.Application.Commands.LystCommands;
using Standard.ToLyst.Model.Constants;

namespace Standard.ToLyst.Application.Validators.LystValidators
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

