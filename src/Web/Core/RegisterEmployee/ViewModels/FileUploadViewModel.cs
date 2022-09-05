using Microsoft.AspNetCore.Http;

namespace Web.Core.RegisterEmployee.ViewModels
{
	public class FileUploadViewModel
	{
		public SaveEmployeeListDtoRequest SaveEmployeeListDtoRequest { get; set; }
		public IFormFile XlsFile { get; set; }
		//public Employee Employee { get; set; }
		//public FileUploadViewModel()
		//{
		//    Employee = new Employee();
		// }
	}
}
