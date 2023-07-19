using System;
using Microsoft.Extensions.Options;
using Standard.ToList.Model.Aggregates.Lists;
using Standard.ToList.Model.Options;

namespace Standard.ToList.Infrastructure.Repositories
{
	public class LystRepository : Repository<Lyst>, ILystRepository
	{
		public LystRepository(IOptions<AppSettingOptions> settings) : base(settings)
		{

		}
	}
}

