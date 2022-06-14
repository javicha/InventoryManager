using AutoMapper;
using Inventory.Application.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Features.Products.Queries.GetAllProducts
{
    /// <summary>
    /// CQRS pattern: GetAllProductsQuery query handler
    /// </summary>
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDTO>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllProductsQueryHandler> _logger;

        public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper, ILogger<GetAllProductsQueryHandler> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        /// <summary>
        /// Get the list with all the products in the inventory
        /// </summary>
        /// <param name="request">No params needed in this case</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The list with all the products in the inventory</returns>
        public async Task<List<ProductDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start handler - GetAllProductsQueryHandler");

            var productList = await _productRepository.GetAllProductsPagAsync(request.Page, request.Size, request.FilterText);
            return _mapper.Map<List<ProductDTO>>(productList);
        }
    }
}
