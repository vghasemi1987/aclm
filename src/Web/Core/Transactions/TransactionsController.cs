using ApplicationCommon;
using DomainContracts.Commons;
using DomainContracts.ExcelFormatAggregate;
using DomainContracts.TransactionAggregate;
using DomainEntities.Commons;
using DomainEntities.Transaction;
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
using Web.Core.Transactions.ViewModels;
using Web.Extensions;
using Web.Extensions.Attributes;

namespace Web.Core.Transactions
{
	[Authorize]
	[DisplayName("تراکنش ها")]
	public class TransactionsController : Controller
	{
		private readonly ITransactionHeaderRepository _transactionHeaderRepository;
		private readonly ITransactionDetailRepository _transactionDetailRepository;
		private readonly ITransactionHeaderService _transactionHeaderService;
		private readonly IExcelFormatHeaderRepository _excelFormatHeaderRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _hostingEnvironment;
		public TransactionsController(ITransactionHeaderRepository transactionHeaderRepository,
									  IExcelFormatHeaderRepository excelFormatHeaderRepository,
									  ITransactionHeaderService transactionHeaderService,
									  ITransactionDetailRepository transactionDetailRepository,
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
			var model = new TransactionViewModel
			{
				ExcelFormatHeaderList = new SelectList(await _excelFormatHeaderRepository.GetDropDownList(),
				nameof(DropDownDto.Id), nameof(DropDownDto.Title))
			};
			return PartialView("_Detail", model);
		}
		//[RequestFormLimits(MultipartBodyLengthLimit = 1073741824)]
		//[RequestSizeLimit(1073741824)]
		[DisableRequestSizeLimit]
		[HttpPost]
		//2-UploadLargFile

		//[Permission]
		[DisplayName("افزودن")]
		public async Task<IActionResult> AddDetail(TransactionViewModel model, IFormFile postedFile)
		{
			//Thread thread = new Thread(()=> AddDetail02(model,postedFile));
			//thread.Start();

			var excelFormatHeader = _excelFormatHeaderRepository.GetSingleBySpec(o => o.Id == model.ExcelFormatHeaderId);

			string path = Commons.File.SaveFile($"{_hostingEnvironment.WebRootPath}\\uploads\\user-excel\\", postedFile);
			if (!System.IO.File.Exists(path))
			{
				return Json(new
				{
					Message = Message.Show("خطا در ذخیره فایل", MessageType.Warning),
					RefreshGrid = true
				});
			}
			//if (excelFormatHeader.FileFormat == FileFormat.Csv)
			//    Commons.File.SaveCsvFile(_hostingEnvironment.WebRootPath, postedFile);

			var transactionHeader = new TransactionHeader
			{
				Title = model.FileName,
				Description = model.Description,
				UserId = User.GetUserId(),
				UploadDate = model.UploadDate.HasValue() ? model.UploadDate.ToMiladiDate() : DateTime.Now,
				RecordStatus = Convert.ToBoolean(RecordStatus.NotDeleted),
			};
			var r = await _transactionHeaderService.UploadExcelFile(transactionHeader, excelFormatHeader, postedFile,
															_hostingEnvironment.WebRootPath, path);
			if (r == "")
			{
				return Json(new
				{
					Message = Message.Show("اطلاعات تراکنش با موفقیت ثبت شد...", MessageType.Success),
					RefreshGrid = true
				});
			}
			else
			{
				return Json(new
				{
					Message = Message.Show(r, MessageType.Warning),
					RefreshGrid = true
				});
			}
		}


		// [HttpPost]
		[Permission]
		[DisplayName("حذف")]
		public async Task<IActionResult> Delete(List<int> ids)
		{
			if (!ids.Any()) return Json(false);

			try
			{
				foreach (var id in ids)
				{


					var deletedChild = _transactionDetailRepository.ListAllByConditionAsync(w => w.HeaderId == id);


					foreach (var item in deletedChild)
					{

						_transactionDetailRepository.Delete(item);
						//_unitOfWork.Save();
					}


					var transactionHeader = await _transactionHeaderRepository.FindByIdAsync(id);


					_transactionHeaderRepository.Delete(transactionHeader);
					_unitOfWork.Save();
				}
				return Json(new
				{
					Message = Message.Show("حذف با موفقیت انجام شد...", MessageType.Success),
					RefreshGrid = true
				});
			}
			catch (Exception)
			{

				return Json(new
				{
					Message = Message.Show($"به دلیل وجود رکورد فایل حذف نخواهد شد", MessageType.Error),
					RefreshGrid = true
				});
			}
		}
		[HttpPost]
		public async Task<IActionResult> Delete02(int id)
		{
			//var deletedChild = _transactionDetailRepository.ListAllByConditionAsync(w => w.HeaderId == id);
			//foreach (var item in deletedChild)
			//{

			//_transactionDetailRepository.Delete(item);
			//_unitOfWork.Save();
			//}
			var transactionHeader = await _transactionHeaderRepository.FindByIdAsync(id);


			_transactionHeaderRepository.Delete(transactionHeader);
			_unitOfWork.Save();
			return RedirectToAction("Index");
		}
		public IActionResult Search()
		{
			return PartialView("_Search", new SearchViewModel());
		}

