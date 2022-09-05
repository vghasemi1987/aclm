using System.Collections.Generic;

namespace Web.Core.GeneralReferences.ViewModels
{
	public class ProtectionOfficeViewModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public List<ProtectionOfficeUserRelationViewModel> ProtectionOfficeUserRelationViewModels { get; set; }
	}
}