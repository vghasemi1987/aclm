using DomainEntities.Commons;

namespace DomainEntities.DepartmentAggregate
{
	public class Department : Entity<int>
	{
		public string Name { get; set; }
	}
}