		[Permission]
		[DisplayName("گزارش تراکنش ها")]
		public IActionResult TransactionDetailReport()
		{
			return View("TransactionDetail", new SearchViewModel());
		}
		public IActionResult GetTransactionDetail(string models, string searchParameters)
		{
			var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);
			var searchModel = JsonConvert.DeserializeObject<SearchViewModel>(searchParameters);

			var list = _transactionDetailRepository
							.GetListByFilter(request, new TransactionFilter
							{
								Amount = searchModel.Amount == null ? 0 : searchModel.Amount.Value,
								SourcePan = searchModel.SourcePan,
								TargetPan = searchModel.TargetPan,
								BeginTransactionDate = searchModel.BeginTransactionDate,
								EndTransactionDate = searchModel.EndTransactionDate,
								//TransactionDate = searchModel.TransactionDate
							});
			ReverseCardNumber(list);
			return Json(list);
		}

		[Permission]
		[DisplayName("جزییات تراکنش ها")]
		public IActionResult TransactionDetailByHeader(SearchViewModel model)
		{
			return View("TransactionDetailByHeader", model);
		}

		public IActionResult GetTransactionDetailByHeader(string models, SearchViewModel model)
		{
			var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);
			var list = _transactionDetailRepository.GetListByFilter(request,
				new TransactionFilter
				{
					Amount = model.Amount ?? 0,
					HeaderId = model.HeaderId,
					SourcePan = model.SourcePan,
					TargetPan = model.TargetPan,
					BeginTransactionDate = model.BeginTransactionDate,
					EndTransactionDate = model.EndTransactionDate,
					//TransactionDate = model.TransactionDate
				});
			ReverseCardNumber(list);
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
			var searchModel = JsonConvert.DeserializeObject<SearchViewModel>(model);
			MemoryStream content = new MemoryStream();
			ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
			using (var package = new ExcelPackage(content))
			{
				var testDetails = _transactionDetailRepository.GetListByFilter(
									new TransactionFilter
									{
										Amount = searchModel.Amount == null ? 0 : searchModel.Amount.Value,
										SourcePan = searchModel.SourcePan,
										TargetPan = searchModel.TargetPan,
										BeginTransactionDate = searchModel.BeginTransactionDate,
										EndTransactionDate = searchModel.EndTransactionDate,
										//TransactionDate = searchModel.TransactionDate
									});

				var worksheet = package.Workbook.Worksheets.Add("TestCases");
				worksheet.View.RightToLeft = true;

				worksheet.Cells[1, 1].Value = "شماره کارت مبدا";
				worksheet.Cells[1, 2].Value = "شماره کارت مقصد";
				worksheet.Cells[1, 3].Value = "تاریخ تراکنش";
				worksheet.Cells[1, 4].Value = "زمان تراکنش";
				worksheet.Cells[1, 5].Value = "مبلغ";
				worksheet.Cells[1, 6].Value = " آدرس Ip";
				worksheet.Cells[1, 7].Value = "شماره مرجع";
				worksheet.Cells[1, 8].Value = "تلفن";
				worksheet.Cells[1, 9].Value = "UserAgent";
				worksheet.Cells[1, 10].Value = "وضعیت";

				var i = 0;
				for (var row = 2; row <= testDetails.Count + 1; row++)
				{
					worksheet.Cells[row, 1].Value = testDetails[i].SourcePan;
					worksheet.Cells[row, 2].Value = testDetails[i].TargetPan;
					worksheet.Cells[row, 3].Value = testDetails[i].TransactionDate;
					worksheet.Cells[row, 4].Value = testDetails[i].TransactionTime;
					worksheet.Cells[row, 5].Value = testDetails[i].Amount;
					worksheet.Cells[row, 6].Value = testDetails[i].IpAddress;
					worksheet.Cells[row, 7].Value = testDetails[i].RefNumber;
					worksheet.Cells[row, 8].Value = testDetails[i].Tel;
					worksheet.Cells[row, 9].Value = testDetails[i].UserAgent;
					worksheet.Cells[row, 10].Value = testDetails[i].Status;
					i++;
				}
				worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
				package.Save();
			}
			return content;
		}
		private void ReverseCardNumber(DataSourceResult list)
		{
			foreach (TransactionDetailListDto item in list.Data)
			{
				item.SourcePan = Reverse(item.SourcePan);
				item.TargetPan = Reverse(item.TargetPan);
			}
		}
		private string Reverse(string cardNumber)
		{
			try
			{
				var array = cardNumber
							.Split(new char[] { '.', '*', 'x', 'X' })
							.Where(p => !string.IsNullOrWhiteSpace(p))
							.ToArray();
				return array[1] + "******" + array[0];
			}
			catch (Exception)
			{

				return cardNumber;
			}
		}
	}
}