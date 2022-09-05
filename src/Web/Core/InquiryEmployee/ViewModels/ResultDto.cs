using System.Collections.Generic;

namespace Web.Core.InquiryEmployee.ViewModels
{
	public class ResultDto
	{
		public string version { get; set; }
		public int statusCode { get; set; }
		public bool isError { get; set; }
		public string message { get; set; }
		public string responseException { get; set; }
		public List<Result> result { get; set; }
	}
	public class Result
	{
		public string organization { get; set; }
		public string nationalCode { get; set; }
		public string lastStatusCheck { get; set; }
		public string lastStatusCheckFa { get; set; }
		public bool isContagious { get; set; }
		public string quarantineEndDate { get; set; }
		public string quarantineEndDateFa { get; set; }

	}
}
