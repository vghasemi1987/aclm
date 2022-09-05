using DomainEntities.AccessibilityRequestAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.AclFileUploadAggregate
{
	public class AccessibilityRequestConfig : IEntityTypeConfiguration<AccessibilityRequest>
	{
		public void Configure(EntityTypeBuilder<AccessibilityRequest> builder)
		{
			builder.ToTable("AccessibilityRequests");
			builder.HasKey(p => p.Id);
			builder.Property(p => p.LetterNo)
				.HasMaxLength(50)
				.IsUnicode();
			builder.Property(p => p.SourceIp)
				.HasMaxLength(50)
				.IsUnicode();
			builder.Property(p => p.DestinationIp)
			   .HasMaxLength(50)
			   .IsUnicode();
			builder.Property(p => p.PhoneNumber)
				  .HasMaxLength(50)
				  .IsUnicode();
			builder.Property(p => p.Description)
				  .HasMaxLength(250)
				  .IsUnicode();

			builder.HasOne(p => p.RequestDepartment).
			   WithMany().HasForeignKey(p => p.RequestDepartmentId).
			   OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasOne(p => p.SourceSystem).
			  WithMany().HasForeignKey(p => p.SourceSystemId).
			  OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasOne(p => p.DestinationSystem).
			  WithMany().HasForeignKey(p => p.DestinationSystemId).
			  OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasOne(p => p.Service).
			  WithMany().HasForeignKey(p => p.ServiceId).
			  OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasOne(p => p.DestinationService).
			 WithMany().HasForeignKey(p => p.DestinationServiceId).
			 OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasOne(p => p.UserDepartment).
			  WithMany().HasForeignKey(p => p.UserDepartmentId).
			  OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasOne(p => p.AccessibilityType).
			  WithMany().HasForeignKey(p => p.AccessibilityTypeId).
			  OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasOne(p => p.RequestingUser).
			  WithMany().HasForeignKey(p => p.RequestingUserId).
			  OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasOne(p => p.ConfirmUser).
			  WithMany().HasForeignKey(p => p.ConfirmUserId).
			  OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasOne(p => p.SourceAccessibilityLevel).
			 WithMany().HasForeignKey(p => p.SourceAccessibilityLevelId).
			 OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasOne(p => p.DestAccessibilityLevel).
				WithMany().HasForeignKey(p => p.DestAccessibilityLevelId).
				OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasOne(p => p.SourceProtocol).
			   WithMany().HasForeignKey(p => p.SourceProtocolId).
			   OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasOne(p => p.DestinationProtocol).
			   WithMany().HasForeignKey(p => p.DestinationProtocolId).
			   OnDelete(DeleteBehavior.ClientSetNull);
		}
	}
}
