using DomainEntities.SearchLogAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.SearchLogAggregate
{
	public class SearchLogDetailConfig : IEntityTypeConfiguration<SearchLogDetail>
	{
		public void Configure(EntityTypeBuilder<SearchLogDetail> builder)
		{
			builder.ToTable("SearchLogDetails");
			builder.HasKey(q => q.Id);
			builder.Property(q => q.FirstName).IsUnicode().HasMaxLength(50);
			builder.Property(q => q.LastName).IsUnicode().HasMaxLength(50);
			builder.Property(q => q.NationalCode).IsUnicode().HasMaxLength(10);
			builder.Property(q => q.FatherName).IsUnicode().HasMaxLength(50);
			builder.Property(q => q.Address).IsUnicode().HasMaxLength(250);
			builder.Property(q => q.SearchTime).IsUnicode().IsRequired().HasMaxLength(50);
			builder.Property(q => q.CardNumber).IsUnicode().HasMaxLength(50);
			builder.HasOne(q => q.User).WithMany().HasForeignKey(q => q.UserId);
		}
	}
}
