using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Core.RegisterEmployee.ViewModels
{
	/// <summary>
	/// مشخصات کارمندان
	/// </summary>
	public class Employee
	{
		public string nationalCode { get; set; }
		public int employeeStatus { get; set; }
	}
	public class xlEmployee
	{
		[Column("1")]
		[Required]
		public string nationalCode { get; set; }
		[Column("2")]
		[Required]
		public int employeeStatus { get; set; }
	}
}
