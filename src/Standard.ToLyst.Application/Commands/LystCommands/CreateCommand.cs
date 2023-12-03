using System.Collections.Generic;
using MediatR;
using Standard.ToList.Model.Aggregates.Lists;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Application.Commands.LystCommands
{
    public class CreateCommand : Request, IRequest<Result<Lyst>>
	{
		public string Name { get; set; }
		public bool IsDraft { get; set; }
		public Dictionary<string, string[]> Items { get; set; }
	}
}

