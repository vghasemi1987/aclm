using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.TopupAccountExcelFormats.ViewModels
{
	public class TopupAccountExcelFormatViewModel
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



		[Display(Name = "شماره حساب")]
		public string AccountNumber { get; set; }
		[Display(Name = "کد شعبه")]
		public string BranchCode { get; set; }
		[Display(Name = "شماره حساب مشتری")]
		public string CustomerAccountNumber { get; set; }
		[Display(Name = "Extra1")]
		public string Extra1 { get; set; }
		[Display(Name = "Extra2")]
		public string Extra2 { get; set; }
		[Display(Name = "Extra3")]
		public string Extra3 { get; set; }
		[Display(Name = "Extra4")]
		public string Extra4 { get; set; }
		[Display(Name = "کد پیگیری کانال")]
		public string FollowupCode { get; set; }
		[Display(Name = "کد پیگیری 2")]
		public string FollowupCode2 { get; set; }
		[Display(Name = "کد مرجع")]
		public string RefrenceCode { get; set; }
		[Display(Name = "مبلغ تراکنش")]
		public string TransactionAmount { get; set; }
		[Display(Name = "مبلغ حرفی")]
		public string TransactionAmountText { get; set; }
		[Display(Name = "تاریخ تراکنش")]
		public string TransactionDate { get; set; }
		[Display(Name = "وضعیت تراکنش")]
		public string TransactionStatus { get; set; }
		[Display(Name = "زمان تراکنش")]
		public string TransactionTime { get; set; }
		[Display(Name = "نوع تراکنش واریز یا برداشت")]
		public string TransactionType { get; set; }
	}
}
