using DomainEntities.ApplicationUserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ApplicationUserAggregate
{
	public class ApplicationUserAccessRageHeaderConfig : IEntityTypeConfiguration<ApplicationUserAccessRageHeader>
	{
		public void Configure(EntityTypeBuilder<ApplicationUserAccessRageHeader> builder)
		{
			builder.ToTable("ApplicationUserAccessRageHeaders");

			builder.HasKey(p => p.Id);

			builder.HasOne(p => p.AccessRangeHeader).
				  WithMany(p => p.ApplicationUserAccessRageHeaders).
				  HasForeignKey(p => p.AccessRangeHeaderId).
				  OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(p => p.ApplicationUser).
				 WithMany(p => p.ApplicationUserAccessRageHeaders).
				 HasForeignKey(p => p.ApplicationUserId).
				 OnDelete(DeleteBehavior.Cascade);
		}
	}
}
