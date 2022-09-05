using DomainEntities.TopupChannelExcelFormatAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.TopupChannelExcelFormatAggregate
{
	public class TopupChannelExcelFormatHeaderConfig : IEntityTypeConfiguration<TopupChannelExcelFormatHeader>
	{
		public void Configure(EntityTypeBuilder<TopupChannelExcelFormatHeader> builder)
		{
			builder.ToTable("TopupChannelExcelFormatHeaders");
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Title).IsUnicode().HasMaxLength(50);
			builder.Property(p => p.ExcelHeaders).IsUnicode().HasMaxLength(500);
		}
	}
}
