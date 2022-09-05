using DomainContracts.Commons;
using DomainEntities.ExcelFormatAggregate;
using KendoHelper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.ExcelFormatAggregate
{
	public interface IExcelFormatDetailRepository : IRepository<ExcelFormatDetail>, IAsyncRepository<ExcelFormatDetail>
	{
		DataSourceResult GetList(DataSourceRequest request);
		Task<List<ExcelFormatDetail>> FindByHeaderIdAsync(int id);
		List<ExcelFormatDetail> FindByHeaderId(int id);
		void Delete(List<ExcelFormatDetail> entityList);
		void Delete(List<int> ids);
	}
}
