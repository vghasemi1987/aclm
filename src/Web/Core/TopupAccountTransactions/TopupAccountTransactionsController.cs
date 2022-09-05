using ApplicationCommon;
using DomainContracts.Commons;
using DomainContracts.TopupAccountExcelFormatAggregate;
using DomainContracts.TopupAccountTransactionAggregate;
using DomainEntities.Commons;
using DomainEntities.TopupAccountExcelFormatAggregate;
using DomainEntities.TopupAccountTransaction;
using KendoHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Web.Core.TopupAccountTransactions.ViewModels;
using Web.Extensions;
using Web.Extensions.Attributes;

namespace Web.Core.TopupAccountTransactions
{
	[Authorize]
	[DisplayName("Topup تراکنش های حساب")]
	public class TopupAccountTransactionsController : Controller
	{
		private readonly ITopupAccountTransactionHeaderRepository _transactionHeaderRepository;
		private readonly ITopupAccountTransactionDetailRepository _transactionDetailRepository;
		private readonly ITopupAccountTransactionHeaderService _transactionHeaderService;
		private readonly ITopupAccountExcelFormatHeaderRepository _excelFormatHeaderRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _hostingEnvironment;
		public TopupAccountTransactionsController(ITopupAccountTransactionHeaderRepository transactionHeaderRepository,
									  ITopupAccountExcelFormatHeaderRepository excelFormatHeaderRepository,
									  ITopupAccountTransactionHeaderService transactionHeaderService,
									  ITopupAccountTransactionDetailRepository transactionDetailRepository,
									  IWebHostEnvironment hostingEnvironment,
									  IUnitOfWork unitOfWork)
		{
			_transactionHeaderRepository = transactionHeaderRepository;
			_transactionDetailRepository = transactionDetailRepository;
			_excelFormatHeaderRepository = excelFormatHeaderRepository;
			_transactionHeaderService = transactionHeaderService;
			_hostingEnvironment = hostingEnvironment;
			_unitOfWork = unitOfWork;
		}

		[Permission]
		[DisplayName("لیست نمایشی")]
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult GetRecords(string models)
		{
			var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);
			var list = _transactionHeaderRepository.GetList(request);
			return Json(list);
		}

		[HttpGet]
		[Permission]
		[DisplayName("مشاهده جزییات فایل")]
		public async Task<IActionResult> GetDetail(int id)
		{
			var model = new TopupAccountTransactionViewModel
			{
				ExcelFormatHeaderList = new SelectList(await _excelFormatHeaderRepository.GetDropDownList(),
				nameof(DropDownDto.Id), nameof(DropDownDto.Title))
			};
			return PartialView("_Detail", model);
		}

		[HttpPost]
		[Permission]
		[DisplayName("افزودن")]
		public async Task<IActionResult> AddDetail(TopupAccountTransactionViewModel model, IFormFile postedFile)
		{
			Commons.File.SaveFile($"{_hostingEnvironment.WebRootPath}\\uploads\\user-excel\\", postedFile);

			if ((TopupAccountFileFormat)model.FileKindId == TopupAccountFileFormat.Csv)
				Commons.File.SaveCsvFile(_hostingEnvironment.WebRootPath, postedFile);

			var transactionHeader = new TopupAccountTransactionHeader
			{
				Title = model.FileName,
				Description = model.Description,
				UserId = User.GetUserId(),
				UploadDate = model.UploadDate.HasValue() ? model.UploadDate.ToMiladiDate() : DateTime.Now,
				RecordStatus = Convert.ToBoolean(RecordStatus.NotDeleted),
			};

			await _transactionHeaderService.UploadExcelFile(transactionHeader, model.ExcelFormatHeaderId, postedFile,
															_hostingEnvironment.WebRootPath, (TopupAccountFileFormat)model.FileKindId);

			return Json(new
			{
				Message = Message.Show("اطلاعات تراکنش با موفقیت ثبت شد...", MessageType.Success),
				RefreshGrid = true
			});
		}

		[Permission]
		[DisplayName("حذف")]
		public async Task<IActionResult> Delete(List<int> ids)
		{
			if (!ids.Any()) return Json(false);
			foreach (var id in ids)
			{
				var transactionHeader = await _transactionHeaderRepository.FindByIdAsync(id);
				_transactionHeaderRepository.Delete(transactionHeader);
			}

			await _unitOfWork.SaveAsync();
			return Json(new
			{
				Message = Message.Show("حذف با موفقیت انجام شد...", MessageType.Success),
				RefreshGrid = true
			});
		}

		public IActionResult Search()
		{
			return PartialView("_Search", new TopupAccountSearchViewModel());
		}

		[Permission]
		[DisplayName("گزارش تراکنش ها")]
		public IActionResult TransactionDetailReport()
		{
			return View("TransactionDetail", new TopupAccountSearchViewModel());
		}
		public IActionResult GetTransactionDetail(string models, string searchParameters)
		{
			var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);
			var searchModel = JsonConvert.DeserializeObject<TopupAccountSearchViewModel>(searchParameters);

