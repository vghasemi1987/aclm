using System.ComponentModel;

namespace DomainEntities.BroadcastAggregate
{
	public enum ReferralStatusBroadCastEnum
	{
		//[Display(Name = "عدم مشاهده")]
		[Description("عدم مشاهده")]
		AdameMoshahede,
		[Description("مشاهده شده")]
		MoshahedeShode,
		[Description("اقدام شده")]
		EghdamShode,
		[Description("مشاهده نوتیفیکیشن")]
		ShowNotification,
	}
}
