namespace DomainEntities.SystemsAggregate
{
	public class SystemsListDto
	{
		public int Id { get; set; }
		public bool RecordStatus { get; set; }
		public string SystemName { get; set; }
		public string IpAddress { get; set; }
		public string Description { get; set; }
		public int? AccessibilityLevelId { get; set; }
		public string AccessibilityLevelTitle { get; set; }
		public int? InformationAccessibilityLevelId { get; set; }
		public string InformationAccessibilityLevelTitle { get; set; }
		public int KindId { get; set; }
		public int? ImportanceFactor { get; set; }
		public int? PersonelCode { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int? DepartmentId { get; set; }
		public string DepartmentTitle { get; set; }
		public string IpFrom { get; set; }
		public string IpTo { get; set; }
	}
}