			var list = _transactionDetailRepository
							.GetListByFilter(request, new TopupAccountTransactionFilter
							{
								TransactionAmount = string.IsNullOrEmpty(searchModel.TransactionAmount) ? "0" : searchModel.TransactionAmount,
								CustomerAccountNumber = searchModel.CustomerAccountNumber,
								FollowupCode = searchModel.FollowupCode,
								TransactionStatus = searchModel.TransactionStatus,
								TransactionTime = searchModel.TransactionTime,
								TransactionDate = searchModel.TransactionDate
							});
			//ReverseCardNumber(list);
			return Json(list);
		}

		[Permission]
		[DisplayName("جزییات تراکنش ها")]
		public IActionResult TransactionDetailByHeader(TopupAccountSearchViewModel model)
		{
			return View("TransactionDetailByHeader", model);
		}

		public IActionResult GetTransactionDetailByHeader(string models, TopupAccountSearchViewModel model)
		{
			var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);
			var list = _transactionDetailRepository.GetListByFilter(request,
				new TopupAccountTransactionFilter
				{
					TransactionAmount = model.TransactionAmount ?? "0",
					HeaderId = model.HeaderId,
					TransactionDate = model.TransactionDate,
					CustomerAccountNumber = model.CustomerAccountNumber,
					FollowupCode = model.FollowupCode,
					TransactionStatus = model.TransactionStatus,
					TransactionTime = model.TransactionTime,
				});
			//ReverseCardNumber(list);
			return Json(list);
		}

		[HttpGet]
		public ActionResult ExportToExcel(string searchParameters)
		{
			var file = DataExportToExcel(searchParameters);
			file.Position = 0;
			return new FileStreamResult(file, MediaTypeNames.Application.Octet)
			{
				FileDownloadName = "ExcelExport.xlsx",
			};
		}

		private MemoryStream DataExportToExcel(string model)
		{
			var searchModel = JsonConvert.DeserializeObject<TopupAccountSearchViewModel>(model);
			MemoryStream content = new MemoryStream();
			ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
			using (var package = new ExcelPackage(content))
			{
				var testDetails = _transactionDetailRepository.GetListByFilter(
									new TopupAccountTransactionFilter
									{
										TransactionAmount = string.IsNullOrEmpty(searchModel.TransactionAmount) ? "0" : searchModel.TransactionAmount,
										TransactionDate = searchModel.TransactionDate,
										TransactionTime = searchModel.TransactionTime,
										TransactionStatus = searchModel.TransactionStatus,
										FollowupCode = searchModel.FollowupCode,
										CustomerAccountNumber = searchModel.CustomerAccountNumber,
									});

				var worksheet = package.Workbook.Worksheets.Add("TestCases");
				worksheet.View.RightToLeft = true;

				worksheet.Cells[1, 1].Value = "شماره حساب";
				worksheet.Cells[1, 2].Value = "کد شعبه";
				worksheet.Cells[1, 3].Value = "شماره حساب مشتری";
				worksheet.Cells[1, 4].Value = "Extra1";
				worksheet.Cells[1, 5].Value = "Extra2";
				worksheet.Cells[1, 6].Value = "Extra3";
				worksheet.Cells[1, 7].Value = "Extra4";
				worksheet.Cells[1, 8].Value = "کد پیگیری کانال";
				worksheet.Cells[1, 9].Value = "کد پیگیری 2";
				worksheet.Cells[1, 10].Value = "کد مرجع";
				worksheet.Cells[1, 11].Value = "مبلغ تراکنش";
				worksheet.Cells[1, 12].Value = "مبلغ حرفی";
				worksheet.Cells[1, 13].Value = "تاریخ تراکنش";
				worksheet.Cells[1, 14].Value = "وضعیت تراکنش";
				worksheet.Cells[1, 15].Value = "زمان تراکنش";
				worksheet.Cells[1, 16].Value = "نوع تراکنش واریز یا برداشت";
				var i = 0;
				for (var row = 2; row <= testDetails.Count + 1; row++)
				{
					worksheet.Cells[row, 1].Value = testDetails[i].AccountNumber;
					worksheet.Cells[row, 2].Value = testDetails[i].BranchCode;
					worksheet.Cells[row, 3].Value = testDetails[i].CustomerAccountNumber;
					worksheet.Cells[row, 4].Value = testDetails[i].Extra1;
					worksheet.Cells[row, 5].Value = testDetails[i].Extra2;
					worksheet.Cells[row, 6].Value = testDetails[i].Extra3;
					worksheet.Cells[row, 7].Value = testDetails[i].Extra4;
					worksheet.Cells[row, 8].Value = testDetails[i].FollowupCode;
					worksheet.Cells[row, 9].Value = testDetails[i].FollowupCode2;
					worksheet.Cells[row, 10].Value = testDetails[i].RefrenceCode;
					worksheet.Cells[row, 11].Value = testDetails[i].TransactionAmount;
					worksheet.Cells[row, 12].Value = testDetails[i].TransactionAmountText;
					worksheet.Cells[row, 13].Value = testDetails[i].TransactionDate;
					worksheet.Cells[row, 14].Value = testDetails[i].TransactionStatus;
					worksheet.Cells[row, 15].Value = testDetails[i].TransactionTime;
					worksheet.Cells[row, 16].Value = testDetails[i].TransactionType;
					i++;
				}
				worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
				package.Save();
			}
			return content;
		}
		private void ReverseCardNumber(DataSourceResult list)
		{
			/*foreach (TopupAccountTransactionDetailListDto item in list.Data)
            {
                item.SourcePan = Reverse(item.SourcePan);
                item.TargetPan = Reverse(item.TargetPan);
            }*/
		}
		private string Reverse(string cardNumber)
		{
			var array = cardNumber.Split('*')
				.Where(p => !string.IsNullOrWhiteSpace(p))
				.ToArray();
			return array[1] + "******" + array[0];
		}
	}
}