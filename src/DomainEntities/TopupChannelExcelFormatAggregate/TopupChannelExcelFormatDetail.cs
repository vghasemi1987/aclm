using DomainEntities.Commons;

namespace DomainEntities.TopupChannelExcelFormatAggregate
{
	public class TopupChannelExcelFormatDetail : Entity<int>
	{
		public string Column { get; set; }
		public string MappedColumn { get; set; }
		public int HeaderId { get; set; }
		public TopupChannelExcelFormatHeader Header { get; set; }
	}
}
