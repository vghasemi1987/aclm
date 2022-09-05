using DomainContracts.Commons;
using DomainEntities.AclFilesUploadAggregate;
using KendoHelper;
using System.Threading.Tasks;

namespace DomainContracts.AclFilesUploadAggregate
{
	public interface IAclFilesUploadRepository : IRepository<AclFilesUpload>, IAsyncRepository<AclFilesUpload>
	{
		DataSourceResult GetList(DataSourceRequest request);
		Task<AclFilesUpload> FindByIdAsync(int id);
	}
}
