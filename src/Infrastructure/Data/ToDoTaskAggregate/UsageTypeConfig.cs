using DomainEntities.ToDoTaskAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ToDoTaskAggregate
{
	public class UsageTypeConfig : IEntityTypeConfiguration<UsageType>
	{
		public void Configure(EntityTypeBuilder<UsageType> builder)
		{
			builder.ToTable("ToDoTask_UsageTypes");
			builder.HasKey(o => o.Id);
			builder.Property(current => current.Id)
				.ValueGeneratedNever();

			builder.Property(o => o.Title)
				.IsUnicode()
				.HasMaxLength(50);

			builder.HasData(new State { Id = 1, Title = "تماس تلفنی" },
				new State { Id = 2, Title = "جلسه" },
				new State { Id = 3, Title = "یادآوری" });
		}
	}
}