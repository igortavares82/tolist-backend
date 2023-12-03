using System;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Model.ViewModels.Lysts
{
	public class LystRequest : Request
	{
		public string Name { get; set; }
		public bool IsDraft { get; set; }
        public bool IsEnabled { get; set; }
    }
}

