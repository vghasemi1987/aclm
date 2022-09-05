
using DomainEntities.AccessRangeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.AccessRangeAggregate
{
	public class AccessRangeHeaderConfig : IEntityTypeConfiguration<AccessRangeHeader>
	{
		public void Configure(EntityTypeBuilder<AccessRangeHeader> builder)
		{
			builder.ToTable("AccessRangeHeaders");
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Title)
				.HasMaxLength(50)
				.IsRequired()
				.IsUnicode();
		}
	}
}
