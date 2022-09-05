using DomainContracts.TestOptionsAggregate;
using DomainEntities.TestOptionsAggregate;
using Infrastructure.Data.Commons;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.TestOptionsAggregate
{
	public class TestOptionRepository : EfRepository<TestOptions>, ITestOptionRepository
	{
		private DbSet<TestOptions> _dbSet { get; }

		public TestOptionRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<TestOptions>();
		}

		public async Task<TestOptions> FindByIdAsync(short id)
		{
			return await _dbSet.FindAsync(id);
		}

		public Task<List<TestOptions>> GetByTableId(TestOptionsTable id)
		{
			return _dbSet
				.Where(o => o.TableId == id)
				.AsNoTracking()
				.ToListAsync();
		}
	}
}