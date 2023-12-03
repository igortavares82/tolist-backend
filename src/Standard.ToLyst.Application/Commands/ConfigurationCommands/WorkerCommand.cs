using System;
using Standard.ToLyst.Model.Aggregates.Configuration;

namespace Standard.ToLyst.Application
{
   public class WorkerCommand
    {
        public WorkerType Type { get; set; }
        public bool IsEnabled { get; set; }
        public int Delay { get; set; }
        public int Items { get; set; } = 100;
        public int Next { get; set; } = 1;
        public string Properties { get; set; }
    }
}