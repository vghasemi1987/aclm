using System.Collections.Generic;

namespace Web.Core.InquiryEmployee.ViewModels
{
	public class GetLastStatusOfEmployeesDtoResponseVM
	{
		/// <summary>
		/// نسخه سرویس
		/// </summary>
		public string Version { get; set; }
		/// <summary>
		/// وضعیت فراخوانی سرویس
		/// </summary>
		public int StatusCode { get; set; }
		/// <summary>
		/// آیا فراخوانی سرویس با خطا مواجه شده ؟
		/// </summary>
		public bool IsError { get; set; }
		/// <summary>
		/// پیام و توضیحات
		/// </summary>
		public string Message { get; set; }
		public IList<ResultResponseVM> ResultResponse { get; set; }
	}
}
