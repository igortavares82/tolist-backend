using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Standard.ToList.Application.Comparers;
using Standard.ToList.Model.Aggregates.Markets;
using Standard.ToList.Model.Aggregates.Products;

namespace Standard.ToList.Application.Services
{
	public class MarketService : IMarketService
    {
        private readonly IMarketRepository _marketRepository;
        private readonly IProductRepository _productRepository;
        private readonly MarketFactory _marketFactory;

        public MarketService(IMarketRepository marketRepository,
                             IProductRepository productRepository,
                             MarketFactory marketFactory)
        {
            _marketRepository = marketRepository;
            _productRepository = productRepository;
            _marketFactory = marketFactory;
        }

        public async Task SearchMissingProductsAsync()
        {
            List<Product> products = new List<Product>();

            var markets = _marketRepository.GetAsync(it => it.IsEnabled == true)
                                           .Result
                                           .ToList();

            string[] marketIds = markets.Select(_it => _it.Id)
                                        .ToArray();

            var missingProducts = await _productRepository.GetAsync<MissingProduct>(it => marketIds.Contains(it.MarketId));

            foreach (var missingProduct in missingProducts)
            {
                try
                {
                    var tasks = markets.Select(it =>
                    {
                        var market = _marketFactory.Instance(it);
                        return market.SearchAsync(missingProduct.Name);
                    }).ToArray();

                    Task.WaitAll(tasks);

                    var _products = tasks.SelectMany(it => it.Result).ToArray();
                    products.AddRange(_products);
                }
                catch (Exception ex)
                {
                    // TODO: log exception here
                }
            }

            await CreateOrUpdateAsync(marketIds, products);
        }

        private async Task CreateOrUpdateAsync(string[] marketIds, List<Product> products)
        {
            foreach (var marketId in marketIds)
            {
                string[] productNames = products.Select(it => it.Name).ToArray();

                var _products = await _productRepository.GetAsync(marketId, productNames);
                products.AddRange(_products);

                _products = products.Distinct(new ProductComparer()).ToArray();

                await _productRepository.CreateAsync(_products.Where(it => it.Id == null).ToArray());
            }
        }
    }
}

