namespace DomainEntities.PolicyAggregate
{
	public class PolicyListDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public bool RecordStatus { get; set; }
		public int? ProtocolId { get; set; }
		public string ProtocolTitle { get; set; }
		public string Description { get; set; }
	}
}
