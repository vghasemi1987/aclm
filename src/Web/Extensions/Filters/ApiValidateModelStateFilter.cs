using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Web.Extensions.Filters
{
	public class ApiValidateModelStateFilter : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if (context.ModelState.IsValid)
			{
				return;
			}

			var validationErrors = string.Join("", context.ModelState
				.Keys
				.SelectMany(k => context.ModelState[k].Errors)
				.Select(e => e.ErrorMessage));

			context.Result = new BadRequestObjectResult(validationErrors);
		}
	}
}
