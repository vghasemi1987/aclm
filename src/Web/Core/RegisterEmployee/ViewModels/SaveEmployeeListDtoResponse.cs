namespace Web.Core.RegisterEmployee.ViewModels
{
	/*خروجی سرویس ثبت کدملی کارمندان سازمان*/
	public class SaveEmployeeListDtoResponse
	{
		/// <summary>
		/// شناسه رکورد
		/// </summary>
		public string uid { get; set; }
		/// <summary>
		/// کدملی کارمند
		/// </summary>
		public string nationalCode { get; set; }
		/// <summary>
		/// آیا فراخوانی سرویس با خطا مواجه شد؟
		/// </summary>
		public bool isError { get; set; }
		/// <summary>
		/// پیام/توضیحات
		/// </summary>
		public string message { get; set; }
	}
}
