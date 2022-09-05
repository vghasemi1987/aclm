using DomainEntities.Transaction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.TransactionAggregate
{
	public class TransactionHeaderConfig : IEntityTypeConfiguration<TransactionHeader>
	{
		public void Configure(EntityTypeBuilder<TransactionHeader> builder)
		{
			builder.ToTable("TransactionHeaders");
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
