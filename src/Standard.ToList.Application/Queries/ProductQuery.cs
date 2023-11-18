﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Standard.ToList.Model.Aggregates.Lists;
using Standard.ToList.Model.Aggregates.Markets;
using Standard.ToList.Model.Aggregates.Products;
using Standard.ToList.Model.Aggregates.Requests;
using Standard.ToList.Model.Common;
using System.Linq;
using Standard.ToList.Model.ViewModels.Products;

namespace Standard.ToList.Application.Queries
{
	public class ProductQuery : IProductQuery
    {
        private readonly IProductRepository _productRepository;
        private readonly IMarketRepository _marketRepository;
        private readonly IMarketService _marketService;

		public ProductQuery(IProductRepository productRepository, 
                            IMarketRepository marketRepository,
                            IMarketService marketService)
		{
            _productRepository = productRepository;
            _marketRepository = marketRepository;
            _marketService = marketService;
		}

        public async Task<Result<IEnumerable<Product>>> GetAsync()
        {
            var result = await _productRepository.GetAsync(it => it.Name != null);
            return new Result<IEnumerable<Product>>(result, ResultStatus.Success);
        }

        public async Task<Result<ProductSearchViewModel>> GetAsync(ProductRequest request)
        {
            var markets = _marketRepository.GetAsync(it => request.MarketIds.Contains(it.Id) && it.IsEnabled == true)
                                           .Result
                                           .ToArray();

            var products = await _productRepository.GetAsync(request.MarketIds, request.Names, request.Page, request.Order);

            var notFound = request.MarketIds
                                  .Select(it => new Tuple<string, string>(it, request.Names[0]))
                                  .Where(it => !products.Select(_it => _it.Market.Id).ToList().Exists(_it => _it == it.Item1))
                                  .ToArray();

            if (request.Page.Index <= 1 && notFound.Any())
            {
                var productsNotFound = notFound.Select(it => new MissingProduct(it.Item2, it.Item1)).ToArray();
                await _marketService.RegisterMissingProductAsync(productsNotFound);
            }

            var result = new ProductSearchViewModel(products.ToArray(), markets.ToArray(), null);
            return new Result<ProductSearchViewModel>(result, ResultStatus.Success);
        }
    }
}
