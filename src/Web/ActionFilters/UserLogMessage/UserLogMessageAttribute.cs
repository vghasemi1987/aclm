using DomainContracts.BroadcastAggregate;
using DomainContracts.Commons;
using DomainEntities.ApplicationUserAggregate;
using DomainEntities.BroadcastAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.ComponentModel;
using System.Reflection;

namespace Web.ActionFilters
{
	public class UserLogMessageAttribute : IActionFilter//ActionFilterAttribute
	{
		private readonly IUserLogMessageRepository _userLogMessageRepository;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IUnitOfWork _unitOfWork;
		public UserLogMessageAttribute(IUserLogMessageRepository userLogMessageRepository, UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
		{
			_userLogMessageRepository = userLogMessageRepository;
			_userManager = userManager;
			_unitOfWork = unitOfWork;
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{

			var controller = context.RouteData.Values["controller"].ToString();
			var action = context.RouteData.Values["action"].ToString();
			string description = "";
			if (context?.ActionDescriptor is ControllerActionDescriptor descriptor)
			{
				description = $"{descriptor.MethodInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName} _" +
					$"{descriptor.ControllerTypeInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName}";
			}


			UserLogMessage userLogMessage = new()
			{
				Action = action,
				Controller = controller,
				UserName = context.HttpContext.User.Identity.Name,
				DateActivity = DateTime.Now,
				Description = description,
				IP = context.HttpContext.Connection.RemoteIpAddress.ToString(),
			};
			_userLogMessageRepository.Add(userLogMessage);
			_unitOfWork.SaveAsync();
		}

		//public override void OnActionExecuted(ActionExecutedContext context)
		//{
		//	var controller = context.RouteData.Values["controller"].ToString();
		//	var action = context.RouteData.Values["action"].ToString();
		//	UserLogMessage userLogMessage = new()
		//	{
		//		Action = action,
		//		Controller = controller,
		//		UserName = context.HttpContext.User.Identity.Name,
		//		DateActivity = DateTime.Now,
		//		Description = ""
		//	};
		//	_userLogMessageRepository.Add(userLogMessage);
		//	_unitOfWork.SaveAsync();
		//}

		public void OnActionExecuting(ActionExecutingContext context)
		{

		}
	}
}