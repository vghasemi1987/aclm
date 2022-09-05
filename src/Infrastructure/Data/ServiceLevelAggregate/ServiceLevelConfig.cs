using DomainEntities.ServiceLevelAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ServiceLevelAggregate
{
	public class ServiceLevelConfig : IEntityTypeConfiguration<ServiceLevel>
	{
		public void Configure(EntityTypeBuilder<ServiceLevel> builder)
		{
			builder.ToTable("ServiceLevels");
			builder.HasKey(s => s.Id);
			builder.Property(s => s.Title)
				.IsRequired()
				.HasMaxLength(50)
				.IsUnicode();
			//builder.HasData(new ServiceLevel { Id = 1, Title = "عادی", RecordStatus = false },
			//    new ServiceLevel { Id = 1, Title = "مهم", RecordStatus = false },
			//    new ServiceLevel { Id = 1, Title = "ویژه", RecordStatus = false },
			//    new ServiceLevel { Id = 1, Title = "حساس", RecordStatus = false });
		}
	}
}
