using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Standard.ToLyst.Model.Helpers;

namespace Standard.ToLyst.Model.Common
{
    public class Result<TEntity> : IActionResult
	{
		public TEntity Data { get; private set; }
		public Page Page { get; private set; }
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
			Messages = messages?.ToList();
        }

        public Result(TEntity data, ResultStatus status, Page page, params string[] messages) : this(data, status)
        {
			Page = page;
            Messages = messages?.ToList();
        }

        public Result<TEntity> SetResult(ResultStatus status, string message, bool cleanup = false)
		{
			if (cleanup)
				Messages.Clear();

			Status = status;
			Messages.Add(message);

			return this;
		}

        public Result<TEntity> SetResult(TEntity data, ResultStatus status, string message = null, bool cleanup = false)
        {
			this.SetResult(status, message, cleanup);
			Data = data;

            return this;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
			var statusCode = HttpStatusCode.OK;

            switch (Status)
			{
				case ResultStatus.Error:
                    statusCode = HttpStatusCode.BadRequest;
					break;

				case ResultStatus.NotFound:
                    statusCode = HttpStatusCode.NotFound;
					break;

				case ResultStatus.UnprosseableEntity:
                    statusCode = HttpStatusCode.UnprocessableEntity;
					break;

				case ResultStatus.Created:
                    statusCode = HttpStatusCode.Created;
					break;

				case ResultStatus.NoContent:
                    statusCode = HttpStatusCode.NoContent;
					break;
            }

            await context.HttpContext.WriteResult(this, statusCode);
        }
    }
}

