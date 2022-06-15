namespace EventBus.Messages.Events
{
    /// <summary>
    /// Common properties for all events
    /// </summary>
    public class IntegrationBaseEvent
    {
		public Guid Id { get; private set; }

		public DateTime CreationDate { get; private set; }


		public IntegrationBaseEvent()
		{
			Id = Guid.NewGuid();
			CreationDate = DateTime.UtcNow;
		}

		public IntegrationBaseEvent(Guid id, DateTime createDate)
		{
			Id = id;
			CreationDate = createDate;
		}
	}
}
