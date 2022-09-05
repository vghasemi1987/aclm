using DomainContracts.Commons;
using DomainEntities.AccessibilityAggregate;
using KendoHelper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainContracts.AccessibilityAggregate
{
	public interface IAccessibilityRepository : IRepository<Accessibility>, IAsyncRepository<Accessibility>
	{
		DataSourceResult GetList(DataSourceRequest request);
		IQueryable<Accessibility> GetListAsQueryable();
		Task<Accessibility> FindByIdAsync(int id);
		void DeleteTemp(int userId);
		void ConfirmAccessibilities(int userId);
		void DeleteByRouterId(int routerId);

		DataSourceResult GetAccessibilityReportByCount(DataSourceRequest request, int count);
		List<AccessibilityReportByCount> GetAccessibilityReportByCount(int count);
		DataSourceResult GetReportByFilter(DataSourceRequest request, AccessibilityFilter filter);
	}
}
