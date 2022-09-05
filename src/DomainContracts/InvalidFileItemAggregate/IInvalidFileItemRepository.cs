using DomainContracts.Commons;
using DomainEntities.InvalidFileItemAggregate;
using KendoHelper;

namespace DomainContracts.InvalidFileItemAggregate
{
	public interface IInvalidFileItemRepository : IRepository<InvalidFileItem>, IAsyncRepository<InvalidFileItem>
	{
		DataSourceResult GetList(DataSourceRequest request);
		void DeleteByAclFileUploadId(int id);
	}
}
