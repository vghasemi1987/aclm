using DomainEntities.BroadcastAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.BroadCastAggregate
{
	public class BroadCastConfig : IEntityTypeConfiguration<BroadCast>
	{
		public void Configure(EntityTypeBuilder<BroadCast> builder)
		{
			builder.Property(c => c.FirstName)
				.HasMaxLength(50)
				.IsUnicode(true)
				.IsRequired(false);
			builder.Property(c => c.LastName)
				.HasMaxLength(50)
				.IsUnicode(true)
				.IsRequired(false);
			builder.Property(c => c.Subject)
				.HasMaxLength(100)
				.IsUnicode(true)
				.IsRequired(true);
			builder.Property(c => c.Text)
				.HasMaxLength(2000)
				.IsUnicode(true)
				.IsRequired(true);


			builder.Ignore(c => c.CreateDatePersian);
		}
	}
}