using DomainEntities.Transaction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.TransactionAggregate
{
	public class TransactionDetailConfig : IEntityTypeConfiguration<TransactionDetail>
	{
		public void Configure(EntityTypeBuilder<TransactionDetail> builder)
		{
			builder.ToTable("TransactionDetails");
			builder.Property(p => p.IpAddress).HasMaxLength(50).IsUnicode();
			builder.Property(p => p.SourcePan).HasMaxLength(16).IsUnicode();
			builder.Property(p => p.TargetPan).HasMaxLength(16).IsUnicode();
			builder.Property(p => p.SourcePanOrgianl).HasMaxLength(30).IsUnicode();
			builder.Property(p => p.TargetPanOrgianl).HasMaxLength(30).IsUnicode();
			builder.Property(p => p.TransactionTime).HasMaxLength(8).IsUnicode();
			builder.Property(p => p.Tel).HasMaxLength(13).IsUnicode();
			builder.Property(p => p.Status).HasMaxLength(50).IsUnicode();
			builder.Property(p => p.RefNumber).HasMaxLength(10).IsUnicode();
			builder.Property(p => p.Application).HasMaxLength(50).IsUnicode();
			builder.Property(p => p.UserAgent).HasMaxLength(50).IsUnicode();
			builder.Ignore(p => p.TransactionDateString);

			//builder.HasOne(p => p.Header).WithMany().HasForeignKey(p => p.HeaderId).OnDelete(DeleteBehavior.Cascade);
		}
	}
}
