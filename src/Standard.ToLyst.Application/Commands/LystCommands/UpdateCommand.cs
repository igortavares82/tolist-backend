using System.Collections.Generic;
using MediatR;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Application.Commands.LystCommands
{
    public class UpdateCommand : Request, IRequest<Result<Unit>>
	{
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsDraft { get; set; }
        public Dictionary<string, string[]> Items { get; set; } 
    }
}

