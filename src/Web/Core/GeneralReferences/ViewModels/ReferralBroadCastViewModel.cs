using DomainEntities.ApplicationUserAggregate;
using DomainEntities.BroadcastAggregate;
using System;

namespace Web.Core.GeneralReferences.ViewModels
{
	public class ReferralBroadCastViewModel
	{
		public int Id { get; set; }
		public int? SrcUser { get; set; }
		public int? DstUserID { get; set; }
		public string Description { get; set; }
		public ReferralStatusBroadCastEnum Status { get; set; }
		public DateTime? DeadLine { get; set; }
		public bool IsImmediate { get; set; }
		public int? BroadCastId { get; set; }
		public BroadCast BroadCast { get; set; }
		public string ActionDescription { get; set; }
		/// <summary>
		/// Relation between ApplicationUser and ReferralBroadCast
		/// </summary>
		public ApplicationUser ApplicationUser { get; set; }
	}
}
