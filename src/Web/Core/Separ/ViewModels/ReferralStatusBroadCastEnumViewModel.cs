using System.ComponentModel;

namespace Web.Core.Separ.ViewModels
{
	public enum ReferralStatusBroadCastEnumViewModel
	{
		//[Display(Name = "عدم مشاهده")]
		[Description("عدم مشاهده")]
		AdameMoshahede,
		[Description("مشاهده شده")]
		MoshahedeShode,
		[Description("اقدام شده")]
		EghdamShode,
		[Description("مشاهده نوتیفیکیشن")]
		ShowNotification
	}
}