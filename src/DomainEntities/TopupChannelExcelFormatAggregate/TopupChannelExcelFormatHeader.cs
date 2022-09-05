using DomainEntities.Commons;

namespace DomainEntities.TopupChannelExcelFormatAggregate
{
	public class TopupChannelExcelFormatHeader : Entity<int>
	{
		public string Title { get; set; }
		public string ExcelHeaders { get; set; }
	}
}
