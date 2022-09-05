using DomainEntities.ServiceAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ServiceAggregate
{
	public class ServiceConfig : IEntityTypeConfiguration<Service>
	{
		public void Configure(EntityTypeBuilder<Service> builder)
		{
			builder.ToTable("Services");
			builder.HasKey(s => s.Id);
			builder.Property(s => s.Name)
				.IsRequired()
				.HasMaxLength(50)
				.IsUnicode();

			builder.Property(s => s.Description)
			   .IsRequired()
			   .HasMaxLength(200)
			   .IsUnicode();

			builder.HasOne(p => p.Protocol).WithMany().
				HasForeignKey(p => p.ProtocolId).OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(p => p.ServiceLevel).WithMany().
				HasForeignKey(p => p.ServiceLevelId).OnDelete(DeleteBehavior.ClientSetNull);
		}
	}
}
