using DomainEntities.BroadcastAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.BroadCastAggregate
{
	public class ProtectionOfficeConfig : IEntityTypeConfiguration<ProtectionOffice>
	{
		public void Configure( EntityTypeBuilder<ProtectionOffice> builder )
		{
			builder.ToTable( "ProtectionOffices" );
			builder.Property( c => c.Title ).IsRequired( true ).IsUnicode( true ).HasMaxLength( 50 );
		}
	}
	public class ProtectionOfficeMemberConfig : IEntityTypeConfiguration<ProtectionOfficeMember>
	{
		public void Configure( EntityTypeBuilder<ProtectionOfficeMember> builder )
		{
			builder.ToTable( "ProtectionOfficeMember" );
			//builder.HasOne(a => a.ProtectionOffice).WithMany(e => e.ProtectionOfficeMembers);
			//builder.HasOne(a => a.ApplicationUser).WithOne(e => e.ProtectionOfficeMember);
		}
	}
}
