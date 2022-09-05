using DomainEntities.AccessibilityLevelAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.AccessibilityLevelAggregate
{
	public class AccessibilityLevelConfig : IEntityTypeConfiguration<AccessibilityLevel>
	{
		public void Configure(EntityTypeBuilder<AccessibilityLevel> builder)
		{
			builder.ToTable("AccessibilityLevels");
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Title)
				.HasMaxLength(50)
				.IsRequired()
				.IsUnicode();

			//builder.HasData(new AccessibilityLevel{Id=1, Title="عادی",RecordStatus=false },
			//    new AccessibilityLevel { Id = 2, Title = "مهم", RecordStatus = false },
			//    new AccessibilityLevel { Id = 3, Title = "حساس", RecordStatus = false },
			//    new AccessibilityLevel { Id = 4, Title = "حیاتی", RecordStatus = false });

		}
	}
}
