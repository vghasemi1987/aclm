using DomainContracts.Commons;
using DomainEntities.BroadcastAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.BroadcastAggregate
{
	public interface IProtectionOfficeRepository : IRepository<ProtectionOffice>, IAsyncRepository<ProtectionOffice>
	{
		Task<ProtectionOffice> GetById(int? id);
		Task<List<ProtectionOffice>> GetProtectionOfficeAll();
	}
}
