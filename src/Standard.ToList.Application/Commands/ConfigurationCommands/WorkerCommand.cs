using System;
using Standard.ToList.Model.Aggregates.Configuration;

namespace Standard.ToList.Application
{
   public class WorkerCommand
    {
        public WorkerType Type { get; set; }
        public int Delay { get; set; }
        public int Items { get; set; } = 100;
        public int Next { get; set; } = 1;
        public string Properties { get; set; }
    }
}