using System;
namespace Standard.ToList.Application.Extensions
{
	public static class DateTimeExtension
	{
		public static int GetDays(this DateTime? source, DateTime? dateTime = null)
		{
			if (!source.HasValue)
				return 0;

			if (!dateTime.HasValue)
				dateTime = DateTime.Now;

			var diff = dateTime.Value - source.Value;
			return diff.Days;
		}
	}
}

