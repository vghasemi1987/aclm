using DomainEntities.Commons;

namespace DomainEntities.ExcelFormatAggregate
{
	public class ExcelFormatDetail : Entity<int>
	{
		public string Column { get; set; }
		public string MappedColumn { get; set; }
		public int HeaderId { get; set; }
		public ExcelFormatHeader Header { get; set; }
	}
}
