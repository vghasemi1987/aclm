using ApplicationCommon;
using System;

namespace Web.ActionFilters
{
	public class UserLogMessageViewModel
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string Action { get; set; }
		public string Controller { get; set; }
		public string Description { get; set; }
		public DateTime DateActivity { get; set; }
		public string Ip { get; set; }
		public string PersianDate
		{
			get
			{
				return DateActivity.ToPersianDateTime("-");
			}
		}
	}
}