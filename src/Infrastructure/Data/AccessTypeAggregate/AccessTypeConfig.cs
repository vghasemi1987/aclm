using DomainEntities.AccessTypeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.AccessTypeAggregate
{
	public class AccessTypeConfig : IEntityTypeConfiguration<AccessType>
	{
		public void Configure(EntityTypeBuilder<AccessType> builder)
		{
			builder.ToTable("AccessTypes");
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
