using DomainEntities.BroadcastAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Data.BroadCastAggregate
{
	public class UserLogMessageConfig : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<UserLogMessage>
	{
		public void Configure(EntityTypeBuilder<UserLogMessage> builder)
		{
			builder.Property(c => c.Action).HasMaxLength(50);
			builder.Property(c => c.Controller).HasMaxLength(50);
			builder.Property(c => c.Description).HasMaxLength(256);
			builder.Property(c => c.UserName).HasMaxLength(50);
			builder.Property(c => c.IP).HasMaxLength(50);
		}
	}
}
