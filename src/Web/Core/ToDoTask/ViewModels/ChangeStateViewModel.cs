using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.ToDoTask.ViewModels
{
	public class ChangeStateViewModel
	{
		[Display(Name = "وضعیت انجام وظیفه")]
		public short StateId { get; set; }
		public SelectList Status { get; set; }
		[HiddenInput]
		public int Id { get; set; }
	}
}