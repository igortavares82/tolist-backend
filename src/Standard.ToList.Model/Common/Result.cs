using System;
using System.Collections.Generic;
using System.Linq;

namespace Standard.ToList.Model.Common
{
	public class Result<TEntity>
	{
		public TEntity Data { get; private set; }
		public List<string> Messages { get; private set; }
		public ResultStatus Status { get; private set; } = default;

		public Result(TEntity data)
		{
			Data = data;
			Messages = new List<string>();
		}

        public Result(TEntity data, ResultStatus status) : this(data)
        {
			Status = status;
        }

		public Result(TEntity data, ResultStatus status, params string[] messages) : this(data, status)
		{
			Messages = messages.ToList();
		}
    }
}

