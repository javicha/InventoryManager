using EventBus.Messages.Events.Products;
using MassTransit;

namespace Accounting.API.EventBusConsumer
{
    /// <summary>
    /// Class that is subscribed to the ProductRemovedEvent event.
    /// Only for illustrative purposes. Does not perform any action. It only receives the event to which it is subscribed, and logs it
    /// </summary>
    public class ProductRemovedConsumer : IConsumer<ProductRemovedEvent>
    {
        private readonly ILogger<ProductRemovedConsumer> _logger;

        public ProductRemovedConsumer(ILogger<ProductRemovedConsumer> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Consume(ConsumeContext<ProductRemovedEvent> context)
        {
            _logger.LogInformation($"ProductRemovedConsumer - ProductRemovedEvent consumed - {Newtonsoft.Json.JsonConvert.SerializeObject(context.Message)}");

            /*
             * Note
             * Some examples of functionality at this point: 
             *  - We could implement CQRS, creating a command from the event, and execute it, in order to, for example, update the accounting database 
             *  - Or we could send an email to a list of users with a certain management role.
             *  - Display a notification in our front in real time
             */

            return Task.CompletedTask;
        }
    }
}
