using DomainEntities.BroadcastAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.BroadCastAggregate
{
	public class GroupingOfficeConfig : IEntityTypeConfiguration<GroupingOffice>
	{
		public void Configure( EntityTypeBuilder<GroupingOffice> builder )
		{
			builder.ToTable( "GroupingOffice" );
			builder.Property( c => c.Title ).IsRequired( true ).IsUnicode( true ).HasMaxLength( 50 );
		}
	}
	public class GroupingOfficeMemberConfig : IEntityTypeConfiguration<GroupingOfficeMember>
	{
		public void Configure( EntityTypeBuilder<GroupingOfficeMember> builder )
		{
			builder.ToTable( "GroupingOfficeMember" );
			//builder.HasOne( a => a.GroupingOffice ).WithMany( e => e.GroupingOfficeMembers );

			//builder.HasOne( a => a.ApplicationUser ).WithOne( e => e.GroupingOfficeMember );
		}
	}
}
