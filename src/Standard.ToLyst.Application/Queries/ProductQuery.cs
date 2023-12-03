using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Standard.ToLyst.Model.Aggregates.Lists;
using Standard.ToLyst.Model.Aggregates.Markets;
using Standard.ToLyst.Model.Aggregates.Products;
using Standard.ToLyst.Model.Aggregates.Requests;
using Standard.ToLyst.Model.Common;
using System.Linq;
using Standard.ToLyst.Model.ViewModels.Products;

namespace Standard.ToLyst.Application.Queries
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
                                  .Where(it => markets.Select(_it => _it.Id).Contains(it))
                                  .Select(it => new Tuple<string, string>(it, request.Names[0]))
                                  .Where(it => !products.Select(_it => _it.Market.Id).ToList().Exists(_it => _it == it.Item1))
                                  .ToArray();

            await RegisterMissingProduct(markets, request, notFound);

            var result = new ProductSearchViewModel(products.ToArray(), markets.ToArray(), null);
            return new Result<ProductSearchViewModel>(result, ResultStatus.Success);
        }

        private async Task RegisterMissingProduct(Market[] markets, ProductRequest request, Tuple<string, string>[] notFound)
        {
            if ((request.SetAsMissingProduct && !request.IsAdmin()) || 
                (!request.SetAsMissingProduct && request.Page.Index < 0) || 
                (!request.SetAsMissingProduct && notFound.Length == 0)) 
                return;

            if (request.SetAsMissingProduct)
            {
                notFound = new Tuple<string, string>[0];
                notFound = request.MarketIds
                                  .Where(it => markets.Select(_it => _it.Id).Contains(it))
                                  .Select(it => new Tuple<string, string>(it, request.Names[0]))
                                  .ToArray();
            }

            var missingProducts = notFound.Select(it => new MissingProduct(it.Item2, it.Item1))
                                          .Where(it => it != null)
                                          .ToArray();

            await _marketService.RegisterMissingProductAsync(missingProducts);
        }
    }
}
