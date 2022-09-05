using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.RegisterEmployee.ViewModels
{
	public class SaveEmployeeListDtoRequest
	{
		[Display(Name = "نام کاربری")]
		[Required(ErrorMessage = "{0} اجباری می باشد.")]
		public string username { get; set; }
		[Display(Name = "رمز عبور")]
		[Required(ErrorMessage = "{0} اجباری می باشد.")]
		public string password { get; set; }
		public List<Employee> Employees { get; set; }
	}
	public class SaveEmployeeListDtoRequestDto
	{
		[Display(Name = "نام کاربری")]
		[Required(ErrorMessage = "{0} اجباری می باشد.")]
		public string usernameHeader { get; set; }
		[Display(Name = "رمز عبور")]
		[Required(ErrorMessage = "{0} اجباری می باشد.")]
		public string passwordHeader { get; set; }
		[Display(Name = "نام کاربری")]
		[Required(ErrorMessage = "{0} اجباری می باشد.")]
		public string usernameBody { get; set; }
		[Display(Name = "رمز عبور")]
		[Required(ErrorMessage = "{0} اجباری می باشد.")]
		public string passwordBody { get; set; }
	}
}