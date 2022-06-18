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
        public string Name { get; private set; }

        /// <summary>
        /// User who removes the product
        /// </summary>
        public string UserDeleted { get; private set; }

        /// <summary>
        /// Date deleted
        /// </summary>
        public DateTime DateDeleted { get; private set; }

        public ProductRemovedEvent(string name, string userDeleted, DateTime dateDeleted)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            UserDeleted = userDeleted ?? throw new ArgumentNullException(nameof(userDeleted));
            DateDeleted = dateDeleted;
        }
    }
}
