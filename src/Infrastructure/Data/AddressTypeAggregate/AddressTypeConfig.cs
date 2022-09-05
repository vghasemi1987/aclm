using DomainEntities.AddressTypeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.AddressTypeAggregate
{
	public class AddressTypeConfig : IEntityTypeConfiguration<AddressType>
	{
		public void Configure(EntityTypeBuilder<AddressType> builder)
		{
			builder.ToTable("AddressTypes");
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
