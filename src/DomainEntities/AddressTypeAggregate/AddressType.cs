using DomainEntities.Commons;

namespace DomainEntities.AddressTypeAggregate
{
	public class AddressType : Entity<int>
	{
		public string Title { get; set; }
		public string Description { get; set; }
	}
}