using DomainEntities.TopupAccountTransaction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.TopupAccountTransactionAggregate
{
	public class TopupAccountTransactionHeaderConfig : IEntityTypeConfiguration<TopupAccountTransactionHeader>
	{
		public void Configure(EntityTypeBuilder<TopupAccountTransactionHeader> builder)
		{
			builder.ToTable("TopupAccountTransactionHeaders");
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
