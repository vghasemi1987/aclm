using DomainEntities.AclFilesUploadAggregate;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DomainContracts.AclFilesUploadAggregate
{
	public interface IAclFileUploadService
	{
		Task UploadAclFile(AclFilesUpload model, IFormFile file);
	}
}
