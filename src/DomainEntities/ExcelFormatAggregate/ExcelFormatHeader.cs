using DomainEntities.Commons;

namespace DomainEntities.ExcelFormatAggregate
{
	public class ExcelFormatHeader : Entity<int>
	{
		public string Title { get; set; }
		public string ExcelHeaders { get; set; }
		public FileFormat FileFormat { get; set; }
		public string Separator { get; set; }
	}
}
