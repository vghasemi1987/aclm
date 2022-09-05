using DomainEntities.ActionTypeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ActionTypeAggregate
{
	public class ActionTypeConfig : IEntityTypeConfiguration<ActionType>
	{
		public void Configure(EntityTypeBuilder<ActionType> builder)
		{
			builder.ToTable("ActionTypes");
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Title)
				.HasMaxLength(50)
				.IsRequired()
				.IsUnicode();
		}
	}
}
