using DomainEntities.AclFilesUploadAggregate;
using DomainEntities.ActionTypeAggregate;
using DomainEntities.ApplicationUserAggregate;
using DomainEntities.Commons;
using DomainEntities.ProtocolAggregate;
using DomainEntities.RouterAggregate;
using DomainEntities.ServiceAggregate;
using DomainEntities.SystemsAggregate;
using System;

namespace DomainEntities.AccessibilityAggregate
{
	public class Accessibility : Entity<int>
	{
		public string SourceIp { get; set; }
		public string DestinationIp { get; set; }
		public string SourcePort { get; set; }
		public string DestinationPort { get; set; }
		public string Port { get; set; }
		public bool IsTemp { get; set; }
		public string Protocol { get; set; }
		public int? ProtocolsId { get; set; }
		public Protocol Protocols { get; set; }
		public int? SourceSystemId { get; set; }
		public Systems SourceSystem { get; set; }
		public int? DestinationSystemId { get; set; }
		public Systems DestinationSystem { get; set; }
		public int? ServiceId { get; set; }
		public Service Service { get; set; }
		public int? DestinationServiceId { get; set; }
		public Service DestinationService { get; set; }
		public string ActionType { get; set; }
		public int? ActionTypesId { get; set; }
		public ActionType ActionTypes { get; set; }
		public int? RouterId { get; set; }
		public Router Router { get; set; }
		public int UserId { get; set; }
		public ApplicationUser User { get; set; }
		public int? AclFilesUploadId { get; set; }
		public AclFilesUpload AclFilesUpload { get; set; }
		public DateTime? VersionDate { get; set; }
	}
}