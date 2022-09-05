using ApplicationCommon;
using DomainContracts.Commons;
using DomainContracts.TopupChannelExcelFormatAggregate;
using DomainContracts.TopupChannelTransactionAggregate;
using DomainEntities.Commons;
using DomainEntities.TopupChannelExcelFormatAggregate;
using DomainEntities.TopupChannelTransaction;
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
using Web.Core.TopupChannelTransactions.ViewModels;
using Web.Extensions;
using Web.Extensions.Attributes;

namespace Web.Core.TopupChannelTransactions
{
	[Authorize]
	[DisplayName("Topup تراکنش های کانال")]
	public class TopupChannelTransactionsController : Controller
	{
		private readonly ITopupChannelTransactionHeaderRepository _transactionHeaderRepository;
		private readonly ITopupChannelTransactionDetailRepository _transactionDetailRepository;
		private readonly ITopupChannelTransactionHeaderService _transactionHeaderService;
		private readonly ITopupChannelExcelFormatHeaderRepository _excelFormatHeaderRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _hostingEnvironment;
		public TopupChannelTransactionsController(ITopupChannelTransactionHeaderRepository transactionHeaderRepository,
									  ITopupChannelExcelFormatHeaderRepository excelFormatHeaderRepository,
									  ITopupChannelTransactionHeaderService transactionHeaderService,
									  ITopupChannelTransactionDetailRepository transactionDetailRepository,
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
			var model = new TopupChannelTransactionViewModel
			{
				ExcelFormatHeaderList = new SelectList(await _excelFormatHeaderRepository.GetDropDownList(),
				nameof(DropDownDto.Id), nameof(DropDownDto.Title))
			};
			return PartialView("_Detail", model);
		}

		[HttpPost]
		[Permission]
		[DisplayName("افزودن")]
		public async Task<IActionResult> AddDetail(TopupChannelTransactionViewModel model, IFormFile postedFile)
		{
			Commons.File.SaveFile($"{_hostingEnvironment.WebRootPath}\\uploads\\user-excel\\", postedFile);

			if ((TopupChannelFileFormat)model.FileKindId == TopupChannelFileFormat.Csv)
				Commons.File.SaveCsvFile(_hostingEnvironment.WebRootPath, postedFile);

			var transactionHeader = new TopupChannelTransactionHeader
			{
				Title = model.FileName,
				Description = model.Description,
				UserId = User.GetUserId(),
				UploadDate = model.UploadDate.HasValue() ? model.UploadDate.ToMiladiDate() : DateTime.Now,
				RecordStatus = Convert.ToBoolean(RecordStatus.NotDeleted),
			};

			await _transactionHeaderService.UploadExcelFile(transactionHeader, model.ExcelFormatHeaderId, postedFile,
															_hostingEnvironment.WebRootPath, (TopupChannelFileFormat)model.FileKindId);

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
			return PartialView("_Search", new TopupChannelSearchViewModel());
		}

		[Permission]
		[DisplayName("گزارش تراکنش ها")]
		public IActionResult TransactionDetailReport()
		{
			return View("TransactionDetail", new TopupChannelSearchViewModel());
		}
		public IActionResult GetTransactionDetail(string models, string searchParameters)
		{
			var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);
			var searchModel = JsonConvert.DeserializeObject<TopupChannelSearchViewModel>(searchParameters);

			var list = _transactionDetailRepository
							.GetListByFilter(request, new TopupChannelTransactionFilter
							{
								TransactionAmount = string.IsNullOrEmpty(searchModel.TransactionAmount) ? "0" : searchModel.TransactionAmount,
								TransactionDate = searchModel.TransactionDate,
								TransactionStatus = searchModel.TransactionStatus,
								FollowupCode = searchModel.FollowupCode,
								CustomerAccountNumber = searchModel.CustomerAccountNumber,
								OperatorName = searchModel.OperatorName,
							});
			//ReverseCardNumber(list);
			return Json(list);
		}

