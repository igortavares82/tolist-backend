using System;
using Standard.ToList.Model.Aggregates.Configuration;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Model
{
    public class ConfigurationRequest : Request
    {
        public WorkerType[] WorkerTypes { get; set; }
    }
}
