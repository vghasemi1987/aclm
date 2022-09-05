using System.ComponentModel.DataAnnotations;

namespace Web.Core.InquiryEmployee.ViewModels
{
	/// <summary>
	/// خروجی استعلام وضعیت ابتلای کارمندان
	/// </summary>
	public class GetLastStatusOfEmployeesDtoRequestVM
	{
		[Display(Name = "نام کاربری")]
		[Required(ErrorMessage = "{0} اجباری می باشد")]
		public string Username01 { get; set; }
		[Display(Name = "رمز عبور")]
		[Required(ErrorMessage = "{0} اجباری می باشد")]
		public string Password01 { get; set; }
		[Display(Name = "نام کاربری")]
		[Required(ErrorMessage = "{0} اجباری می باشد")]
		public string Username02 { get; set; }
		[Display(Name = "رمز عبور")]
		[Required(ErrorMessage = "{0} اجباری می باشد")]
		public string Password02 { get; set; }
	}
}
