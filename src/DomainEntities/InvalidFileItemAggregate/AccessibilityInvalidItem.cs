using DomainEntities.AclFilesUploadAggregate;
using DomainEntities.Commons;

namespace DomainEntities.InvalidFileItemAggregate
{
	public class InvalidFileItem : Entity<int>
	{
		public string InvalidItemTitle { get; set; }
		public int LineNumber { get; set; }
		public int AclFilesUploadId { get; set; }
		public AclFilesUpload AclFilesUpload { get; set; }
	}
}