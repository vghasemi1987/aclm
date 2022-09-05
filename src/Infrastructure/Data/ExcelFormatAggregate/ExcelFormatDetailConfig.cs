using DomainEntities.ExcelFormatAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ExcelFormatAggregate
{
	public class ExcelFormatDetailConfig : IEntityTypeConfiguration<ExcelFormatDetail>
	{
		public void Configure(EntityTypeBuilder<ExcelFormatDetail> builder)
		{
			builder.ToTable("ExcelFormatDetails");
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Column).IsUnicode().HasMaxLength(50);
			builder.Property(p => p.MappedColumn).IsUnicode().HasMaxLength(50);
			builder.HasOne(p => p.Header).WithMany().
				HasForeignKey(p => p.HeaderId).
				OnDelete(DeleteBehavior.Cascade);
		}
	}
}
