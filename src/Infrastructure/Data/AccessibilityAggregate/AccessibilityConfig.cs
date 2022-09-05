using DomainEntities.AccessibilityAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.AclFileUploadAggregate
{
	public class AccessibilityConfig : IEntityTypeConfiguration<Accessibility>
	{
		public void Configure(EntityTypeBuilder<Accessibility> builder)
		{
			builder.ToTable("Accessibilities");
			builder.HasKey(p => p.Id);

			builder.Property(p => p.SourceIp)
				.HasMaxLength(50)
				.IsUnicode();
			builder.Property(p => p.DestinationIp)
			   .HasMaxLength(50)
			   .IsUnicode();
			builder.Property(p => p.Port)
			  .HasMaxLength(50)
			  .IsUnicode();
			builder.Property(p => p.SourcePort)
			   .HasMaxLength(50)
			   .IsUnicode();
			builder.Property(p => p.DestinationPort)
			  .HasMaxLength(50)
			  .IsUnicode();
			builder.Property(p => p.ActionType)
			   .HasMaxLength(50)
			   .IsUnicode();
			builder.Property(p => p.Protocol)
			  .HasMaxLength(50)
			  .IsUnicode();
			builder.HasOne(p => p.SourceSystem).
			  WithMany().
			  HasForeignKey(p => p.SourceSystemId).
			  OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasOne(p => p.DestinationSystem).
			  WithMany().
			  HasForeignKey(p => p.DestinationSystemId).
			  OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasOne(p => p.Service).
			  WithMany().
			  HasForeignKey(p => p.ServiceId).
			  OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasOne(p => p.Router).
			 WithMany().
			 HasForeignKey(p => p.RouterId).
			 OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasOne(p => p.ActionTypes).
			 WithMany().
			 HasForeignKey(p => p.ActionTypesId).
			 OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasOne(p => p.AclFilesUpload).
			 WithMany().
			 HasForeignKey(p => p.AclFilesUploadId).
			 OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasOne(p => p.Protocols).
			 WithMany().
			 HasForeignKey(p => p.ProtocolsId).
			 OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasOne(p => p.User).
			WithMany().
			HasForeignKey(p => p.UserId).
			OnDelete(DeleteBehavior.ClientSetNull);

		}
	}
}
