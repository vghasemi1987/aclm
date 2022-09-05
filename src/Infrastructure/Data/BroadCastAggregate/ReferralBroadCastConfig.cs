using DomainEntities.BroadcastAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.BroadCastAggregate
{
	public class ReferralBroadCastConfig : IEntityTypeConfiguration<ReferralBroadCast>
	{
		public void Configure(EntityTypeBuilder<ReferralBroadCast> builder)
		{
			builder.Property(c => c.Description)
				.HasMaxLength(500);

			builder.HasOne(q => q.BroadCast)
				.WithMany(q => q.ReferralBroadCasts)
				.HasForeignKey(q => q.BroadCastId);


			builder.HasOne(c => c.ApplicationUser).WithMany(q => q.referralBroadCast).HasForeignKey(q => q.DstUserID);
			//Ignore
			builder.Ignore(c => c.DeadLinePersian);
		}
	}
}