using DomainEntities.AclFilesRecordAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.AclFileUploadAggregate
{
	public class AclFilesRecordConfig : IEntityTypeConfiguration<AclFilesRecord>
	{
		public void Configure(EntityTypeBuilder<AclFilesRecord> builder)
		{
			builder.ToTable("AclFilesRecords");
			builder.HasKey(p => p.Id);
			builder.Property(p => p.SourceIp)
				.HasMaxLength(50)
				.IsUnicode();
			builder.Property(p => p.DestinationIp)
			   .HasMaxLength(50)
			   .IsUnicode();
			builder.Property(p => p.Action)
			  .HasMaxLength(50)
			  .IsUnicode();
			builder.Property(p => p.SourceIp2)
				.HasMaxLength(50)
				.IsUnicode();
			builder.Property(p => p.DestinationIp2)
			   .HasMaxLength(50)
			   .IsUnicode();
			builder.Property(p => p.SourcePort)
			   .HasMaxLength(50)
			   .IsUnicode();
			builder.Property(p => p.DestinationPort)
			  .HasMaxLength(50)
			  .IsUnicode();
			builder.Property(p => p.Protocol)
				.HasMaxLength(50)
				.IsUnicode();
			builder.Property(p => p.RouterNo)
				.HasMaxLength(50)
				.IsUnicode();
			builder.HasOne(p => p.SourceAddressType).
				WithMany().HasForeignKey(p => p.SourceAddressTypeId).
				OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasOne(p => p.DestinationAddressType).
				WithMany().HasForeignKey(p => p.DestinationAddressTypeId).
				OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasOne(p => p.AclFilesUpload).
			   WithMany(p => p.AclFilesRecords).HasForeignKey(p => p.AclFilesUploadId).
			   OnDelete(DeleteBehavior.ClientSetNull);
		}
	}
}
