using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Standard.ToList.Model.Aggregates.Markets;
using Standard.ToList.Model.Aggregates.Products;

namespace Standard.ToList.Application.Searchers
{
    public class AuchanSearcher : Searcher
    {
        private const string URL = "pt/pesquisa?q={0}";

        public AuchanSearcher(Market market, HttpClient httpClient) : base(market, httpClient)
        {
            _httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.32.2");
            _httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "utf-8");
        }

        public override async Task<IEnumerable<Product>> SearchAsync(string product) 
        {
            base.Sleep();

            using var httpResponse = await _httpClient.GetAsync(string.Format(URL, product));
            string json = await httpResponse.Content.ReadAsStringAsync();

            return Enumerable.Empty<Product>();
        }
    }
}
