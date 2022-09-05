using DomainEntities.RouterAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.RouterAggregate
{
	public class RouterConfig : IEntityTypeConfiguration<Router>
	{
		public void Configure(EntityTypeBuilder<Router> builder)
		{
			builder.ToTable("Routers");
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Name)
				.HasMaxLength(50)
				.IsUnicode();
			builder.Property(p => p.AccessNo)
				.HasMaxLength(50)
				.IsUnicode();
		}
	}
}
