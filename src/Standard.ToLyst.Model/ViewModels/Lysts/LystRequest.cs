using System;
using Standard.ToLyst.Model.Common;

namespace Standard.ToLyst.Model.ViewModels.Lysts
{
	public class LystRequest : Request
	{
		public string Name { get; set; }
		public bool IsDraft { get; set; }
        public bool IsEnabled { get; set; }
    }
}

