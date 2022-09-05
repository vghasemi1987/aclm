using DomainEntities.BroadcastAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.BroadCastAggregate
{
	public class MessageHandlerConfig : IEntityTypeConfiguration<MessageHandler>
	{
		public void Configure(EntityTypeBuilder<MessageHandler> builder)
		{
			builder.Property(c => c.Title).HasMaxLength(50);
			builder.Property(c => c.Description).HasMaxLength(500);
		}
	}
}
