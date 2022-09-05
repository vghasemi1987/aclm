using DomainContracts.AclFilesUploadAggregate;
using DomainEntities.AclFilesUploadAggregate;
using DomainEntities.Commons;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.AclFilesUploadAggregate
{
	public class AclFilesUploadRepository : EfRepository<AclFilesUpload>, IAclFilesUploadRepository
	{
		private readonly DbSet<AclFilesUpload> _dbSet;
		public AclFilesUploadRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<AclFilesUpload>();
		}

		public DataSourceResult GetList(DataSourceRequest request)
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				 .Select(o => new AclFilesUploadListDto
				 {
					 Id = o.Id,
					 Description = o.Description,
					 FileName = o.FileName,
					 RouterId = o.RouterId,
					 RouterTitle = o.Router.Name
				 })
				 .AsNoTracking()
				 .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
		}

		public Task<AclFilesUpload> FindByIdAsync(int id)
		{
			return _dbSet.Where(p => p.Id == id).FirstOrDefaultAsync();
		}
	}
}
