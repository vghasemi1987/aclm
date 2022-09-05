using DomainEntities.TopupChannelExcelFormatAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.TopupChannelExcelFormatAggregate
{
	public class TopupChannelExcelFormatDetailConfig : IEntityTypeConfiguration<TopupChannelExcelFormatDetail>
	{
		public void Configure(EntityTypeBuilder<TopupChannelExcelFormatDetail> builder)
		{
			builder.ToTable("TopupChannelExcelFormatDetails");
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Column).IsUnicode().HasMaxLength(50);
			builder.Property(p => p.MappedColumn).IsUnicode().HasMaxLength(50);
			builder.HasOne(p => p.Header).WithMany().
				HasForeignKey(p => p.HeaderId).
				OnDelete(DeleteBehavior.Cascade);
		}
	}
}
