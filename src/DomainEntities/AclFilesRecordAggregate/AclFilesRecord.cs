using DomainEntities.AclFilesUploadAggregate;
using DomainEntities.AddressTypeAggregate;
using DomainEntities.Commons;
using DomainEntities.RouterAggregate;

namespace DomainEntities.AclFilesRecordAggregate
{
	public class AclFilesRecord : Entity<int>
	{
		public string SourceIp { get; set; }
		public string DestinationIp { get; set; }
		public string Action { get; set; }
		public string SourceIp2 { get; set; }
		public string DestinationIp2 { get; set; }
		public string SourcePort { get; set; }
		public string DestinationPort { get; set; }
		public string Protocol { get; set; }
		public string RouterNo { get; set; }
		public int? RouterId { get; set; }
		public Router Router { get; set; }
		public int? SourceAddressTypeId { get; set; }
		public AddressType SourceAddressType { get; set; }
		public int? DestinationAddressTypeId { get; set; }
		public AddressType DestinationAddressType { get; set; }
		public int AclFilesUploadId { get; set; }
		public AclFilesUpload AclFilesUpload { get; set; }
	}
}