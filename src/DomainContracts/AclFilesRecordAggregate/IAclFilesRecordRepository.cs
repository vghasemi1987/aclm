using DomainContracts.Commons;
using DomainEntities.AclFilesRecordAggregate;
using KendoHelper;

namespace DomainContracts.AclFilesRecordAggregate
{
	public interface IAclFilesRecordRepository : IRepository<AclFilesRecord>, IAsyncRepository<AclFilesRecord>
	{
		DataSourceResult GetList(DataSourceRequest request, int aclFilesUploadId);

		void TransportAclRecords(int aclFilesUploadID, int userId);
	}
}
