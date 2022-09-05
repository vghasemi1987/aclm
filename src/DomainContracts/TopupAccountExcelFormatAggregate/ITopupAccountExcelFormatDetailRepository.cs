using DomainContracts.Commons;
using DomainEntities.TopupAccountExcelFormatAggregate;
using KendoHelper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.TopupAccountExcelFormatAggregate
{
	public interface ITopupAccountExcelFormatDetailRepository : IRepository<TopupAccountExcelFormatDetail>, IAsyncRepository<TopupAccountExcelFormatDetail>
	{
		DataSourceResult GetList(DataSourceRequest request);
		Task<List<TopupAccountExcelFormatDetail>> FindByHeaderIdAsync(int id);
		List<TopupAccountExcelFormatDetail> FindByHeaderId(int id);
		void Delete(List<TopupAccountExcelFormatDetail> entityList);
		void Delete(List<int> ids);
	}
}
