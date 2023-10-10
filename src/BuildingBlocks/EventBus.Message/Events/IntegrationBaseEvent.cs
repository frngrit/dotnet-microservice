namespace EventBus.Message.Events
{
	public class IntegrationBaseEvent
	{
		public Guid Id { get; set; }
		public DateTime CreateDate { get; set; }

		public IntegrationBaseEvent()
		{
			Id = new();
            CreateDate = DateTime.Now;
        }
	}
}

