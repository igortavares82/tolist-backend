﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Standard.ToLyst.Application.Extensions;
using Standard.ToLyst.Infrastructure.Searchers;
using Standard.ToLyst.Model.Aggregates.Configuration;
using Standard.ToLyst.Model.Aggregates.Markets;
using Standard.ToLyst.Model.Aggregates.Products;

namespace Standard.ToLyst.Application.Services
{
    public class MarketService : IMarketService
    {
        private readonly IMarketRepository _marketRepository;
        private readonly IProductRepository _productRepository;
        private readonly SearcherFactory _searcherFactory;
        private readonly ILogger<MarketService> _logger;

        public MarketService(IMarketRepository marketRepository,
                             IProductRepository productRepository,
                             SearcherFactory searcherFactory,
                             ILogger<MarketService> logger)
        {
            _marketRepository = marketRepository;
            _productRepository = productRepository;
            _searcherFactory = searcherFactory;
            _logger = logger;
        }

        public async Task RegisterMissingProductAsync(MissingProduct[] missingProducts)
        {
            var _missingProducts = await _productRepository.GetMissingProductsAsync(missingProducts);
            var notFound = missingProducts.Where(it => 
                                           {
                                                return !_missingProducts.ToList()
                                                                        .Exists(_it => it.MarketId == _it.MarketId && 
                                                                                        it.Name == _it.Name);
                                           })
                                          .ToArray();
            
            if (notFound.Any()) 
                await _productRepository.CreateAsync(notFound);
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
                    var market = markets.First(it => it.Id == missingProduct.MarketId);
                    var searcher = _searcherFactory.Instance(market);

                    var _products = await searcher.SearchAsync(missingProduct.Name);
                    products.AddRange(_products);

                    await _productRepository.DeleteAsync<MissingProduct>(it => it.Id == missingProduct.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
            }

            if (products.Any())
                await CreateProductAsync(products.ToArray());

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
                        var _market = _searcherFactory.Instance(market);
                        return _market.SearchAsync(it.Name);
                    });

                    Task.WaitAll(tasks);

                    var _products = tasks.SelectMany(it => it.Result).ToArray();
                    await UpdateProductAsync(products.ToArray(), _products.ToArray());
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
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

                prd.Update(_prd.Name, _prd.Description, _prd.Price, _prd.Quantity, _prd.Unit, _prd.Media);
            }

            await _productRepository.UpdateAsync(products);
        }

        private async Task CreateProductAsync(Product[] products)
        {
            var groupedProducts = products.GroupBy(it => it.Market.Id);

            foreach(var groupedProduct in groupedProducts)
            {
                try 
                {
                    var registeredProducts = _productRepository.GetAsync(groupedProduct.Key, groupedProduct.Select(it => it.Name).ToArray())
                                                               .Result
                                                               .ToList();
                
                    var found = groupedProduct.Where(it => registeredProducts.Exists(_it => _it.Name == it.Name))
                                              .Select(it => 
                                              {
                                                    var product = registeredProducts.First(_it => _it.Name == it.Name);
                                                    product.Update(it.Name, it.Description, it.Price, it.Quantity, it.Unit, it.Media);

                                                    return product;
                                              })
                                              .ToArray();

                    var notFound = groupedProduct.Where(it => !registeredProducts.Exists(_it => _it.Name == it.Name))
                                                .ToArray();

                    _productRepository.UpdateAsync(found);
                    _productRepository.CreateAsync(notFound);
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.Message, ex);
                }
            }
        }
    }
}

