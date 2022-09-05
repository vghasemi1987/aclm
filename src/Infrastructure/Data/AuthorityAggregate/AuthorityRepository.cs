using DomainContracts.AuthorityAggregate;
using DomainEntities.AuthorityAggregate;
using DomainEntities.Commons;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.AuthorityAggregate
{
	public class AuthorityRepository : EfRepository<Authority>, IAuthorityRepository
	{
		private readonly DbSet<Authority> _dbSet;
		public AuthorityRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<Authority>();
		}

		public DataSourceResult GetList(DataSourceRequest request)
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				 .Select(o => new AuthorityListDto
				 {
					 Id = o.Id,
					 Description = o.Description,
					 Title = o.Title
				 })
				 .AsNoTracking()
				 .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
		}

		public Task<Authority> FindByIdAsync(int id)
		{
			return _dbSet.Where(p => p.Id == id).FirstOrDefaultAsync();
		}

		public List<DropDownDto> GetDropDownList()
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				 .Select(o => new DropDownDto
				 {
					 Id = o.Id,
					 Title = o.Title
				 })
				 .AsNoTracking().ToList();
		}
	}
}
