using DomainContracts.Commons;
using DomainEntities.Commons;
using DomainEntities.ExcelFormatAggregate;
using KendoHelper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.ExcelFormatAggregate
{
	public interface IExcelFormatHeaderRepository : IRepository<ExcelFormatHeader>, IAsyncRepository<ExcelFormatHeader>
	{
		DataSourceResult GetList(DataSourceRequest request);
		Task<ExcelFormatHeader> FindByIdAsync(int id);
		ExcelFormatHeader FindById(int id);
		Task<List<DropDownDto>> GetDropDownList();
		void Delete(List<int> idList);
	}
}
