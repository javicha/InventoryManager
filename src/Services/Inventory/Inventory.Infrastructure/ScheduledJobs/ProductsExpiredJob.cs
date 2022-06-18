using AutoMapper;
using Coravel.Invocable;
using Inventory.Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;

namespace Inventory.Infrastructure.ScheduledJobs
{
    /// <summary>
    /// Job that is responsible for checking the products that have expired, and firing the ProductRemovedEvent event if so 
    /// </summary>
    public class ProductsExpiredJob : IInvocable
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsExpiredJob> _logger;

        public ProductsExpiredJob(IProductRepository productRepository, IMapper mapper, ILogger<ProductsExpiredJob> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke()
        {
            //get the expired products in the current day
            var productsExpired = await _productRepository.GetProductsExpiredByDateAsync(DateTime.Today, DateTime.Today);

            if(productsExpired != null)
            {
                //Note We could optimize performance by publishing the events in parallel, using Parallel.ForEach
                productsExpired.ForEach(p =>
                {
                    //TODO publish
                });
            }
        }
    }
}
