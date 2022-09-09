using DomainEntities.ApplicationUserAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.ApplicationUserAggregate
{
	public interface IApplicationUserRepository
	{
		Task<List<ApplicationUser>> GetUserList();
		//Task<List<ApplicationUser>> GetUserListWithRole(string roleName);
		Task<ApplicationUser> GetUserByBranchAndDepartment(int branchId, int departmentId);
	}
}
