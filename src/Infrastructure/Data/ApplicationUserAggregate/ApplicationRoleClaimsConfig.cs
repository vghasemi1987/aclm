using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ApplicationUserAggregate
{
	public class ApplicationRoleClaimsConfig : IEntityTypeConfiguration<IdentityRoleClaim<int>>
	{
		public void Configure(EntityTypeBuilder<IdentityRoleClaim<int>> builder)
		{
			builder.ToTable("ApplicationUser_RoleClaims");

			builder.HasData(new IdentityRoleClaim<int> { Id = 1, ClaimType = "Permission", ClaimValue = "AccessRights_Index", RoleId = 1 },
				new IdentityRoleClaim<int> { Id = 2, ClaimType = "Permission", ClaimValue = "AccessRights_GetDetail", RoleId = 1 },
				new IdentityRoleClaim<int> { Id = 3, ClaimType = "Permission", ClaimValue = "AccessRights_AddDetail", RoleId = 1 },
				new IdentityRoleClaim<int> { Id = 4, ClaimType = "Permission", ClaimValue = "AccessRights_EditDetail", RoleId = 1 },
				new IdentityRoleClaim<int> { Id = 5, ClaimType = "Permission", ClaimValue = "AccessRights_DeleteRows", RoleId = 1 });
		}
	}
}