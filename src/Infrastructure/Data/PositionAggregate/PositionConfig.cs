using DomainEntities.PositionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.PositionAggregate
{
	public class PositionConfig : IEntityTypeConfiguration<Position>
	{
		public void Configure(EntityTypeBuilder<Position> builder)
		{
			builder.ToTable("Positions");
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Title)
				.HasMaxLength(50)
				.IsUnicode();
			builder.Property(p => p.Description)
				.HasMaxLength(50)
				.IsUnicode();
		}
	}
}
