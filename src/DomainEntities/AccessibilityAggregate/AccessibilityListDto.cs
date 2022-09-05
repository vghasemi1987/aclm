namespace DomainEntities.AccessibilityAggregate
{
	public class AccessibilityListDto
	{
		public int Id { get; set; }
		public bool RecordStatus { get; set; }
		public string SourceIp { get; set; }
		public string DestinationIp { get; set; }
		public string SourcePort { get; set; }
		public string DestinationPort { get; set; }
		public string Port { get; set; }
		public bool? IsTemp { get; set; }
		public int? ProtocolId { get; set; }
		public string Protocol { get; set; }
		public int? SourceSystemId { get; set; }
		public string SourceSystemTitle { get; set; }
		public int? DestinationSystemId { get; set; }
		public string DestinationSystemTitle { get; set; }
		public int? ServiceId { get; set; }
		public string ServiceTitle { get; set; }
		public int? DestinationServiceId { get; set; }
		public string DestinationServiceTitle { get; set; }
		public int? ActionTypeId { get; set; }
		public string ActionType { get; set; }
		public int? RouterId { get; set; }
		public string RouterTitle { get; set; }
		public int? AclFilesUploadId { get; set; }
		public string AclFilesUploadTitle { get; set; }
	}
}
