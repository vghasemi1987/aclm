using DomainEntities.ServiceGroupAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ServiceGroupAggregate
{
	public class ServiceGroupConfig : IEntityTypeConfiguration<ServiceGroup>
	{
		public void Configure(EntityTypeBuilder<ServiceGroup> builder)
		{
			builder.ToTable("ServiceGroups");
			builder.HasKey(s => s.Id);
			builder.Property(s => s.Name)
				.HasMaxLength(50)
				.IsUnicode();

			builder.Property(s => s.Description)
					.HasMaxLength(256)
					.IsUnicode();
		}
	}
}
