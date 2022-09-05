using DomainEntities.Commons;

namespace DomainEntities.TopupAccountExcelFormatAggregate
{
	public class TopupAccountExcelFormatHeader : Entity<int>
	{
		public string Title { get; set; }
		public string ExcelHeaders { get; set; }
	}
}
