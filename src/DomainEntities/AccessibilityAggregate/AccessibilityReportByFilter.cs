namespace DomainEntities.AccessibilityAggregate
{
	public class AccessibilityReportByFilter
	{
		public int Id { get; set; }
		public string SourceIp { get; set; }
		public string SourceSystemName { get; set; }
		public string SourcePort { get; set; }
		public string DestinationIp { get; set; }
		public string DestinationSystemName { get; set; }
		public string DestinationPort { get; set; }
		public string RequestingUserName { get; set; }
		public string ConfirmerUserName { get; set; }
		public string RequestDepartment { get; set; }
		public string AccessType { get; set; }
	}
}
