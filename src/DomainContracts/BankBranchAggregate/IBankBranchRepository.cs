using DomainContracts.Commons;
using DomainEntities.BankBranchAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.BankBranchAggregate
{
	public interface IBankBranchRepository : IRepository<BankBranch>, IAsyncRepository<BankBranch>
	{
		Task<List<BankBranchDropDownDto>> GetDropDownBranchHeadsList();
		Task<List<BankBranchDropDownDto>> GetDropDownBankBranchesList();
		//Task<List<BankBranchDropDownDto>> GetDropDownList();
		Task<BankBranch> GetBranchByName(string branchName);
	}
}
