using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Standard.ToList.Model.Common
{
    public class Result<TEntity> : IActionResult
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

		public Result<TEntity> SetResult(ResultStatus status, string message, bool cleanup = false)
		{
			if (cleanup)
				Messages.Clear();

			Status = status;
			Messages.Add(message);

			return this;
		}

        public async Task ExecuteResultAsync(ActionContext context)
        {
			var response = context.HttpContext.Response;

			switch (Status)
			{
				case ResultStatus.Error:
                case ResultStatus.Exists:
                case ResultStatus.Inactive:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
					break;

				case ResultStatus.NotFound:
					response.StatusCode = (int)HttpStatusCode.NotFound;
					break;

                case ResultStatus.Success:
					response.StatusCode = (int)HttpStatusCode.OK;
					break;
			}

			var serializer = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var json = JsonConvert.SerializeObject(this, serializer);
			var bytes = Encoding.UTF8.GetBytes(json);

			context.HttpContext.Response.ContentType = "application/json";
            await context.HttpContext.Response.Body.WriteAsync(bytes);
        }
    }
}

