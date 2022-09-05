using System.Collections.Generic;

namespace Web.Core.InquiryEmployee.ViewModels
{
	public class ResultDtoPager
	{
		public ResultDto ResultDto { get; set; }
		public List<Result> ResultListDto { get; set; }
		public PagingInfo PagingInfo { get; set; }
		public string CurrentCategory { get; set; }

	}
}
