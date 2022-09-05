using DomainEntities.AuthorityAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.AuthorityAggregate
{
	public class AuthorityConfig : IEntityTypeConfiguration<Authority>
	{
		public void Configure(EntityTypeBuilder<Authority> builder)
		{
			builder.ToTable("Authorities");
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Title)
				.HasMaxLength(50)
				.IsRequired()
				.IsUnicode();

			builder.Property(p => p.Description)
			  .HasMaxLength(256)
			  .IsUnicode();
		}
	}
}
