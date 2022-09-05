using DomainEntities.ToDoTaskAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ToDoTaskAggregate
{
	public class ToDoTaskConfig : IEntityTypeConfiguration<ToDoTask>
	{
		public void Configure(EntityTypeBuilder<ToDoTask> builder)
		{
			builder.ToTable("ToDoTasks");

			builder.HasKey(o => o.Id);

			builder.Property(o => o.Title)
				.IsUnicode()
				.HasMaxLength(300);

			builder.Property(o => o.Description)
				.IsUnicode()
				.HasMaxLength(500);

			builder.HasOne(o => o.CreatorUser)
				.WithMany()
				.HasForeignKey(o => o.CreatorUserId);

			builder.HasOne(o => o.AssignedToUser)
				.WithMany()
				.HasForeignKey(o => o.AssignedToUserId);


			//builder.HasMany(o => o.Notes)
			//    .WithOne(o => o.ToDoTask)
			//    .HasForeignKey(o => o.ToDoTaskId)
			//    .OnDelete(DeleteBehavior.Cascade);
			builder.HasMany(o => o.NotificationItems)
				.WithOne(o => o.ToDoTask)
				.HasForeignKey(o => o.ToDoTaskId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}