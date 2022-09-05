namespace DomainEntities.ServiceAggregate
{
	public class ServiceListDto
	{
		public int Id { get; set; }
		public bool RecordStatus { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int? Port { get; set; }
		public int? ProtocolId { get; set; }
		public string ProtocolTitle { get; set; }
		public int? ServiceLevelId { get; set; }
		public string ServiceLevelTitle { get; set; }
	}
}
