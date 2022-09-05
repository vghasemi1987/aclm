﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.UserManagement.ViewModels
{
	public class ImpersonateUserViewModel
	{
		[Required(ErrorMessage = "{0} را انتخاب نمایید"), Display(Name = "ورود به عنوان", Prompt = "ورود به عنوان")]
		public string UserId { get; set; }
		public SelectList Users { get; set; }
	}
}
