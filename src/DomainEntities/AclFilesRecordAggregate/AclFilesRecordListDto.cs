namespace DomainEntities.AclFilesRecordAggregate
{
	public class AclFilesRecordListDto
	{
		public int Id { get; set; }
		public bool RecordStatus { get; set; }
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
		public string SourceAddressTypeTitle { get; set; }
		public int? DestinationAddressTypeId { get; set; }
		public string DestinationAddressTypeTitle { get; set; }
		public int AclFilesUploadId { get; set; }
		public string AclFilesUploadTitle { get; set; }

	}
}
