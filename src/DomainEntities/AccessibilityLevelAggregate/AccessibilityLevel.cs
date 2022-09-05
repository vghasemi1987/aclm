using DomainEntities.Commons;

namespace DomainEntities.AccessibilityLevelAggregate
{
	public class AccessibilityLevel : Entity<int>
	{
		public string Title { get; set; }
	}
}