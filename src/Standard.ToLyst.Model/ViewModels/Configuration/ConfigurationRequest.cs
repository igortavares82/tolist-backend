using System;
using Standard.ToLyst.Model.Aggregates.Configuration;
using Standard.ToLyst.Model.Common;

namespace Standard.ToLyst.Model
{
    public class ConfigurationRequest : Request
    {
        public WorkerType[] WorkerTypes { get; set; }
    }
}
