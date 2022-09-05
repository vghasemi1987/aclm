using DomainEntities.PolicyAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.PolicyAggregate
{
	public class PolicyConfig : IEntityTypeConfiguration<Policy>
	{
		public void Configure(EntityTypeBuilder<Policy> builder)
		{
			builder.ToTable("Policies");
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Title)
				.HasMaxLength(50)
				.IsUnicode();
			builder.Property(p => p.Description)
				.HasMaxLength(200)
				.IsUnicode();
			builder.HasOne(p => p.Protocol).WithMany().
				HasForeignKey(p => p.ProtocolId).OnDelete(DeleteBehavior.ClientSetNull);
		}
	}
}
