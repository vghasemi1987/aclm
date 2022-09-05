using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.TopupChannelExcelFormats.ViewModels
{
	public class TopupChannelExcelFormatViewModel
	{
		[HiddenInput]
		public int Id { get; set; }
		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "عنوان")]
		public string Title { get; set; }
		[Display(Name = "فرمت فایل اکسل")]
		public string Files { get; set; }
		[Required(ErrorMessage = "{0} را وارد نمایید")]
		public int HeaderId { get; set; }
		//public string LogDate { get; set; }
		//public string Application { get; set; }
		public SelectList PropList { get; set; }


		[Display(Name = "مبلغ حرفی")]
		public string AmountText { get; set; }
		[Display(Name = "نوع کانال")]
		public string ChannelType { get; set; }
		[Display(Name = "شماره حساب مشتری")]
		public string CustomerAccountNumber { get; set; }
		[Display(Name = "Extra1")]
		public string Extra1 { get; set; }
		[Display(Name = "Extra2")]
		public string Extra2 { get; set; }
		[Display(Name = "کد پیگیری")]
		public string FollowupCode { get; set; }
		[Display(Name = "کد پیگیری 2")]
		public string FollowupCode2 { get; set; }
		[Display(Name = "شماره موبایل")]
		public string MobileNumber { get; set; }
		[Display(Name = "نام اپراتور")]
		public string OperatorName { get; set; }
		[Display(Name = "مبلغ تراکنش")]
		public string TransactionAmount { get; set; }
		[Display(Name = "تاریخ تراکنش")]
		public string TransactionDate { get; set; }
		[Display(Name = "وضعیت تراکنش")]
		public string TransactionStatus { get; set; }

	}
}
