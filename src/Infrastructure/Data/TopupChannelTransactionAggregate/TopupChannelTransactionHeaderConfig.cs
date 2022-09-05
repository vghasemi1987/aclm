using DomainEntities.TopupChannelTransaction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.TopupChannelTransactionAggregate
{
	public class TopupChannelTransactionHeaderConfig : IEntityTypeConfiguration<TopupChannelTransactionHeader>
	{
		public void Configure(EntityTypeBuilder<TopupChannelTransactionHeader> builder)
		{
			builder.ToTable("TopupChannelTransactionHeaders");
			builder.Property(p => p.Title)
				.HasMaxLength(50).IsUnicode();
			builder.Property(p => p.Description)
				.HasMaxLength(50).IsUnicode();
			builder.HasOne(p => p.User).WithMany()
				.HasForeignKey(p => p.UserId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
