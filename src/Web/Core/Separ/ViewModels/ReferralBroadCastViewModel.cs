using DomainEntities.ApplicationUserAggregate;
using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.Separ.ViewModels
{
	public class ReferralBroadCastViewModel
	{
		public string SrcUser { get; set; }
		public int DstUserID { get; set; }
		public string Description { get; set; }
		public ReferralStatusBroadCastEnumViewModel Status { get; set; }
		public string StatusString
		{
			get
			{
				if (Status == ReferralStatusBroadCastEnumViewModel.AdameMoshahede) return "عدم مشاهده";
				else if (Status == ReferralStatusBroadCastEnumViewModel.EghdamShode) return "اقدام شده";
				else if (Status == ReferralStatusBroadCastEnumViewModel.MoshahedeShode) return "مشاهده شده";
				else if (Status == ReferralStatusBroadCastEnumViewModel.ShowNotification) return "مشاهده نوتیفیکیشن";
				else return "";
			}
		}
		public DateTime? DeadLine { get; set; }
		public bool IsImmediate { get; set; }
		public int BroadCastId { get; set; }
		public BroadCastViewModel BroadCast { get; set; }
		[Display(Name = "اقدام")]
		public string ActionDescription { get; set; }
		public ReferralBroadCastViewModel()
		{
			BroadCast = new BroadCastViewModel();
		}
		public ApplicationUser ApplicationUser { get; set; }
		//Ignore Property
		public string DeadLinePersian
		{
			get
			{
				if (DeadLine != null)
				{
					System.Globalization.PersianCalendar pcr = new System.Globalization.PersianCalendar();
					return $"{pcr.GetYear((DateTime)DeadLine)}/{pcr.GetMonth((DateTime)DeadLine)}/{pcr.GetDayOfMonth((DateTime)DeadLine)} -- {DeadLine.Value.Hour}:{DeadLine.Value.Minute}";
				}
				return "ندارد";

			}
		}
	}
}
