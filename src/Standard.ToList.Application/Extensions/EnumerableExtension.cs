using System;
using System.Collections.Generic;
using System.Linq;

namespace Standard.ToList.Application.Extensions
{
	public static class EnumerableExtension
	{
		public static TOutput[] ToArray<TInput,TOutput>(this IEnumerable<TInput> source, Func<TInput,TOutput> selector)
		{
			return source.Select(selector).ToArray();
		}
	}
}

