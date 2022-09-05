using DomainEntities.AccessibilityAggregate;
using DomainEntities.ApplicationUserAggregate;
using DomainEntities.SearchLogAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Linq;
using System.Reflection;


namespace Infrastructure.Data.Commons
{
	public class ServerAccessibilityMonitorContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, IdentityUserClaim<int>,
		ApplicationUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
	{
		//private readonly int _user;
		public ServerAccessibilityMonitorContext(DbContextOptions<ServerAccessibilityMonitorContext> options) : base(options)
		{
			//Database.EnsureDeleted();
			//Database.EnsureCreated();
			//_user = userService.GetUser();
		}

		public DbSet<AccessibilityReportByCount> AccessibilityReportByCount { get; set; }
		public DbSet<AccessibilityReportByFilter> AccessibilityReportByFilter { get; set; }
		public DbSet<AbundanceReport> AbundanceReports { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
						 .Where(t => t.GetInterfaces().Any(gi => gi.IsGenericType && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))).ToList();

			foreach (var type in typesToRegister)
			{
				dynamic configurationInstance = Activator.CreateInstance(type);
				builder.ApplyConfiguration(configurationInstance);
			}
			builder.Entity<IdentityUserClaim<int>>(entity => entity.ToTable("ApplicationUser_Claims"));
			builder.Entity<IdentityUserLogin<int>>(entity => entity.ToTable("ApplicationUser_Logins"));
			builder.Entity<IdentityUserToken<int>>(entity => entity.ToTable("ApplicationUser_Tokens"));
		}
		public class ServerAccessibilityMonitorContextFactory : IDesignTimeDbContextFactory<ServerAccessibilityMonitorContext>
		{
			public ServerAccessibilityMonitorContext CreateDbContext(string[] args)
			{
				var builder = new DbContextOptionsBuilder<ServerAccessibilityMonitorContext>();
				builder.UseSqlServer("Data Source=.;Password=Ss1234!@#$;Initial Catalog=SamNew000331;User ID=sa;Persist Security Info=true;MultipleActiveResultSets=True");
				return new ServerAccessibilityMonitorContext(builder.Options);
			}
		}
	}
}