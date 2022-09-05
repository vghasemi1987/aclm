using DomainContracts.PolicyAggregate;
using DomainEntities.Commons;
using DomainEntities.PolicyAggregate;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.PolicyAggregate
{
	public class PositionRepository : EfRepository<Policy>, IPolicyRepository
	{
		private readonly DbSet<Policy> _dbSet;
		public PositionRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<Policy>();
		}

		public DataSourceResult GetList(DataSourceRequest request)
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				 .Select(o => new PolicyListDto
				 {
					 Id = o.Id,
					 Description = o.Description,
					 ProtocolId = o.ProtocolId,
					 ProtocolTitle = o.Protocol.Name,
					 Title = o.Title
				 })
				 .AsNoTracking()
				 .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
		}

		public Task<Policy> FindByIdAsync(int id)
		{
			return _dbSet.Where(q => q.Id == id).FirstOrDefaultAsync();
		}
	}
}
