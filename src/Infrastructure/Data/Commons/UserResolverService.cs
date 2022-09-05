using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Data.Commons
{
	public class UserResolverService
	{
		private readonly IHttpContextAccessor _context;
		public UserResolverService(IHttpContextAccessor context)
		{
			_context = context;
		}

		public int GetUser()
		{
			return string.IsNullOrEmpty(_context.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value) ? 0 :
				int.Parse(_context.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
		}
	}
}
