using AutoMapper;
using Inventory.Application.Contracts.Persistence;
using Inventory.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Features.Products.Commands.AddProduct
{
    /// <summary>
    /// CQRS pattern: AddProductCommand command handler
    /// </summary>
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, NewProductDTO>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddProductCommandHandler> _logger;


        public AddProductCommandHandler(IProductRepository productRepository, IMapper mapper, ILogger<AddProductCommandHandler> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        /// <summary>
        /// Add a product to the inventory
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Information about the new product just added</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<NewProductDTO> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = _mapper.Map<Product>(request);
            var newProduct = await _productRepository.AddAsync(productEntity, "javier.val"); //TODO leer usuario del token

            _logger.LogInformation($"Product {newProduct.Id} is successfully created.");

            return _mapper.Map<NewProductDTO>(newProduct);
        }
    }
}
