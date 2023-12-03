using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Standard.ToLyst.Model.Aggregates.Markets;
using Standard.ToLyst.Model.Aggregates.Products;

namespace Standard.ToLyst.Infrastructure.Searchers
{
    public abstract class Searcher
    {
        protected Market _market { get; set; }
        protected HttpClient _httpClient { get; set; }

        public Searcher(Market market, HttpClient httpClient) 
        {
            _market = market;
            _httpClient = httpClient;
        }

        public virtual string Match(MatchCollection? matches, int index, string pattern) 
        {
            return Regex.Match(matches[index]?.Value, pattern).Groups.LastOrDefault()?.Value;
        }

        public virtual async Task<IEnumerable<Product>> SearchAsync(string product)
		{
			return Array.Empty<Product>();
		}

        public virtual void Sleep()
		{
			int time = new Random().Next(1000, 3000);
			Thread.Sleep(time);
		}
    }
}
