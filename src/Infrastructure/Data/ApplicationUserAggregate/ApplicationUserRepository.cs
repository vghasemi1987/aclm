namespace Infrastructure.Data.ApplicationUserAggregate
{
	//public class ApplicationUserRepository : EfRepository<ApplicationUser>, IApplicationUserRepository
	//{
	//private DbSet<ApplicationUser> DbSet { get; }
	//public ApplicationUserRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
	//{
	//    DbSet = dbContext.Set<ApplicationUser>();
	//}
	//public async Task<List<ApplicationUser>> GetUserList()
	//{
	//    var data = await DbContext.Users.ToListAsync();
	//    return data;
	//}

	//public async Task<List<ApplicationUser>> GetUserListWithRole(string roleName)
	//{
	//    //var result = DbContext.Users
	//    //    .Join(DbContext.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { u, ur })
	//    //    .Join(DbContext.Roles, ur => ur.ur.RoleId, r => r.Id, (ur, r) => new { ur, r });
	//    var result2 = (await DbContext.Users
	//    .Join(DbContext.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { u, ur })
	//    .Join(DbContext.Roles, ur => ur.ur.RoleId, r => r.Id, (ur, r) => new { ur, r })
	//    .ToListAsync())
	//    .GroupBy(uv => new { uv.ur.u.UserName, uv.ur.u.Email, uv.ur.u.ApplicationUserRoles, uv.ur.ur.ApplicationRole }).Select(r => new ApplicationUser()
	//    {
	//        UserName = r.Key.UserName,
	//        Email = r.Key.Email,
	//        ApplicationUserRoles = r.Key.ApplicationUserRoles,
	//    }).ToList();

	//    return result2;
	//}
	//}
	//public class UsersViewModel
	//{
	//    public string Username { get; set; }
	//    public string Email { get; set; }
	//    public string Role { get; set; }
	//}
}
