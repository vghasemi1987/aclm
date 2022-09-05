using System;

namespace DomainEntities.AccessibilityRequestAggregate
{
	public class AccessibilityRequestListDto
	{
		public int Id { get; set; }
		public bool RecordStatus { get; set; }
		public string LetterNo { get; set; }
		public string LetterDate { get; set; }
		public string SourceIp { get; set; }
		public string DestinationIp { get; set; }
		public int? SourcePort { get; set; }
		public int? DestinationPort { get; set; }
		public DateTime? AccessStartDate { get; set; }
		public DateTime? AccessEndDate { get; set; }
		public string Description { get; set; }
		public string PhoneNumber { get; set; }
		public int? RequestDepartmentId { get; set; }
		public string RequestDepartmentTitle { get; set; }
		public int? SystemsId { get; set; }
		public string SystemTitle { get; set; }
		public int? SourceSystemId { get; set; }
		public string SourceSystemTitle { get; set; }
		public int? DestinationSystemId { get; set; }
		public string DestinationSystemTitle { get; set; }
		public int? ServiceId { get; set; }
		public string ServiceTitle { get; set; }
		public int? DestinationServiceId { get; set; }
		public string DestinationServiceTitle { get; set; }
		public int? UserDepartmentId { get; set; }
		public string UserDepartmentTitle { get; set; }
		public int? AccessibilityTypeId { get; set; }
		public string AccessibilityTypeTitle { get; set; }
		public int? RequestingUserId { get; set; }
		public string RequestingUser { get; set; }
		public int? ConfirmUserId { get; set; }
		public string ConfirmUser { get; set; }
		public int? SourceAccessibilityLevelId { get; set; }
		public string SourceAccessibilityLevelTitle { get; set; }
		public int? DestAccessibilityLevelId { get; set; }
		public string DestAccessibilityLevelTitle { get; set; }
		public int? SourceProtocolId { get; set; }
		public string SourceProtocolTitle { get; set; }
		public int? DestinationProtocolId { get; set; }
		public string DestinationProtocolTitle { get; set; }
	}
}
