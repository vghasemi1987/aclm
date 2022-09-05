namespace DomainEntities.AccessibilityAggregate
{
	public class AccessibilityFilter
	{
		public string SourceIp { get; set; }
		public int? SourceSystemId { get; set; }
		public string DestinationIp { get; set; }
		public int? DestinationSystemId { get; set; }
		public int? ServiceId { get; set; }
		public int? DestinationServiceId { get; set; }
		public int? RequestingUserId { get; set; }
		public int? ConfirmUserId { get; set; }
		public int? UserDepartmentId { get; set; }
		public int? RequestDepartmentId { get; set; }
		public int? AccessibilityTypeId { get; set; }
	}
}
