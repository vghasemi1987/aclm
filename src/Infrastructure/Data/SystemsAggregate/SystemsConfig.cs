using DomainEntities.SystemsAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.SystemsAggregate
{
	public class SystemsConfig : IEntityTypeConfiguration<Systems>
	{
		public void Configure(EntityTypeBuilder<Systems> builder)
		{
			builder.ToTable("Systems");
			builder.HasKey(s => s.Id);
			builder.Property(s => s.SystemName)
				.IsRequired()
				.HasMaxLength(50)
				.IsUnicode();
			builder.Property(s => s.IpAddress)
			   .HasMaxLength(50)
			   .IsUnicode();
			builder.Property(s => s.Description)
			   .HasMaxLength(250)
			   .IsUnicode();
			builder.Property(s => s.FirstName)
			  .HasMaxLength(50)
			  .IsUnicode();
			builder.Property(s => s.LastName)
			  .HasMaxLength(50)
			  .IsUnicode();
			builder.HasOne(p => p.AccessibilityLevel).WithMany().
				HasForeignKey(p => p.AccessibilityLevelId).
				OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasOne(p => p.InformationAccessibilityLevel).WithMany().
				 HasForeignKey(p => p.InformationAccessibilityLevelId).
				 OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasOne(p => p.Department).WithMany().
			 HasForeignKey(p => p.DepartmentId).
			 OnDelete(DeleteBehavior.ClientSetNull);
		}
	}
}
