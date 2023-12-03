using System;
using Amazon.Runtime.Internal;
using MediatR;
using Standard.ToLyst.Model.Common;

namespace Standard.ToLyst.Application
{
    public class RetrieveCommand : IRequest<Result<Unit>>
    {
        public string Email { get; set; }
    }
}
