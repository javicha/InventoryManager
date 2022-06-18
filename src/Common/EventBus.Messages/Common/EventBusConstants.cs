namespace EventBus.Messages.Common
{
    /// <summary>
    /// Constants with the correspondence between the events and the queue in which they are published/consumed
    /// </summary>
    public static class EventBusConstants
	{
		public const string ProductRemovedQueue = "ProductRemoved.Queue";
        public const string ProductExpiredQueue = "ProductExpired.Queue";
    }
}
