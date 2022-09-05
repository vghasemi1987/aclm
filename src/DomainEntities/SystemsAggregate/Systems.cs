using DomainEntities.AccessibilityLevelAggregate;
using DomainEntities.Commons;
using DomainEntities.DepartmentAggregate;

namespace DomainEntities.SystemsAggregate
{
	public class Systems : Entity<int>
	{
		public string SystemName { get; set; }
		public string IpAddress { get; set; }
		public string Description { get; set; }
		public int? AccessibilityLevelId { get; set; }
		public AccessibilityLevel AccessibilityLevel { get; set; }
		public int? InformationAccessibilityLevelId { get; set; }
		public AccessibilityLevel InformationAccessibilityLevel { get; set; }
		public int KindId { get; set; }
		public int? ImportanceFactor { get; set; }
		public int? PersonelCode { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int? DepartmentId { get; set; }
		public Department Department { get; set; }
		public string IpFrom { get; set; }
		public string IpTo { get; set; }
	}
}