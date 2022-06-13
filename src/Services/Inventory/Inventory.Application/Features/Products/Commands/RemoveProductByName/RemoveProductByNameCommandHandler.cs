using AutoMapper;
using Inventory.Application.Contracts.Persistence;
using Inventory.Application.Exceptions;
using Inventory.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Features.Products.Commands.RemoveProductByName
{
    /// <summary>
    /// CQRS pattern: RemoveProductByNameCommand command handler
    /// </summary>
    public class RemoveProductByNameCommandHandler : IRequestHandler<RemoveProductByNameCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RemoveProductByNameCommandHandler> _logger;

        public RemoveProductByNameCommandHandler(IProductRepository productRepository, IMapper mapper, ILogger<RemoveProductByNameCommandHandler> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(RemoveProductByNameCommand request, CancellationToken cancellationToken)
        {
            var productToDelete = await _productRepository.GetByName(request.ProductName);
            if (productToDelete == null)
            {
                _logger.LogError($"Product {request.ProductName} not found.");
                throw new NotFoundException(nameof(Product), request.ProductName);
            }

            await _productRepository.DeleteAsync(productToDelete);
            _logger.LogInformation($"Product {productToDelete.Id} is successfully deleted.");

            return Unit.Value;
        }
    }
}
