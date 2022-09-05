using DomainEntities.ProtocolAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ProtocolAggregate
{
	public class ProtocolConfig : IEntityTypeConfiguration<Protocol>
	{
		public void Configure(EntityTypeBuilder<Protocol> builder)
		{
			builder.ToTable("Protocols");
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Name)
				.HasMaxLength(50)
				.IsUnicode();
			builder.Property(p => p.Description)
				.HasMaxLength(200)
				.IsUnicode();
		}
	}
}
