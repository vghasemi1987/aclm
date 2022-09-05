namespace DomainEntities.NetworkDiagramAggregate
{
	public class NetworkDiagramFilter
	{
		public int? SystemId { get; set; }
		public int? ServiceId { get; set; }
		public string Address { get; set; }
		public int? RouterId { get; set; }
		public int? Level { get; set; }
		public int AccessCount { get; set; }
		public string SourceIp { get; set; }
		public string DestinationIp { get; set; }
		public int UserId { get; set; }
		public int? PersonnelCode { get; set; }
		public string RecoveryDate { get; set; }
		public int? ImportanceFactor { get; set; }
	}
}
