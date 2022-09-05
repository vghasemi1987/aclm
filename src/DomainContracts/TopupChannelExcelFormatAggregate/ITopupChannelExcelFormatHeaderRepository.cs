using DomainContracts.Commons;
using DomainEntities.Commons;
using DomainEntities.TopupChannelExcelFormatAggregate;
using KendoHelper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.TopupChannelExcelFormatAggregate
{
	public interface ITopupChannelExcelFormatHeaderRepository : IRepository<TopupChannelExcelFormatHeader>, IAsyncRepository<TopupChannelExcelFormatHeader>
	{
		DataSourceResult GetList(DataSourceRequest request);
		Task<TopupChannelExcelFormatHeader> FindByIdAsync(int id);
		TopupChannelExcelFormatHeader FindById(int id);
		Task<List<DropDownDto>> GetDropDownList();
		void Delete(List<int> idList);
	}
}
