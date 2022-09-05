using DomainEntities.ApplicationUserAggregate;
using DomainEntities.BroadcastAggregate;

namespace Web.Core.GeneralReferences.ViewModels
{
	public class ProtectionOfficeUserRelationViewModel
	{
		public int Id { get; set; }
		public int ProtectionOfficeId { get; set; }
		public ProtectionOffice ProtectionOffice { get; set; }
		public int ApplicationUserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
	}
}
