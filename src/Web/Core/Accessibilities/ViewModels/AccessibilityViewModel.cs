using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.Accessibilities.ViewModels
{
	public class AccessibilityViewModel
	{
		[HiddenInput]
		public int Id { get; set; }

		[Display(Name = "آدرس سیستم مبدا")]
		public string SourceIp { get; set; }

		[Display(Name = "آدرس سیستم مقصد")]
		public string DestinationIp { get; set; }

		[Display(Name = "شماره پورت مبدا")]
		public int? SourcePort { get; set; }

		[Display(Name = "شماره پورت مبدا")]
		public int? DestinationPort { get; set; }

		public bool? IsTemp { get; set; }

		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "پروتکل")]
		public int? ProtocolId { get; set; }

		public string ProtocolTitle { get; set; }

		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "سیستم مبدا")]
		public int? SourceSystemId { get; set; }

		public string SourceSystemTitle { get; set; }

		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "سیستم مقصد")]
		public int? DestinationSystemId { get; set; }

		public string DestinationSystemTitle { get; set; }

		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "پورت مبدا")]
		public int? ServiceId { get; set; }

		public string ServiceTitle { get; set; }

		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "پورت مقصد")]
		public int? DestinationServiceId { get; set; }

		public string DestinationServiceTitle { get; set; }

		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "نوع عملیات")]
		public int? ActionTypeId { get; set; }

		public string ActionTypeTitle { get; set; }

		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "روتر")]
		public int? RouterId { get; set; }

		public string RouterTitle { get; set; }

		public int? AclFilesUploadId { get; set; }
		public string AclFilesUploadTitle { get; set; }

		public SelectList SystemList { get; set; }
		public SelectList ProtocolList { get; set; }
		public SelectList RouterList { get; set; }
		public SelectList ServiceList { get; set; }
		public SelectList ActionTypeList { get; set; }
		public object SystemIpList { get; set; }
	}
}
