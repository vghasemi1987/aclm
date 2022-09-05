using DomainContracts.InvalidFileItemAggregate;
using DomainEntities.Commons;
using DomainEntities.InvalidFileItemAggregate;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Infrastructure.Data.InvalidFileItemAggregate
{
	public class InvalidFileItemRepository : EfRepository<InvalidFileItem>, IInvalidFileItemRepository
	{
		private readonly DbSet<InvalidFileItem> _dbSet;
		public InvalidFileItemRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<InvalidFileItem>();
		}

		public DataSourceResult GetList(DataSourceRequest request)
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				 .Select(o => new InvalidFileItemListDto
				 {
					 Id = o.Id,
					 InvalidItemTitle = o.InvalidItemTitle,
					 LineNumber = o.LineNumber,
					 AclFilesUploadId = o.AclFilesUploadId,
					 AclFilesUploadTitle = o.AclFilesUpload.FileName
				 })
				 .AsNoTracking()
				 .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
		}

		public void DeleteByAclFileUploadId(int id)
		{
			//TODO Implementing REC.usp_DeleteInvalidFileItem
			throw new NotImplementedException();
		}
	}
}
