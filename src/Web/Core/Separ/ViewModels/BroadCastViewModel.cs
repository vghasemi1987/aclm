using DomainEntities.ApplicationUserAggregate;
using DomainEntities.BroadcastAggregate;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web.Core.GeneralReferences.ViewModels;

namespace Web.Core.Separ.ViewModels
{
	public class BroadCastViewModel
	{
		public string UserNameSender { get; set; }
		public int Id { get; set; }
		[Display(Name = "نام")]
		public string FirstName { get; set; }
		[Display(Name = "نام خانوادگی")]
		public string LastName { get; set; }
		[Display(Name = "کدپرسنلی")]
		public string PersonnelCode { get; set; }
		[Required(ErrorMessage = "{0} اجباری می باشد")]
		[Display(Name = "موضوع")]
		public string Subject { get; set; }
		[Required(ErrorMessage = "{0} اجباری می باشد")]
		[Display(Name = "متن")]
		public string Text { get; set; }
		[Display(Name = "فایل")]
		public IFormFile File { get; set; }
		[Display(Name = "نام فایل")]
		public string FileName { get; set; }
		public List<ReferralBroadCastViewModel> ReferralBroadCasts { get; set; }
		[Display(Name = "تاریخ ضرب الاجل")]
		public string DateTimeSubject { get; set; }
		public BroadCastViewModel()
		{
			CreateDate = DateTime.Now;
			ReferralBroadCasts = new List<ReferralBroadCastViewModel>();
		}
		public DateTime CreateDate { get; set; }
		//Ignore Property
		public string CreateDatePersian
		{
			get
			{
				System.Globalization.PersianCalendar pcr = new System.Globalization.PersianCalendar();
				return $"{pcr.GetYear(CreateDate)}/{pcr.GetMonth(CreateDate)}/{pcr.GetDayOfMonth(CreateDate)} --  {CreateDate.Hour}:{CreateDate.Minute}";

			}
		}

	}
	public class BroadCastViewModelGet : BroadCastViewModel
	{
		//[Required(ErrorMessage = "اجباری می باشد")]
		public IEnumerable<ApplicationUser> Users { get; set; }
		public IEnumerable<GroupingOffice> GroupingOffices { get; set; }
	}
	public class BroadCastViewModelPost : BroadCastViewModel
	{
		//[Required(ErrorMessage = "اجباری می باشد")]
		public List<int> SelectedUsers { get; set; }
		public List<int> SelectedGroup { get; set; }
		public BroadCastViewModelPost()
		{
			SelectedUsers = new List<int>();
			SelectedGroup = new List<int>();
		}
	}
	public class BroadCastViewModelGeneral
	{
		[Display(Name = "نام")]
		public string FirstName { get; set; }
		[Display(Name = "نام خانوادگی")]
		public string LastName { get; set; }
		[Display(Name = "کدپرسنلی")]
		public string PersonnelCode { get; set; }
		[Display(Name = "موضوع")]
		[Required(ErrorMessage = "{0} اجباری می باشد")]
		public string Subject { get; set; }
		[Required(ErrorMessage = "{0} اجباری می باشد")]
		[Display(Name = "متن")]
		public string Text { get; set; }
		[Display(Name = "فایل")]
		public IFormFile File { get; set; }
		[Display(Name = "نام فایل")]
		public string FileName { get; set; }
		public BroadCastTypeEnum BroadCastType { get; set; }
		public List<ReferralBroadCastViewModel> ReferralBroadCasts { get; set; }
		public List<ProtectionOfficeViewModel> ProtectionOfficeViewModels { get; set; }
		[Display(Name = "تاریخ ضرب الاجل")]
		public string DateTimeSubject { get; set; }
		public BroadCastViewModelGeneral()
		{
			ReferralBroadCasts = new List<ReferralBroadCastViewModel>();
			ProtectionOfficeViewModels = new List<ProtectionOfficeViewModel>();
		}
		public string Town { get; set; }
	}
	public enum BroadCastTypeEnum
	{
		Immediate, General,
	}
}