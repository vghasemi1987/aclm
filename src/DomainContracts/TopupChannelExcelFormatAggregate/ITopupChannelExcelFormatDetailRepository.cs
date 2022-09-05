using DomainContracts.Commons;
using DomainEntities.TopupChannelExcelFormatAggregate;
using KendoHelper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.TopupChannelExcelFormatAggregate
{
	public interface ITopupChannelExcelFormatDetailRepository : IRepository<TopupChannelExcelFormatDetail>, IAsyncRepository<TopupChannelExcelFormatDetail>
	{
		DataSourceResult GetList(DataSourceRequest request);
		Task<List<TopupChannelExcelFormatDetail>> FindByHeaderIdAsync(int id);
		List<TopupChannelExcelFormatDetail> FindByHeaderId(int id);
		void Delete(List<TopupChannelExcelFormatDetail> entityList);
		void Delete(List<int> ids);
	}
}
