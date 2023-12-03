using System.Collections.Generic;
using MediatR;
using Standard.ToLyst.Model.Common;

namespace Standard.ToLyst.Application.Commands.LystCommands
{
    public class UpdateCommand : Request, IRequest<Result<Unit>>
	{
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsDraft { get; set; }
        public Dictionary<string, string[]> Items { get; set; } 
    }
}

