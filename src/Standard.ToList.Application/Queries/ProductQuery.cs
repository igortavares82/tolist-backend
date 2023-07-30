using System;
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

		public ProductQuery(IProductRepository productRepository, IMarketRepository marketRepository)
		{
            _productRepository = productRepository;
            _marketRepository = marketRepository;
		}

        public async Task<Result<IEnumerable<Product>>> GetAsync()
        {
            var result = await _productRepository.GetAsync(it => it.Name != null);
            return new Result<IEnumerable<Product>>(result, ResultStatus.Success);
        }

        public async Task<Result<ResultViewModel>> GetAsync(ProductRequest request)
        {
            var markets = _marketRepository.GetAsync(it => request.MarketIds.Contains(it.Id) && it.IsEnabled == true)
                                           .Result
                                           .ToArray();

            string[] notFound = Array.Empty<string>();
            List<Product> products = new List<Product>();

            foreach (var market in markets)
            {
                var searchResult = _productRepository.GetAsync(market.Id, request.Names).Result.ToList();

                var found = searchResult.Where(it => request.Names
                                                            .Any(_it => it.Name.ToLower().Contains(_it.ToLower())))
                                                            .ToList();

                notFound = request.Names
                                  .Where(it => !found.Exists(_it => _it.Name.ToLower().Contains(it.ToLower())))
                                  .ToArray();

                if (notFound.Any())
                {
                    var productsNotFound = notFound.Select(it => new MissingProduct(it, market.Id)).ToArray();
                    await _productRepository.CreateAsync(productsNotFound);
                }

                products?.AddRange(searchResult);
            }
       
            var result = new ResultViewModel(products.ToArray(), markets.ToArray(), notFound);
            return new Result<ResultViewModel>(result, ResultStatus.Success);
        }
    }
}
