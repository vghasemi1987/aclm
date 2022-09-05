using DomainContracts.Commons;
using DomainContracts.InvalidFileItemAggregate;
using KendoHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel;
using Web.Extensions.Attributes;

namespace Web.Core.InvalidFileItems
{
	[Authorize]
	[DisplayName("گزارش استثنایات")]
	public class InvalidFileItemsController : Controller
	{
		private readonly IInvalidFileItemRepository _invalidFileItemRepository;
		private readonly IUnitOfWork _unitOfWork;

		public InvalidFileItemsController(IInvalidFileItemRepository invalidFileItemRepository,
										  IUnitOfWork unitOfWork)
		{
			_invalidFileItemRepository = invalidFileItemRepository;
			_unitOfWork = unitOfWork;
		}
		[Permission]
		[DisplayName("گزارش استثنایات")]
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult GetRecords(string models)
		{
			var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);
			var list = _invalidFileItemRepository.GetList(request);
			return Json(list);
		}
	}
}