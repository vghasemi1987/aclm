using DomainContracts.BankBranchAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Core.Common
{
	[Authorize]
	//[DisplayName("کارهای عمومی")]
	public class CommonController : Controller
	{
		private readonly IBankBranchRepository _bankBranchRepository;

		public CommonController(IBankBranchRepository bankBranchRepository)
		{
			_bankBranchRepository = bankBranchRepository;
		}

		//[HttpGet, AllowAnonymous]
		//public async Task<JsonResult> GetCityByProvinceId(int? itemId)
		//{
		//    if (!itemId.HasValue)
		//    {
		//        return Json("Error");
		//    }

		//    var result = await _cityRepository.GetByProvinceId(itemId.Value);
		//    return Json(result);
		//}

		//[HttpGet, AllowAnonymous, AjaxOnly]
		//public async Task<JsonResult> GetBankBranchByCityId(int? itemId)
		//{
		//    if (!itemId.HasValue)
		//    {
		//        return Json("Error");
		//    }

		//    //var result = await _bankBranchRepository.GetByCityId(itemId.Value);
		//    return Json(result);
		//}
	}
}