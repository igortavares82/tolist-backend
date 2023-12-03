using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Standard.ToLyst.Application.Extensions;
using Standard.ToLyst.Model.Aggregates.Products;
using Standard.ToLyst.Model.Aggregates.Watchers;
using Standard.ToLyst.Model.Common;
using Standard.ToLyst.Model.Constants;
using Standard.ToLyst.Model.ViewModels.Watchers;

namespace Standard.ToLyst.Application.Commands.WatcherCommands
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