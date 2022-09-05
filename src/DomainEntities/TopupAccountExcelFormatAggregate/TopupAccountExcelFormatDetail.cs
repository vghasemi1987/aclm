using DomainEntities.Commons;

namespace DomainEntities.TopupAccountExcelFormatAggregate
{
	public class TopupAccountExcelFormatDetail : Entity<int>
	{
		public string Column { get; set; }
		public string MappedColumn { get; set; }
		public int HeaderId { get; set; }
		public TopupAccountExcelFormatHeader Header { get; set; }
	}
}
