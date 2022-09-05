using DomainEntities.ToDoTaskAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ToDoTaskAggregate
{
	public class StateConfig : IEntityTypeConfiguration<State>
	{
		public void Configure(EntityTypeBuilder<State> builder)
		{
			builder.ToTable("ToDoTask_States");
			builder.HasKey(o => o.Id);
			builder.Property(current => current.Id)
				.ValueGeneratedNever();

			builder.Property(o => o.Title)
				.IsUnicode()
				.HasMaxLength(50);

			builder.HasData(new State { Id = 1, Title = "شروع نشده" },
				new State { Id = 2, Title = "در حال انجام" },
				new State { Id = 3, Title = "انجام شد" });
		}
	}
}