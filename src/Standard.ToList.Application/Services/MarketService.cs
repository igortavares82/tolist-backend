using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Standard.ToList.Application.Extensions;
using Standard.ToList.Model.Aggregates.Configuration;
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

        public async Task<Worker> SearchMissingProductsAsync(Worker worker)
        {
            var products = new List<Product>(); 
            var markets = _marketRepository.GetAsync(it => it.IsEnabled == true).Result.ToList();
            var marketIds = markets.ToArray(it => it.Id);
            var missingProducts = await _productRepository.GetAsync<MissingProduct>(it => marketIds.Contains(it.MarketId), worker.Page);

            foreach (var missingProduct in missingProducts.Data)
            {
                try
                {
                    var tasks = markets.ToArray(it =>
                    {
                        var market = _marketFactory.Instance(it);
                        return market.SearchAsync(missingProduct.Name);
                    });

                    Task.WaitAll(tasks);

                    await _productRepository.DeleteAsync<MissingProduct>(it => it.Id == missingProduct.Id);
                    var _products = tasks.SelectMany(it => it.Result).ToArray();
                    products.AddRange(_products);
                }
                catch (Exception ex)
                {
                    // TODO: log exception here
                }
            }

            if (products.Any())
                await _productRepository.CreateAsync(products.ToArray());

            return worker;
        }

        public async Task<Worker> SearchOutdatedProductsAsync(Worker worker)
        {
            var markets = await _marketRepository.GetAsync(it => it.IsEnabled == true, worker.Page);

            foreach(var market in markets.Data)
            {
                try
                {
                    var products = await _productRepository.GetAsync(market.Id, 1, 10);
                    var tasks = products.ToArray(it =>
                    {
                        var _market = _marketFactory.Instance(market);
                        return _market.SearchAsync(it.Name);
                    });

                    Task.WaitAll(tasks);

                    var _products = tasks.SelectMany(it => it.Result).ToArray();
                    await UpdateProductAsync(products.ToArray(), _products.ToArray());
                }
                catch (Exception ex)
                {
                    // TODO: log exception here
                }
            }

            return worker;
        }

        private async Task UpdateProductAsync(Product[] products, Product[] _products)
        {
            if (!products.Any() || !_products.Any())
                return;

            foreach (Product prd in products)
            {
                var _prd = _products.FirstOrDefault(it => it.Name == prd.Name);

                if (_prd == null)
                    continue;

                prd.Update(_prd.Name, _prd.Description, _prd.Price);
            }

            await _productRepository.UpdateAsync(products);
        }
    }
}

