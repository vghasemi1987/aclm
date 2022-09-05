using DomainContracts.ServiceAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Extensions.Filters;

namespace Web.Core.SearchLogs
{
	[Authorize, ApiValidateModelStateFilter]
	[Route("api/[controller]")]
	[ApiController]
	public class SearchLogsApiController : ControllerBase
	{
		private readonly ISearchLogDetailRepository _searchLogDetail;
		public SearchLogsApiController(ISearchLogDetailRepository searchLogDetail)
		{
			_searchLogDetail = searchLogDetail;
		}

		public async Task<IActionResult> GetByLetterIdentifier(string letterNo)
		{
			var result = await Task.Run(() => _searchLogDetail.GetByLetterIdentifier(letterNo));
			return new JsonResult(result);
		}
	}
}