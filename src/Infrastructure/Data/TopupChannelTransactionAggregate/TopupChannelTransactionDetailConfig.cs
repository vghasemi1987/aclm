using DomainEntities.TopupChannelTransaction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.TopupChannelTransactionAggregate
{
	public class TopupChannelTransactionDetailConfig : IEntityTypeConfiguration<TopupChannelTransactionDetail>
	{
		public void Configure(EntityTypeBuilder<TopupChannelTransactionDetail> builder)
		{
			builder.ToTable("TopupChannelTransactionDetails");
			builder.Property(p => p.FollowupCode).HasMaxLength(50).IsUnicode();
			builder.Property(p => p.CustomerAccountNumber).HasMaxLength(50).IsUnicode();
			builder.Property(p => p.FollowupCode2).HasMaxLength(50).IsUnicode();
			builder.Property(p => p.Extra1).HasMaxLength(50).IsUnicode();
			builder.Property(p => p.Extra2).HasMaxLength(20).IsUnicode();
			builder.Property(p => p.ChannelType).HasMaxLength(10).IsUnicode();
			builder.Property(p => p.TransactionDate).HasMaxLength(20).IsUnicode();
			builder.Property(p => p.MobileNumber).HasMaxLength(50).IsUnicode();
			builder.Property(p => p.AmountText).HasMaxLength(50).IsUnicode();
			builder.Property(p => p.TransactionStatus).HasMaxLength(10).IsUnicode();
			builder.Property(p => p.OperatorName).HasMaxLength(50).IsUnicode();


		}
	}
}
