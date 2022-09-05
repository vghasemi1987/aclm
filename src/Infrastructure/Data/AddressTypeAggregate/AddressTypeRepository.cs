using DomainContracts.AddressTypeAggregate;
using DomainEntities.AddressTypeAggregate;
using DomainEntities.Commons;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.AddressTypeAggregate
{
	public class AddressTypeRepository : EfRepository<AddressType>, IAddressTypeRepository
	{
		private readonly DbSet<AddressType> _dbSet;
		public AddressTypeRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<AddressType>();
		}

		public DataSourceResult GetList(DataSourceRequest request)
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				 .Select(o => new AddressTypeListDto
				 {
					 Id = o.Id,
					 Description = o.Description,
					 Title = o.Title
				 })
				 .AsNoTracking()
				 .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
		}

		public Task<AddressType> FindByIdAsync(int id)
		{
			return _dbSet.Where(p => p.Id == id).FirstOrDefaultAsync();
		}
	}
}
