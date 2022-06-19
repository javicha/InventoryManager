namespace EventBus.Messages.Events.Products
{
    /// <summary>
    /// Event that is fired when a product expires
    /// </summary>
    public class ProductExpiredEvent : IntegrationBaseEvent
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
        /// Product expiration date
        /// </summary>
        public DateTime ExpirationDate { get; private set; }

        public ProductExpiredEvent(string? name, string? reference, DateTime expirationDate)
        {
            Name = name;
            Reference = reference;
            ExpirationDate = expirationDate;
        }
    }
}
