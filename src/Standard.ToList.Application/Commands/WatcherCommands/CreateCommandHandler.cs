using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Standard.ToList.Application.Extensions;
using Standard.ToList.Model.Aggregates.Products;
using Standard.ToList.Model.Aggregates.Watchers;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.Constants;
using Standard.ToList.Model.ViewModels.Watchers;

namespace Standard.ToList.Application.Commands.WatcherCommands
{
    public class CreateCommandHandler : IRequestHandler<CreateCommand, Result<WatcherViewModel>>
    {
        private readonly IWatcherRepository _watcherRepository;
        private readonly IProductRepository _productRepository;

        public CreateCommandHandler(IWatcherRepository watcherRepository, IProductRepository productRepository)
        {
            _watcherRepository = watcherRepository;
            _productRepository = productRepository;
        }

        public async Task<Result<WatcherViewModel>> Handle(CreateCommand request, CancellationToken cancelationToken)
        {
            var result = new Result<WatcherViewModel>(null);
            var watcher = await _watcherRepository.GetOneAsync(request.Expression);

            if (watcher != null)
                return result.SetResult(ResultStatus.UnprosseableEntity, Messages.Exists.SetMessageValues("Watcher"));

            var product = await _productRepository.GetOneAsync(it => it.Id == request.ProductId &&
                                                                     it.IsEnabled == true);

            if (product == null)
                return result.SetResult(ResultStatus.NotFound, Messages.NotFound.SetMessageValues("Product"));

            watcher = new Watcher(request.UserId,
                                  product.Name,
                                  product.Id,
                                  product.Price,
                                  product.Price,
                                  request.Desired);

            await _watcherRepository.CreateAsync(watcher);
            return result.SetResult(new WatcherViewModel(watcher), ResultStatus.Created);
        }
    }
}