using DomainEntities.DepartmentAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.DepartmentAggregate
{
	public class DepartmentConfig : IEntityTypeConfiguration<Department>
	{
		public void Configure(EntityTypeBuilder<Department> builder)
		{
			builder.ToTable("Departments");
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Name)
				.HasMaxLength(50)
				.IsUnicode();
		}
	}
}
