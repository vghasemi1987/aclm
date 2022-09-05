using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.SearchLogs.ViewModels
{
	public class SearchLogDetailViewModel
	{
		[Display(Name = "نام")]
		public string FirstName { get; set; }
		[Display(Name = "نام خانوادگی")]
		public string LastName { get; set; }
		[Display(Name = "کد ملی"), StringLength(10, ErrorMessage = "{0} وارد شده صحیح نمی باشد.", MinimumLength = 10)]
		public string NationalCode { get; set; }
		[Display(Name = "نام پدر")]
		public string FatherName { get; set; }
		[Display(Name = "آدرس")]
		public string Address { get; set; }
		public string IpAddress { get; set; }
		[Display(Name = "شناسه نامه")]
		public string LetterIdentifier { get; set; }
		[Display(Name = "شماره کارت")]
		public string CardNumberPart1 { get; set; }
		public string CardNumberPart2 { get; set; }
		public string CardNumberPart3 { get; set; }
		public string CardNumberPart4 { get; set; }
		public List<int> TransactionDetailIdList { get; set; } = new List<int>();
		public int UserId { get; set; }
		[Display(Name = "تاریخ جستجو")]
		public string SearchDate { get; set; }
		public string SearchTime { get; set; }
		[Display(Name = "وضعیت جستجو")]
		public short IsSuccess { get; set; }
		[Display(Name = "وضعیت تراکنش")]
		public short IsVictim { get; set; }
		public short Search { get; set; }
	}
}
