namespace EventBus.Messages.Common
{
    /// <summary>
    /// Constants with the correspondence between the events and the queue in which they are published/consumed
    /// </summary>
    public static class EventBusConstants
	{
		public static readonly string ProductRemovedQueue = "ProductRemoved.Queue";
        public static readonly string ProductExpiredQueue = "ProductExpired.Queue";
    }
}
