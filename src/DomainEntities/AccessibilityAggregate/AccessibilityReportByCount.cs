namespace DomainEntities.AccessibilityAggregate
{
	public class AccessibilityReportByCount
	{
		public int Id { get; set; }
		public int Count { get; set; }
		public string DestinationIp { get; set; }
		public string DestinationSystemName { get; set; }
	}
}