		[Permission]
		[DisplayName("جزییات تراکنش ها")]
		public IActionResult TransactionDetailByHeader(TopupChannelSearchViewModel model)
		{
			return View("TransactionDetailByHeader", model);
		}

		public IActionResult GetTransactionDetailByHeader(string models, TopupChannelSearchViewModel model)
		{
			var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);
			var list = _transactionDetailRepository.GetListByFilter(request,
				new TopupChannelTransactionFilter
				{
					TransactionAmount = model.TransactionAmount ?? "0",
					HeaderId = model.HeaderId,
					TransactionDate = model.TransactionDate,
					OperatorName = model.OperatorName,
					CustomerAccountNumber = model.CustomerAccountNumber,
					FollowupCode = model.FollowupCode,
					TransactionStatus = model.TransactionStatus,
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
			var searchModel = JsonConvert.DeserializeObject<TopupChannelSearchViewModel>(model);
			MemoryStream content = new MemoryStream();
			ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
			using (var package = new ExcelPackage(content))
			{
				var testDetails = _transactionDetailRepository.GetListByFilter(
									new TopupChannelTransactionFilter
									{
										TransactionAmount = string.IsNullOrEmpty(searchModel.TransactionAmount) ? "0" : searchModel.TransactionAmount,
										TransactionDate = searchModel.TransactionDate,
										TransactionStatus = searchModel.TransactionStatus,
										FollowupCode = searchModel.FollowupCode,
										CustomerAccountNumber = searchModel.CustomerAccountNumber,
										OperatorName = searchModel.OperatorName,
									});

				var worksheet = package.Workbook.Worksheets.Add("TestCases");
				worksheet.View.RightToLeft = true;

				worksheet.Cells[1, 1].Value = "مبلغ حرفی";
				worksheet.Cells[1, 2].Value = "نوع کانال";
				worksheet.Cells[1, 3].Value = "شماره حساب مشتری";
				worksheet.Cells[1, 4].Value = "Extra1";
				worksheet.Cells[1, 5].Value = "Extra2";
				worksheet.Cells[1, 6].Value = "کد پیگیری";
				worksheet.Cells[1, 7].Value = "کد پیگیری 2";
				worksheet.Cells[1, 8].Value = "شماره موبایل";
				worksheet.Cells[1, 9].Value = "نام اپراتور";
				worksheet.Cells[1, 10].Value = "مبلغ تراکنش";
				worksheet.Cells[1, 11].Value = "تاریخ تراکنش";
				worksheet.Cells[1, 12].Value = "وضعیت تراکنش";

				var i = 0;
				for (var row = 2; row <= testDetails.Count + 1; row++)
				{
					worksheet.Cells[row, 1].Value = testDetails[i].AmountText;
					worksheet.Cells[row, 2].Value = testDetails[i].ChannelType;
					worksheet.Cells[row, 3].Value = testDetails[i].CustomerAccountNumber;
					worksheet.Cells[row, 4].Value = testDetails[i].Extra1;
					worksheet.Cells[row, 5].Value = testDetails[i].Extra2;
					worksheet.Cells[row, 6].Value = testDetails[i].FollowupCode;
					worksheet.Cells[row, 7].Value = testDetails[i].FollowupCode2;
					worksheet.Cells[row, 8].Value = testDetails[i].MobileNumber;
					worksheet.Cells[row, 9].Value = testDetails[i].OperatorName;
					worksheet.Cells[row, 10].Value = testDetails[i].TransactionAmount;
					worksheet.Cells[row, 11].Value = testDetails[i].TransactionDate;
					worksheet.Cells[row, 12].Value = testDetails[i].TransactionStatus;
					i++;
				}
				worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
				package.Save();
			}
			return content;
		}
		private void ReverseCardNumber(DataSourceResult list)
		{
			/*foreach (TopupChannelTransactionDetailListDto item in list.Data)
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