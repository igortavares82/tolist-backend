using System;
using Amazon.Runtime.Internal;
using MediatR;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Application
{
    public class RetrieveCommand : IRequest<Result<Unit>>
    {
        public string Email { get; set; }
    }
}
