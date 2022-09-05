using DomainEntities.TopupAccountExcelFormatAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.TopupAccountExcelFormatAggregate
{
	public class TopupAccountExcelFormatDetailConfig : IEntityTypeConfiguration<TopupAccountExcelFormatDetail>
	{
		public void Configure(EntityTypeBuilder<TopupAccountExcelFormatDetail> builder)
		{
			builder.ToTable("TopupAccountExcelFormatDetails");
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Column).IsUnicode().HasMaxLength(50);
			builder.Property(p => p.MappedColumn).IsUnicode().HasMaxLength(50);
			builder.HasOne(p => p.Header).WithMany().
				HasForeignKey(p => p.HeaderId).
				OnDelete(DeleteBehavior.Cascade);
		}
	}
}
