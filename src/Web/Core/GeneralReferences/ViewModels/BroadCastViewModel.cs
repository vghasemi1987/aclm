using System;
using System.Collections.Generic;

namespace Web.Core.GeneralReferences.ViewModels
{
	public class BroadCastViewModel
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PersonnelCode { get; set; }
		public string Subject { get; set; }
		public string Text { get; set; }
		public string FileAddress { get; set; }
		public string FileName { get; set; }
		public DateTime CreateDate { get; set; }
		public BroadCastTypeEnum BroadCastType { get; set; }
		public string BroadCastTypeName
		{
			get
			{
				return BroadCastType == BroadCastTypeEnum.General ? "عمومی" : "فوری";
			}
		}
		public List<ReferralBroadCastViewModel> ReferralBroadCasts { get; set; }
		public BroadCastViewModel()
		{
			ReferralBroadCasts = new List<ReferralBroadCastViewModel>();
		}
	}
	public enum BroadCastTypeEnum
	{
		Immediate, General,
	}
}
