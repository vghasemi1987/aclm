using DomainContracts.ApplicationUserAggregate;
using DomainEntities.ApplicationUserAggregate;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Infrastructure.Data.ApplicationUserAggregate
{
	public class ApplicationUserActivityRepository : EfRepository<ApplicationUserActivity>, IApplicationUserActivityRepository
	{
		private DbSet<ApplicationUserActivity> _dbSet { get; }

		public ApplicationUserActivityRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<ApplicationUserActivity>();
		}

		//public async Task<ApplicationSystem> FindByIdAsync(int id)
		//{
		//    return await DbSet.FindAsync(id);
		//}

		public DataSourceResult GetList(DataSourceRequest request)
		{
			return _dbSet
				//.Select(o => new ApplicationSystemListDto
				//{
				//    Id = o.Id,
				//    Title= o.Title,
				//    RegDateTime = o.RegDateTime,
				//    PublishDate = o.PublishDate,
				//    CreatedDate = o.CreatedDate,
				//    IpAddress = o.IpAddress,
				//    CreatorDepartment = o.CreatorDepartment.Title,
				//    RegUserId = o.RegUserId,
				//    ApplicationSystemType = o.ApplicationSystemType.Title
				//})
				.AsNoTracking()
				.ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
		}

		public Task<List<ApplicationUserActivity>> GetDropDownList()
		{
			return _dbSet
				.AsNoTracking()
				.ToListAsync();

		}

		
	}
}