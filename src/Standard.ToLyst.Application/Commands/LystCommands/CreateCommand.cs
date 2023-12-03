using System.Collections.Generic;
using MediatR;
using Standard.ToLyst.Model.Aggregates.Lists;
using Standard.ToLyst.Model.Common;

namespace Standard.ToLyst.Application.Commands.LystCommands
{
    public class CreateCommand : Request, IRequest<Result<Lyst>>
	{
		public string Name { get; set; }
		public bool IsDraft { get; set; }
		public Dictionary<string, string[]> Items { get; set; }
	}
}

