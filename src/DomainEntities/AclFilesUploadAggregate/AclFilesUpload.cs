using DomainEntities.AclFilesRecordAggregate;
using DomainEntities.Commons;
using DomainEntities.InvalidFileItemAggregate;
using DomainEntities.RouterAggregate;
using System.Collections.Generic;

namespace DomainEntities.AclFilesUploadAggregate
{
	public class AclFilesUpload : Entity<int>
	{
		public string FileName { get; set; }
		public string Description { get; set; }
		public int? RouterId { get; set; }
		public Router Router { get; set; }

		public List<AclFilesRecord> AclFilesRecords { get; set; } = new List<AclFilesRecord>();
		public List<InvalidFileItem> InvalidFileItems { get; set; } = new List<InvalidFileItem>();
	}
}