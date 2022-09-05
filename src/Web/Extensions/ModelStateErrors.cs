using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace Web.Extensions
{
	public static class ModelStateErrors
	{
		public static string GetErrors(this ModelStateDictionary modelState)
		{
			return string.Join("<br />", (from item in modelState
										  where item.Value.Errors.Any()
										  select item.Value.Errors[0].ErrorMessage).ToList());
		}
	}
}
