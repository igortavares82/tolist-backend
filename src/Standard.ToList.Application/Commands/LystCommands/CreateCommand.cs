using System;
using MediatR;
using Standard.ToList.Model.Aggregates.Lists;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Application.Commands.LystCommands
{
	public class CreateCommand : IRequest<Result<Lyst>>
	{
		public string Name { get; set; }
		public string UserId { get; set; }
		public bool IsDraft { get; set; }
		public string[] Items { get; set; }
	}
}

