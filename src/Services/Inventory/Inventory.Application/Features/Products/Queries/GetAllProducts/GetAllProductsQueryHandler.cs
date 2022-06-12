using AutoMapper;
using Inventory.Application.Contracts.Persistence;
using MediatR;

namespace Inventory.Application.Features.Products.Queries.GetAllProducts
{
    /// <summary>
    /// CQRS pattern: GetAllProductsQuery query handler
    /// </summary>
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDTO>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get the list with all the products in the inventory
        /// </summary>
        /// <param name="request">No params needed in this case</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The list with all the products in the inventory</returns>
        public async Task<List<ProductDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productRepository.GetAllAsync();
            return _mapper.Map<List<ProductDTO>>(productList);
        }
    }
}
