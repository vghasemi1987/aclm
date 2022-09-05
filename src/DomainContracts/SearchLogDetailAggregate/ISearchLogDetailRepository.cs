using DomainContracts.Commons;
using DomainEntities.SearchLogAggregate;
using KendoHelper;
using System.Collections.Generic;

namespace DomainContracts.ServiceAggregate
{
	public interface ISearchLogDetailRepository : IRepository<SearchLogDetail>, IAsyncRepository<SearchLogDetail>
	{
		DataSourceResult GetList(DataSourceRequest request);
		DataSourceResult GetAbundanceReport(DataSourceRequest request);
		DataSourceResult GetSajayaReport(DataSourceRequest request, SearchLogFilter filter);
		List<SearchLogDetailListDto> GetByLetterIdentifier(string letterIdentifier);
	}
}
