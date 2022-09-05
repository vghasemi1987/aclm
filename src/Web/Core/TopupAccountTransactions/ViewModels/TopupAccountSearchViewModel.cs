using System.ComponentModel.DataAnnotations;

namespace Web.Core.TopupAccountTransactions.ViewModels
{
	public class TopupAccountSearchViewModel
	{

		[Display(Name = "مبلغ تراکنش")]
		public string TransactionAmount { get; set; }
		[Display(Name = "شماره حساب مشتری")]
		public string CustomerAccountNumber { get; set; }
		[Display(Name = "کد پیگیری")]
		public string FollowupCode { get; set; }
		[Display(Name = "وضعیت تراکنش")]
		public string TransactionStatus { get; set; }
		[Display(Name = "تاریخ تراکنش")]
		public string TransactionDate { get; set; }
		[Display(Name = "زمان تراکنش")]
		public string TransactionTime { get; set; }
		public int HeaderId { get; set; }
	}
}
