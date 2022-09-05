using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Web.Extensions.Attributes
{
	public abstract class AttributeAuthorizationHandler<TRequirement, TAttribute> : AuthorizationHandler<TRequirement> where TRequirement : IAuthorizationRequirement where TAttribute : Attribute
	{
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement)
		{
			var attributes = new List<TAttribute>();

			if ((context.Resource as AuthorizationFilterContext)?.ActionDescriptor is ControllerActionDescriptor action)
			{
				attributes.AddRange(GetAttributes(action.ControllerTypeInfo.UnderlyingSystemType));
				attributes.AddRange(GetAttributes(action.MethodInfo));
			}

			return HandleRequirementAsync(context, requirement, attributes);
		}

		protected abstract Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement, IEnumerable<TAttribute> attributes);

		private static IEnumerable<TAttribute> GetAttributes(ICustomAttributeProvider memberInfo)
		{
			return memberInfo.GetCustomAttributes(typeof(TAttribute), false).Cast<TAttribute>();
		}
	}
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
	public sealed class PermissionAttribute : AuthorizeAttribute
	{
		//public string Name { get; }

		public PermissionAttribute(/*string name*/) : base("Permission")
		{
			//Name = name;
		}
	}
	public class PermissionAuthorizationRequirement : IAuthorizationRequirement
	{
	}
	public class PermissionAuthorizationHandler : AttributeAuthorizationHandler<PermissionAuthorizationRequirement, PermissionAttribute>
	{
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement, IEnumerable<PermissionAttribute> attributes)
		{
			var permissionName = string.Empty;
			var mvcContext = context.Resource as AuthorizationFilterContext;
			if (mvcContext?.ActionDescriptor is ControllerActionDescriptor descriptor)
			{
				permissionName = $"{descriptor.ControllerName}_{descriptor.ActionName}";
			}

			if (attributes.Any(permissionAttribute => context.User == null || !context.User.HasClaim(permissionAttribute.Policy, permissionName)))
			{
				context.Fail();
			}
			context.Succeed(requirement);
			return Task.CompletedTask;
		}
	}
}