using DomainEntities.TopupAccountExcelFormatAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.TopupAccountExcelFormatAggregate
{
	public class TopupAccountExcelFormatHeaderConfig : IEntityTypeConfiguration<TopupAccountExcelFormatHeader>
	{
		public void Configure(EntityTypeBuilder<TopupAccountExcelFormatHeader> builder)
		{
			builder.ToTable("TopupAccountExcelFormatHeaders");
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Title).IsUnicode().HasMaxLength(50);
			builder.Property(p => p.ExcelHeaders).IsUnicode().HasMaxLength(500);
		}
	}
}
