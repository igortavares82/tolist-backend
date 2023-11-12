using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Standard.ToList.Model.Aggregates.Markets;
using Standard.ToList.Model.Aggregates.Products;

namespace Standard.ToList.Application.Searchers
{
    public abstract class Searcher
    {
        protected Market _market { get; set; }
        protected HttpClient _httpClient { get; set; }

        public Searcher(Market market, HttpClient httpClient) 
        {
            _market = market;
            _httpClient = httpClient;
            
            _httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.32.2");
            _httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "utf-8");
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
