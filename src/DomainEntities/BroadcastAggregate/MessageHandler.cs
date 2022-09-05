using DomainEntities.Commons;

namespace DomainEntities.BroadcastAggregate
{
	public class MessageHandler : Entity<int>
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public MessageType MessageType { get; set; }
		public string Address { get; set; }
		public string FileName { get; set; }
	}
	public enum MessageType
	{
		Agent, General
	}
}
