using DomainEntities.ApplicationUserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ApplicationUserAggregate
{
	public class ApplicationRoleAuthorityConfig : IEntityTypeConfiguration<ApplicationRoleAuthority>
	{
		public void Configure(EntityTypeBuilder<ApplicationRoleAuthority> builder)
		{
			builder.ToTable("ApplicationRoleAuthorities");

			builder.HasKey(p => p.Id);

			builder.HasOne(p => p.Authority)
				.WithMany(p => p.ApplicationRoleAuthorities)
				.HasForeignKey(p => p.AuthorityId).OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(p => p.ApplicationRole)
				.WithMany(p => p.ApplicationRoleAuthorities)
				.HasForeignKey(p => p.ApplicationRoleId).OnDelete(DeleteBehavior.Cascade);
		}
	}
}
