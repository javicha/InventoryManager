using Coravel.Invocable;
using EventBus.Messages.Events.Products;
using Inventory.Application.Contracts.Persistence;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Inventory.Infrastructure.ScheduledJobs
{
    /// <summary>
    /// Job that is responsible for checking the products that have expired, and firing the ProductRemovedEvent event if so 
    /// </summary>
    public class ProductsExpiredJob : IInvocable
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductsExpiredJob> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public ProductsExpiredJob(IProductRepository productRepository, ILogger<ProductsExpiredJob> logger, IPublishEndpoint publishEndpoint)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        public async Task Invoke()
        {
            Thread.Sleep(60000); //Note For illustrative purposes only. Notification is launched 1 minute after launching the app

            _logger.LogInformation("ProductsExpiredJob started");

            //get the expired products in the current day
            var productsExpired = await _productRepository.GetProductsExpiredByDateAsync(DateTime.Today, DateTime.Today);

            //Note We could optimize performance by publishing the events in parallel, using Parallel.ForEach
            productsExpired.ForEach(async p =>
            {
                // Ceate ProductExpiredEvent -> Set product info on eventMessage
                // Send checkout event to RabbitMQ
                var eventMessage = new ProductExpiredEvent(p.Name, p.Reference, p.ExpirationDate.Value);
                await _publishEndpoint.Publish(eventMessage);
            });

            _logger.LogInformation($"ProductsExpiredJob finished. Notify {productsExpired.Count} products expired");
        }
    }
}
