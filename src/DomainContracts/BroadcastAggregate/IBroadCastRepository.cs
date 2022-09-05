using DomainContracts.Commons;
using DomainEntities.BroadcastAggregate;
using KendoHelper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.BroadcastAggregate
{
	public interface IBroadCastRepository : IRepository<BroadCast>, IAsyncRepository<BroadCast>
	{
		Task<IList<BroadCast>> ListAllBroadCast();
		Task<BroadCast> GetById(int? id);
	}
	public interface IReferralBroadCastRepository : IRepository<ReferralBroadCast>, IAsyncRepository<ReferralBroadCast>
	{
		Task<IList<ReferralBroadCast>> GetReferralJoinBoard();
		Task<DataSourceResult>  GetList(DataSourceRequest request, int dstUserID);
		Task<IEnumerable<ReferralBroadCast>> GetListAllAdameMoshahede();
		Task<ReferralBroadCast> GetById(int? id);
		Task<ReferralBroadCast> GetByIdUpdate(int? id);
		Task<List<ReferralBroadCast>> GetListtById(int? id);
		public Task<List<ReferralBroadCast>> GetListtByIdDstUser(int? id);
		Task<ReferralBroadCast> GetReferralBroadCastFromBroadCast(int id);
		Task<IEnumerable<ReferralBroadCast>> GetListAllTakhir();
		Task<int> CountUnRead5DayLast();
	}
}