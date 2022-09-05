using DomainContracts.Commons;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Data.Commons
{
	public class UnitOfWork : IUnitOfWork
	{
		private ServerAccessibilityMonitorContext _dbContext;
		public UnitOfWork(ServerAccessibilityMonitorContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task SaveAsync()
		{
			await _dbContext.SaveChangesAsync();
		}
		public void Save()
		{
			_dbContext.SaveChanges();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (!disposing) return;
			if (_dbContext == null) return;
			_dbContext.Dispose();
			_dbContext = null;
		}
	}
}
