using DomainEntities.TopupAccountTransaction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.TopupAccountTransactionAggregate
{
	public class TopupAccountTransactionDetailConfig : IEntityTypeConfiguration<TopupAccountTransactionDetail>
	{
		public void Configure(EntityTypeBuilder<TopupAccountTransactionDetail> builder)
		{
			builder.ToTable("TopupAccountTransactionDetails");
			builder.Property(p => p.TransactionDate).HasMaxLength(20).IsUnicode();
			builder.Property(p => p.TransactionTime).HasMaxLength(10).IsUnicode();
			builder.Property(p => p.TransactionType).HasMaxLength(10).IsUnicode();
			builder.Property(p => p.TransactionStatus).HasMaxLength(10).IsUnicode();
			builder.Property(p => p.TransactionAmountText).HasMaxLength(50).IsUnicode();
			builder.Property(p => p.RefrenceCode).HasMaxLength(20).IsUnicode();
			builder.Property(p => p.Extra1).HasMaxLength(100).IsUnicode();
			builder.Property(p => p.Extra2).HasMaxLength(100).IsUnicode();
			builder.Property(p => p.Extra3).HasMaxLength(100).IsUnicode();
			builder.Property(p => p.Extra4).HasMaxLength(100).IsUnicode();
			builder.Property(p => p.BranchCode).HasMaxLength(20).IsUnicode();
			builder.Property(p => p.FollowupCode2).HasMaxLength(50).IsUnicode();
			builder.Property(p => p.AccountNumber).HasMaxLength(30).IsUnicode();
			builder.Property(p => p.CustomerAccountNumber).HasMaxLength(30).IsUnicode();
			builder.Property(p => p.FollowupCode).HasMaxLength(50).IsUnicode();
		}
	}
}
