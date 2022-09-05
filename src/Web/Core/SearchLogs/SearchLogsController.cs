using DomainContracts.Commons;
using DomainContracts.ServiceAggregate;
using DomainContracts.TransactionAggregate;
using DomainEntities.SearchLogAggregate;
using KendoHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using Web.Core.SearchLogs.ViewModels;
using Web.Extensions;
using Web.Extensions.Attributes;

namespace Web.Core.SearchLogs
{

	[Authorize]
	[DisplayName("جستجو در تراکنش ها")]
	public class SearchLogsController : Controller
	{
		private readonly ISearchLogDetailRepository _searchLogDetailRepository;
		private readonly ISearchLogTransactionDetailRepository _searchLogTransactionDetailRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ITransactionDetailRepository _transactionDetailRepository;

		public SearchLogsController(ITransactionDetailRepository transactionDetailRepository,
									  ISearchLogDetailRepository searchLogDetailRepository,
									  ISearchLogTransactionDetailRepository searchLogTransactionDetailRepository,
									  IUnitOfWork unitOfWork)
		{
			_searchLogDetailRepository = searchLogDetailRepository;
			_searchLogTransactionDetailRepository = searchLogTransactionDetailRepository;
			_transactionDetailRepository = transactionDetailRepository;
			_unitOfWork = unitOfWork;
		}

		[Permission]
		[DisplayName("جستجو در تراکنش ها")]
		public IActionResult SearchLog()
		{
			return View("SearchLog", new SearchLogDetailViewModel());
		}

		public IActionResult GetListByCardNumber(string models, string cardNumber)
		{
			var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);
			var list = _transactionDetailRepository.GetListByCardNumber(request, cardNumber);
			return Json(list);
		}

		[HttpPost]
		public IActionResult AddSearchLog(SearchLogDetailViewModel model)
		{
			var searchLogDetail = new SearchLogDetail
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				NationalCode = model.NationalCode,
				FatherName = model.FatherName,
				Address = model.Address,
				LetterIdentifier = model.LetterIdentifier,
				CardNumber = model.CardNumberPart4 + model.CardNumberPart3 +
							 model.CardNumberPart2 + model.CardNumberPart1,
				SearchDate = DateTime.Now.Date,
				SearchTime = DateTime.Now.ToLongTimeString(),
				IsSuccess = Convert.ToBoolean(model.IsSuccess),
				IsVictim = model.IsSuccess == 0 ? false : Convert.ToBoolean(model.IsVictim),
				UserId = User.GetUserId()
			};
			_searchLogDetailRepository.Add(searchLogDetail);

			foreach (var transactionDetailId in model.TransactionDetailIdList)
			{
				var searchLogTransactionDetail = new SearchLogTransactionDetail
				{
					SearchLogDetail = searchLogDetail,
					TransactionDetailId = transactionDetailId
				};
				_searchLogTransactionDetailRepository.Add(searchLogTransactionDetail);
			}
			_unitOfWork.SaveAsync();
			return Json(new { Success = true });
		}

		public IActionResult Success()
		{
			return View("_Success");
		}

		[Permission]
		[DisplayName("گزارش فراوانی")]
		public IActionResult AbundanceReport()
		{
			return View();
		}

		[HttpGet]
		public IActionResult GetAbundanceReport(string models)
		{
			var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);
			var list = _searchLogDetailRepository.GetAbundanceReport(request);
			return Json(list);
		}

		[Permission]
		[DisplayName("گزارش سجایا")]
		public IActionResult SajayaReport(SearchLogDetailViewModel model)
		{
			return View(model);
		}

		public ActionResult SearchForSajayaReport()
		{
			return PartialView("_SajayaSearch", new SearchLogDetailViewModel());
		}

		[HttpGet]
		public IActionResult GetSajayaReport(string models, SearchLogDetailViewModel model)
		{
			var list = new DataSourceResult();
			var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);

			list = (model.Search == 1) ?
				_searchLogDetailRepository.GetSajayaReport(request,
					new SearchLogFilter
					{
						FirstName = model.FirstName,
						LastName = model.LastName,
						LetterIdentifier = model.LetterIdentifier,
						IsSuccess = Convert.ToBoolean(model.IsSuccess),
						IsVictim = Convert.ToBoolean(model.IsVictim),
						SearchDate = model.SearchDate
					})
				: list;
			return Json(list);
		}
	}
}