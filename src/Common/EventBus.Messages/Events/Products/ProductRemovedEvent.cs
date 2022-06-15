namespace EventBus.Messages.Events.Products
{
    /// <summary>
    /// Event that is fired when a product is removed from inventory
    /// </summary>
    public class ProductRemovedEvent : IntegrationBaseEvent
    {
        /// <summary>
        /// Product comercial name
        /// </summary>
        public string? Name { get; private set; }
        /// <summary>
        /// Product reference
        /// </summary>
        public string? Reference { get; private set; }

        /// <summary>
        /// User who removes the product
        /// </summary>
        public string UserDeleted { get; private set; }
    }
}
