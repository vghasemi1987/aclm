//using DomainEntities.CityAggregate;
using DomainEntities.Commons;
using System.Collections.Generic;

namespace DomainEntities.BankBranchAggregate
{
	public class BankBranch : Entity<int>
	{
		public int? BranchHeadId { get; set; }
		public string Title { get; set; }
		public BankBranch BranchHead { get; set; }
		public IList<BankBranch> SubBranchHeads { get; set; }

		//public int? CityId { get; set; }
		//public City City { get; set; }
	}
}
