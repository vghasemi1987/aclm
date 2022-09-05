using DomainEntities.InvalidFileItemAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.InvalidFileItemAggregate
{
	public class InvalidFileItemConfig : IEntityTypeConfiguration<InvalidFileItem>
	{
		public void Configure(EntityTypeBuilder<InvalidFileItem> builder)
		{
			builder.ToTable("InvalidFileItems");
			builder.HasKey(p => p.Id);
			builder.Property(p => p.InvalidItemTitle)
				.IsRequired()
				.IsUnicode();
			builder.Property(p => p.LineNumber)
				.IsRequired();
			builder.HasOne(p => p.AclFilesUpload).
			 WithMany(p => p.InvalidFileItems).
			 HasForeignKey(p => p.AclFilesUploadId).
			 OnDelete(DeleteBehavior.ClientSetNull);

		}
	}
}
