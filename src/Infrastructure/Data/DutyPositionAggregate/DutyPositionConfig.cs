using DomainEntities.DutyPositionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.DutyPositionAggregate
{
	public class DutyPositionConfig : IEntityTypeConfiguration<DutyPosition>
	{
		public void Configure(EntityTypeBuilder<DutyPosition> builder)
		{
			builder.ToTable("DutyPositions");
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Title)
				.HasMaxLength(50)
				.IsRequired()
				.IsUnicode();
		}
	}
}
