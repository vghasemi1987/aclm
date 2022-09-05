namespace Web.Core.InquiryEmployee.ViewModels
{
	public class Personnel
	{
		/// <summary>
		/// نام
		/// </summary>
		public string FirstName { get; set; }
		/// <summary>
		/// نام خانوادگی
		/// </summary>
		public string LastName { get; set; }
		/// <summary>
		/// کد پرسنلی
		/// </summary>
		public string Code { get; set; }
		/// <summary>
		/// کدملی
		/// </summary>
		public string NationalCode { get; set; }
		/// <summary>
		/// محل خدمت
		/// </summary>
		public string ProvinceName { get; set; }
		/// <summary>
		/// امور شعب
		/// </summary>
		public string OrganizationName { get; set; }
		/// <summary>
		/// شعبه یا واحد
		/// </summary>
		public string UnitName { get; set; }
	}
}
