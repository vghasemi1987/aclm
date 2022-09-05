using DomainEntities.ExcelFormatAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ExcelFormatAggregate
{
	public class ExcelFormatHeaderConfig : IEntityTypeConfiguration<ExcelFormatHeader>
	{
		public void Configure(EntityTypeBuilder<ExcelFormatHeader> builder)
		{
			builder.ToTable("ExcelFormatHeaders");
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Title).IsUnicode().HasMaxLength(50);
			builder.Property(p => p.Separator).IsUnicode().HasMaxLength(5);
			builder.Property(p => p.ExcelHeaders).IsUnicode().HasMaxLength(500);
		}
	}
}
