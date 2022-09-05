using ApplicationCommon.WebToolkit.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.ToDoTask.ViewModels
{
	public class FormViewModel
	{
		[HiddenInput]
		public int Id { get; set; }

		[Required(ErrorMessage = "{0} را وارد نمایید"), Display(Name = "عنوان")]
		public string Title { get; set; }
		[Required(ErrorMessage = "{0} را انتخاب نمایید"), Display(Name = "تاریخ سررسید")]
		[RegularExpression(@"\d{4,4}/\d{2,2}/\d{2,2} \d{2,2}:\d{2,2}:\d{2,2}", ErrorMessage = "{0} وارد شده نامعتبر است.")]
		public string DueDateTime { get; set; }
		[Required(ErrorMessage = "{0} را انتخاب نمایید"), Display(Name = "انتصاب به")]
		public int AssignedToUserId { get; set; }
		public SelectList Users { get; set; }
		[Display(Name = "توضیحات"), DataType(DataType.MultilineText)]
		public string Description { get; set; }
		[Required(ErrorMessage = "{0} را انتخاب نمایید"), Display(Name = "ارجعیت")]
		public short PriorityId { get; set; }
		public SelectList Priority { get; set; }
		[Required(ErrorMessage = "{0} را انتخاب نمایید"), Display(Name = "وضعیت")]
		public short StateId { get; set; }
		public SelectList StateSelectList { get; set; }
		[Display(Name = "فایل"), ValidateFile(MaxSize = 20000)]
		public IFormFile PostedFile { get; set; }
	}

	public class ToDoTaskListViewModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public DateTime DueDateTime { get; set; }
		public string Priority { get; set; }
		public string Status { get; set; }
		public string User { get; set; }
	}
}