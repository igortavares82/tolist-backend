using System;
using MediatR;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Application.Commands.UserCommands
{
	public class DeleteCommand : Request, IRequest<Result<Unit>>
	{
		
	}
}

