using DomainContracts.Commons;
using DomainEntities.Commons;
using DomainEntities.TopupAccountExcelFormatAggregate;
using KendoHelper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.TopupAccountExcelFormatAggregate
{
	public interface ITopupAccountExcelFormatHeaderRepository : IRepository<TopupAccountExcelFormatHeader>, IAsyncRepository<TopupAccountExcelFormatHeader>
	{
		DataSourceResult GetList(DataSourceRequest request);
		Task<TopupAccountExcelFormatHeader> FindByIdAsync(int id);
		TopupAccountExcelFormatHeader FindById(int id);
		Task<List<DropDownDto>> GetDropDownList();
		void Delete(List<int> idList);
	}
}
