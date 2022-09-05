using DomainEntities.SearchLogAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.SearchLogAggregate
{
	public class SearchLogTransactionDetailConfig : IEntityTypeConfiguration<SearchLogTransactionDetail>
	{
		public void Configure(EntityTypeBuilder<SearchLogTransactionDetail> builder)
		{
			builder.ToTable("SearchLogTransactionDetail");

			builder.HasKey(p => p.Id);

			builder.HasOne(p => p.SearchLogDetail)
				.WithMany(p => p.SearchLogTransactionDetails)
				.HasForeignKey(p => p.SearchLogDetailId).OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(p => p.TransactionDetail)
				.WithMany(p => p.SearchLogTransactionDetails)
				.HasForeignKey(p => p.TransactionDetailId).OnDelete(DeleteBehavior.Cascade);
		}
	}
}
