using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.ToDoTask.ViewModels
{
	public class SearchViewModel
	{
		[Display(Name = "عنوان")]
		public string Title { get; set; }
		[Display(Name = "از تاریخ سررسید")]
		[RegularExpression(@"^$|^([1۱][۰-۹ 0-9]{3}[/\/]([0 ۰][۱-۶ 1-6])[/\/]([0 ۰][۱-۹ 1-9]|[۱۲12][۰-۹ 0-9]|[3۳][01۰۱])|[1۱][۰-۹ 0-9]{3}[/\/]([۰0][۷-۹ 7-9]|[1۱][۰۱۲012])[/\/]([۰0][1-9 ۱-۹]|[12۱۲][0-9 ۰-۹]|(30|۳۰)))$", ErrorMessage = "{0} وارد شده نامعتبر است.")]
		public string FromDueDateTime { get; set; }
		[Display(Name = "تا تاریخ سررسید")]
		[RegularExpression(@"^$|^([1۱][۰-۹ 0-9]{3}[/\/]([0 ۰][۱-۶ 1-6])[/\/]([0 ۰][۱-۹ 1-9]|[۱۲12][۰-۹ 0-9]|[3۳][01۰۱])|[1۱][۰-۹ 0-9]{3}[/\/]([۰0][۷-۹ 7-9]|[1۱][۰۱۲012])[/\/]([۰0][1-9 ۱-۹]|[12۱۲][0-9 ۰-۹]|(30|۳۰)))$", ErrorMessage = "{0} وارد شده نامعتبر است.")]
		public string ToDueDateTime { get; set; }
		[Display(Name = "انتصاب به")]
		public List<string> AssignedToUserId { get; set; }
		[Display(Name = "ایجاد شده")]
		public List<string> CreatorUserId { get; set; }
		public SelectList Users { get; set; }
		[Display(Name = "توضیحات"), DataType(DataType.MultilineText)]
		public string Description { get; set; }
		[Display(Name = "ارجعیت")]
		public List<int> PriorityId { get; set; }
		public SelectList Priority { get; set; }
		[Display(Name = "وضعیت")]
		public List<int> StateId { get; set; }
		public SelectList StateSelectList { get; set; }
		[Display(Name = "نام مخاطب")]
		public string ContactName { get; set; }
		[Display(Name = "از تاریخ ثبت", Prompt = "مثال : 1397/01/25")]
		[RegularExpression(@"^$|^([1۱][۰-۹ 0-9]{3}[/\/]([0 ۰][۱-۶ 1-6])[/\/]([0 ۰][۱-۹ 1-9]|[۱۲12][۰-۹ 0-9]|[3۳][01۰۱])|[1۱][۰-۹ 0-9]{3}[/\/]([۰0][۷-۹ 7-9]|[1۱][۰۱۲012])[/\/]([۰0][1-9 ۱-۹]|[12۱۲][0-9 ۰-۹]|(30|۳۰)))$", ErrorMessage = "{0} وارد شده نامعتبر است.")]
		public string FromEntryDate { get; set; }
		[Display(Name = "تا تاریخ ثبت", Prompt = "مثال : 1397/01/25")]
		[RegularExpression(@"^$|^([1۱][۰-۹ 0-9]{3}[/\/]([0 ۰][۱-۶ 1-6])[/\/]([0 ۰][۱-۹ 1-9]|[۱۲12][۰-۹ 0-9]|[3۳][01۰۱])|[1۱][۰-۹ 0-9]{3}[/\/]([۰0][۷-۹ 7-9]|[1۱][۰۱۲012])[/\/]([۰0][1-9 ۱-۹]|[12۱۲][0-9 ۰-۹]|(30|۳۰)))$", ErrorMessage = "{0} وارد شده نامعتبر است.")]
		public string ToEntryDate { get; set; }
		[HiddenInput] public string Oper { get; set; } = "And";
		[Display(Name = "از تاریخ پایان", Prompt = "مثال : 1397/01/25")]
		[RegularExpression(@"^$|^([1۱][۰-۹ 0-9]{3}[/\/]([0 ۰][۱-۶ 1-6])[/\/]([0 ۰][۱-۹ 1-9]|[۱۲12][۰-۹ 0-9]|[3۳][01۰۱])|[1۱][۰-۹ 0-9]{3}[/\/]([۰0][۷-۹ 7-9]|[1۱][۰۱۲012])[/\/]([۰0][1-9 ۱-۹]|[12۱۲][0-9 ۰-۹]|(30|۳۰)))$", ErrorMessage = "{0} وارد شده نامعتبر است.")]
		public string FromCompletionDateTime { get; set; }
		[Display(Name = "تا تاریخ پایان", Prompt = "مثال : 1397/01/25")]
		[RegularExpression(@"^$|^([1۱][۰-۹ 0-9]{3}[/\/]([0 ۰][۱-۶ 1-6])[/\/]([0 ۰][۱-۹ 1-9]|[۱۲12][۰-۹ 0-9]|[3۳][01۰۱])|[1۱][۰-۹ 0-9]{3}[/\/]([۰0][۷-۹ 7-9]|[1۱][۰۱۲012])[/\/]([۰0][1-9 ۱-۹]|[12۱۲][0-9 ۰-۹]|(30|۳۰)))$", ErrorMessage = "{0} وارد شده نامعتبر است.")]
		public string ToCompletionDateTime { get; set; }
	}
}