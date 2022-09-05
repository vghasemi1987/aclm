using DomainEntities.BroadcastAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.BroadCastAggregate
{
	public class ProtectionOfficeUserRelationConfig : IEntityTypeConfiguration<ProtectionOfficeUserRelation>
	{
		public void Configure(EntityTypeBuilder<ProtectionOfficeUserRelation> builder)
		{
			builder.HasOne(q => q.ProtectionOffice)
				.WithMany(q => q.ProtectionOfficeUserRelations)
				.HasForeignKey(q => q.ProtectionOfficeId);
		}
	}
}