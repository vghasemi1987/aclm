
using DomainEntities.AccessRangeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.AccessRangeAggregate
{
	public class AccessRangeDetailConfig : IEntityTypeConfiguration<AccessRangeDetail>
	{
		public void Configure(EntityTypeBuilder<AccessRangeDetail> builder)
		{
			builder.ToTable("AccessRangeDetails");
			builder.HasKey(p => p.Id);
			builder.Property(p => p.IpFrom)
				.HasMaxLength(50)
				.IsRequired();
			builder.Property(p => p.IpTo)
				.HasMaxLength(50)
				.IsRequired();
			builder.HasOne(p => p.AccessRangeHeader).WithMany().
				HasForeignKey(p => p.AccessRangeHeaderId).OnDelete(DeleteBehavior.Restrict);
		}
	}
}
