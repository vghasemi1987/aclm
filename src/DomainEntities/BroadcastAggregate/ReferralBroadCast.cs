using DomainEntities.ApplicationUserAggregate;
using DomainEntities.Commons;
using System;

namespace DomainEntities.BroadcastAggregate
{
	public class ReferralBroadCast : Entity<int>
	{
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

		//Ignore Property
		public string DeadLinePersian
		{
			get
			{
				if (DeadLine != null)
				{
					System.Globalization.PersianCalendar pcr = new System.Globalization.PersianCalendar();
					return $"{pcr.GetYear((DateTime)DeadLine)}/{pcr.GetMonth((DateTime)DeadLine)}/{pcr.GetDayOfMonth((DateTime)DeadLine)}  {DeadLine.Value.Hour}:{DeadLine.Value.Minute}";
				}
				return "ندارد";

			}
		}
	}
}
