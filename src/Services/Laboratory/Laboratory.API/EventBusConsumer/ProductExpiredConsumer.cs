using EventBus.Messages.Events.Products;
using MassTransit;
using System.Text.Json;

namespace Laboratory.API.EventBusConsumer
{
    /// <summary>
    ///  Class that is subscribed to the ProductExpiredEvent event
    ///  Only for illustrative purposes. Does not perform any action. It only receives the event to which it is subscribed, and logs it
    /// </summary>
    public class ProductExpiredConsumer : IConsumer<ProductExpiredEvent>
    {
        private readonly ILogger<ProductExpiredConsumer> _logger;

        public ProductExpiredConsumer(ILogger<ProductExpiredConsumer> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Consume(ConsumeContext<ProductExpiredEvent> context)
        {
            _logger.LogInformation($"ProductExpiredConsumer - ProductExpiredEvent consumed - {JsonSerializer.Serialize(context.Message)}");

            /*
             * Note
             * Some examples of functionality at this point: 
             *  - We could implement CQRS, creating a command from the event, and execute it, in order to, for example, update the laboratory database so that they cannot use expired products. 
             *  - Or we could send an email to a list of users with a certain management role.
             *  - Display a notification in our front in real time
             */

            return Task.CompletedTask;
        }
    }
}
