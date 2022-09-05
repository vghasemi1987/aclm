using System.ComponentModel.DataAnnotations;

namespace Web.Core.Transactions.ViewModels
{
	public class SearchViewModel
	{
		[Display(Name = "شماره کارت مبدا")/*,Required(ErrorMessage = "{0} را وارد نمایید")*/]
		public string SourcePan { get; set; }
		[Display(Name = "شماره کارت مقصد")/*, Required(ErrorMessage = "{0} را وارد نمایید")*/]
		public string TargetPan { get; set; }
		[Display(Name = "از تاریخ")/*, Required(ErrorMessage = "{0} را وارد نمایید")*/]
		public string BeginTransactionDate { get; set; }
		[Display(Name = "تا تاریخ")/*, Required(ErrorMessage = "{0} را وارد نمایید")*/]
		public string EndTransactionDate { get; set; }
		[Display(Name = "مبلغ")]
		public long? Amount { get; set; }
		public int HeaderId { get; set; }
	}
}
