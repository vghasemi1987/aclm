using DomainEntities.ApplicationUserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ApplicationUserAggregate
{
	public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder.ToTable("ApplicationUserItems");
			builder.Property(o => o.Picture)
				.IsUnicode()
				.HasMaxLength(150);
			builder.Property(o => o.FirstName)
				.IsRequired()
				.IsUnicode()
				.HasMaxLength(150);
			builder.Property(o => o.LastName)
				.IsRequired()
				.IsUnicode()
				.HasMaxLength(150);
			builder.Property(o => o.PersonnelCode)
				.HasMaxLength(10);

			builder.Property(o => o.RegDateTime)
				.IsRequired();

			builder.HasOne(p => p.Department).WithMany().
				 HasForeignKey(p => p.DepartmentId).
			 OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasOne(p => p.Position).WithMany().
				 HasForeignKey(p => p.PositionId).
			 OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasOne(p => p.DutyPosition).WithMany().
				HasForeignKey(p => p.DutyPositionId).
			OnDelete(DeleteBehavior.ClientSetNull);

			builder.Property(o => o.ExpertPerformanceToken)
				.IsUnicode()
				.HasMaxLength(100);
			builder.Property(o => o.QualificationsToken)
			   .IsUnicode()
			   .HasMaxLength(100);

			builder.Property(o => o.QualificationsToken_Second)
			   .IsUnicode()
			   .HasMaxLength(100);

			builder.HasData(
				new ApplicationUser
				{
					Id = 1,
					AccessFailedCount = 0,
					ConcurrencyStamp = "a0c979d1-f65e-4122-b62f-78b5b8df30da",
					Email = "info@test.com",
					EmailConfirmed = false,
					LockoutEnabled = true,
					NormalizedEmail = "INFO@TEST.COM",
					NormalizedUserName = "ADMIN",
					PasswordHash =
						"AQAAAAEAACcQAAAAEFQSCRc6wVL8pu5ChTDI4xT2A5LQ2okSnHseUkzOj0SfLwNzOdHLlhSHaf+lR3jv9A==",
					PhoneNumberConfirmed = false,
					SecurityStamp = "68bccbf3-564b-4f50-b58e-def000a99746",
					TwoFactorEnabled = false,
					FirstName = "مدیر",
					LastName = "سایت",
					UserName = "admin",
				}
			);
		}
	}
}