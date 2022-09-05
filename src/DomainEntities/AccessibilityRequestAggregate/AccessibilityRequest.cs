using DomainEntities.AccessibilityLevelAggregate;
using DomainEntities.AccessTypeAggregate;
using DomainEntities.ApplicationUserAggregate;
using DomainEntities.Commons;
using DomainEntities.DepartmentAggregate;
using DomainEntities.ProtocolAggregate;
using DomainEntities.ServiceAggregate;
using DomainEntities.SystemsAggregate;
using System;

namespace DomainEntities.AccessibilityRequestAggregate
{
	public class AccessibilityRequest : Entity<int>
	{
		public string LetterNo { get; set; }
		public DateTime? LetterDate { get; set; }
		public string SourceIp { get; set; }
		public string DestinationIp { get; set; }
		public DateTime? AccessStartDate { get; set; }
		public DateTime? AccessEndDate { get; set; }
		public string Description { get; set; }
		public string PhoneNumber { get; set; }

		public int? SourceSystemId { get; set; }
		public Systems SourceSystem { get; set; }

		public int? DestinationSystemId { get; set; }
		public Systems DestinationSystem { get; set; }

		public int? ServiceId { get; set; }
		public Service Service { get; set; }

		public int? DestinationServiceId { get; set; }
		public Service DestinationService { get; set; }

		public int? UserDepartmentId { get; set; }
		public Department UserDepartment { get; set; }

		public int? RequestDepartmentId { get; set; }
		public Department RequestDepartment { get; set; }

		public int? AccessibilityTypeId { get; set; }
		public AccessType AccessibilityType { get; set; }

		public int? RequestingUserId { get; set; }
		public ApplicationUser RequestingUser { get; set; }

		public int? ConfirmUserId { get; set; }
		public ApplicationUser ConfirmUser { get; set; }

		public int? SourceAccessibilityLevelId { get; set; }
		public AccessibilityLevel SourceAccessibilityLevel { get; set; }

		public int? DestAccessibilityLevelId { get; set; }
		public AccessibilityLevel DestAccessibilityLevel { get; set; }

		public int? SourceProtocolId { get; set; }
		public Protocol SourceProtocol { get; set; }

		public int? DestinationProtocolId { get; set; }
		public Protocol DestinationProtocol { get; set; }
	}
}