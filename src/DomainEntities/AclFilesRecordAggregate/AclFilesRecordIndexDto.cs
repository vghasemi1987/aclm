using DomainEntities.AclFilesUploadAggregate;
using DomainEntities.AddressTypeAggregate;

namespace DomainEntities.AclFilesRecordAggregate
{
	public class AclFilesRecordIndexDto
	{
		public int Id { get; set; }
		public bool RecordStatus { get; set; }
		public int Index { get; set; }
		public string SourceIp { get; set; }
		public string DestinationIp { get; set; }
		public string Action { get; set; }
		public string SourceIp2 { get; set; }
		public string DestinationIp2 { get; set; }
		public string SourcePort { get; set; }
		public string DestinationPort { get; set; }
		public string Protocol { get; set; }
		public string RouterNo { get; set; }
		public int? SourceAddressTypeId { get; set; }
		public AddressType SourceAddressType { get; set; }
		public int? DestinationAddressTypeId { get; set; }
		public AddressType DestinationAddressType { get; set; }
		public int AclFilesUploadId { get; set; }
		public AclFilesUpload AclFilesUpload { get; set; }
	}
}