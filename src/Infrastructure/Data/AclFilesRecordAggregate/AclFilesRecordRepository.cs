using DomainContracts.AclFilesRecordAggregate;
using DomainContracts.Dapper;
using DomainEntities.AclFilesRecordAggregate;
using DomainEntities.Commons;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Infrastructure.Data.AclFilesRecordAggregate
{
	public class AclFilesRecordRepository : EfRepository<AclFilesRecord>, IAclFilesRecordRepository
	{
		private readonly IDapperRepository _dapperRepository;
		private readonly DbSet<AclFilesRecord> _dbSet;
		public AclFilesRecordRepository(ServerAccessibilityMonitorContext dbContext, IDapperRepository dapperRepository) : base(dbContext)
		{
			_dbSet = dbContext.Set<AclFilesRecord>();
			_dapperRepository = dapperRepository;
		}

		public DataSourceResult GetList(DataSourceRequest request, int aclFilesUploadId)
		{
			return _dbSet.Where(q => q.AclFilesUploadId == aclFilesUploadId &&
							   q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				 .Select(o => new AclFilesRecordListDto
				 {
					 Id = o.Id,
					 Action = o.Action,
					 SourceIp = o.SourceIp,
					 DestinationIp = o.DestinationIp,
					 SourceIp2 = o.SourceIp2,
					 DestinationIp2 = o.DestinationIp2,
					 SourcePort = o.SourcePort,
					 DestinationPort = o.DestinationPort,
					 Protocol = o.Protocol,
					 RouterNo = o.RouterNo,
					 AclFilesUploadId = o.AclFilesUploadId,
					 AclFilesUploadTitle = o.AclFilesUpload.FileName,
					 DestinationAddressTypeId = o.DestinationAddressTypeId,
					 DestinationAddressTypeTitle = o.DestinationAddressType.Title,
					 SourceAddressTypeId = o.SourceAddressTypeId,
					 SourceAddressTypeTitle = o.SourceAddressType.Title
				 })
				 .AsNoTracking()
				 .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
		}

		public void TransportAclRecords(int aclFilesUploadID, int userId)
		{
			_dapperRepository.ExecuteNonQueryProcedure("dbo.uspTransportAclRecords",
			   new
			   {
				   AclFilesUploadID = aclFilesUploadID,
				   UserId = userId
			   });
		}
	}
}
