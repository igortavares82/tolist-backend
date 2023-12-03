using System;
using MediatR;
using Standard.ToLyst.Model.Common;

namespace Standard.ToLyst.Application.Commands.UserCommands
{
	public class DeleteCommand : Request, IRequest<Result<Unit>>
	{
		
	}
}

