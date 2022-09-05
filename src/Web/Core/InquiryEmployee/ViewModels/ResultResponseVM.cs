namespace Web.Core.InquiryEmployee.ViewModels
{
	/// <summary>
	/// نتیجه استعلام ابتلای کارمندان سازمان
	/// (شرح خدمت)
	/// </summary>
	public class ResultResponseVM
	{
		/// <summary>
		/// نام کاربری سازمان
		/// </summary>
		public string Organization { get; set; }
		/// <summary>
		/// کدملی کارمند
		/// </summary>
		public string NationalCode { get; set; }
		/// <summary>
		/// تاریخ و ساعت آخرین بررسی وضعیت
		/// </summary>
		public string LastStatusCheck { get; set; }
		/// <summary>
		/// تاریخ و ساعت شمسی آخرین بررسی وضعیت
		/// </summary>
		public string LastStatusCheckFa { get; set; }
		/// <summary>
		/// آیا ناقل است؟
		/// </summary>
		public bool IsContagious { get; set; }
		/// <summary>
		/// تاریخ اتمام قرنطینه در صورتی که فرد توسط سیستم بررسی شده باشد
		/// </summary>
		public string QuarantineEndDate { get; set; }
		/// <summary>
		/// تاریخ شمسی اتمام قرنطینه در صورتی که فرد توسط سیستم بررسی شده باشد
		/// </summary>
		public string QuarantineEndDateFa { get; set; }
	}
}
