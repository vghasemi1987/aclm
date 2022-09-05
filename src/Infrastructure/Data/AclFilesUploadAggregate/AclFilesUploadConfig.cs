using DomainEntities.AclFilesUploadAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.AclFileUploadAggregate
{
	public class AclFilesUploadConfig : IEntityTypeConfiguration<AclFilesUpload>
	{
		public void Configure(EntityTypeBuilder<AclFilesUpload> builder)
		{
			builder.ToTable("AclFilesUpload");
			builder.HasKey(p => p.Id);
			builder.Property(p => p.FileName)
				.HasMaxLength(50)
				.IsRequired()
				.IsUnicode();
			//builder.Property(p => p.Files)
			//   .HasMaxLength(50)
			//   .IsUnicode();
			builder.Property(p => p.Description)
			  .HasMaxLength(200)
			  .IsUnicode();
			builder.HasOne(p => p.Router).
				WithMany().HasForeignKey(p => p.RouterId).
				OnDelete(DeleteBehavior.ClientSetNull);
		}
	}
}
