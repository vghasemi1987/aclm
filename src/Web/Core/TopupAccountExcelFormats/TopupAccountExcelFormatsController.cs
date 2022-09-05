using ApplicationCommon;
using DomainContracts.Commons;
using DomainContracts.TopupAccountExcelFormatAggregate;
using DomainEntities.Commons;
using DomainEntities.TopupAccountExcelFormatAggregate;
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
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Web.Core.TopupAccountExcelFormats.ViewModels;
using Web.Extensions.Attributes;

namespace Web.Core.TopupAccountExcelFormats
{
	[Authorize]
	[DisplayName("Topup فرمت اکسل حساب")]
	public class TopupAccountExcelFormatsController : Controller
	{
		private readonly ITopupAccountExcelFormatHeaderRepository _excelFormatHeaderRepository;
		private readonly ITopupAccountExcelFormatDetailRepository _excelFormatDetailRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _hostingEnvironment;
		public TopupAccountExcelFormatsController(ITopupAccountExcelFormatHeaderRepository excelFormatHeaderRepository,
									  ITopupAccountExcelFormatDetailRepository excelFormatDetailRepository,
									  IUnitOfWork unitOfWork,
									  IWebHostEnvironment hostingEnvironment)
		{
			_excelFormatHeaderRepository = excelFormatHeaderRepository;
			_excelFormatDetailRepository = excelFormatDetailRepository;
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
			var list = _excelFormatHeaderRepository.GetList(request);
			return Json(list);
		}

		[HttpGet]
		[Permission]
		[DisplayName("مشاهده جزییات")]
		public async Task<IActionResult> GetDetail(int id)
		{
			var model = new TopupAccountExcelFormatHeaderViewModel();
			if (id > 0)
			{
				var excelFormatHeader = await _excelFormatHeaderRepository.FindByIdAsync(id);
				var excelFormatDetails = await _excelFormatDetailRepository.FindByHeaderIdAsync(id);
				model.Id = excelFormatHeader.Id;
				model.Title = excelFormatHeader.Title;

				var modelType = model.GetType();
				var props = new List<PropertyInfo>(modelType.GetProperties());

				foreach (var prop in props)
				{
					prop.SetValue(model, excelFormatDetails.
							FirstOrDefault(q => q.Column == prop.Name)?.MappedColumn, null);
				}
			}
			return PartialView("_Detail", model);
		}

		[HttpPost]
		[Permission]
		[DisplayName("افزودن")]
		//[DisableRequestSizeLimit]
		public async Task<IActionResult> AddDetail(TopupAccountExcelFormatHeaderViewModel model, IFormFile postedFile)
		{
			//SaveFileToServerAsync($"{_hostingEnvironment.WebRootPath}\\uploads\\user-excel\\", postedFile);
			var excelFormatHeader = new TopupAccountExcelFormatHeader
			{
				Title = model.Title,
				ExcelHeaders = GetExcelHeaderColumns(postedFile, (TopupAccountFileFormat)model.FileKindId),
				RecordStatus = Convert.ToBoolean(RecordStatus.NotDeleted),
			};
			_excelFormatHeaderRepository.Add(excelFormatHeader);
			await _unitOfWork.SaveAsync();
			return Json(new
			{
				Message = Message.Show("اطلاعات  با موفقیت ثبت شد...", MessageType.Success),
				RefreshGrid = true
			});
		}

		[HttpPost, ValidateAntiForgeryToken]
		[Permission]
		[DisplayName("ویرایش")]
		public async Task<IActionResult> EditDetail(TopupAccountExcelFormatViewModel model)
		{
			var excelFormatHeader = await _excelFormatHeaderRepository.FindByIdAsync(model.Id);
			excelFormatHeader.Title = model.Title;
			_excelFormatHeaderRepository.Update(excelFormatHeader);

			var excelFormatDetail = await _excelFormatDetailRepository.FindByHeaderIdAsync(model.Id);

			var modelType = excelFormatDetail.GetType();
			var props = new List<PropertyInfo>(modelType.GetProperties());

			foreach (var prop in props)
			{
				if (prop.Name != "Id" && prop.Name != "Title")
				{
					prop.SetValue(model, excelFormatDetail.
						FirstOrDefault(q => q.Column == prop.Name)?.MappedColumn, null);
				}
			}

			await _unitOfWork.SaveAsync();
			return Json(new
			{
				Message = Message.Show("اطلاعات با موفقیت ویرایش شد...", MessageType.Success),
				RefreshGrid = true
			});
		}

		[Permission]
		[DisplayName("تبدیل ستون ها")]
		public async Task<IActionResult> MapColumns(int id)
		{
			var excelFormatHeader = await _excelFormatHeaderRepository.FindByIdAsync(id);
			var excelFormatDetails = await _excelFormatDetailRepository.FindByHeaderIdAsync(id);
			var model = new TopupAccountExcelFormatViewModel()
			{
				HeaderId = id,
				Title = excelFormatHeader.Title,
				PropList = new SelectList(excelFormatHeader.ExcelHeaders.Split(',').ToList()),
				TransactionDate = excelFormatDetails.FirstOrDefault(p => p.Column == "TransactionDate")?.MappedColumn,
				TransactionTime = excelFormatDetails.FirstOrDefault(p => p.Column == "TransactionTime")?.MappedColumn,
				//LogDate = excelFormatDetails.FirstOrDefault(p => p.Column == "LogDate")?.MappedColumn,
				//Application = excelFormatDetails.FirstOrDefault(p => p.Column == "Application")?.MappedColumn,

				Extra1 = excelFormatDetails.FirstOrDefault(p => p.Column == "Extra1")?.MappedColumn,
				Extra2 = excelFormatDetails.FirstOrDefault(p => p.Column == "Extra2")?.MappedColumn,
				Extra3 = excelFormatDetails.FirstOrDefault(p => p.Column == "Extra3")?.MappedColumn,
				Extra4 = excelFormatDetails.FirstOrDefault(p => p.Column == "Extra4")?.MappedColumn,
				AccountNumber = excelFormatDetails.FirstOrDefault(p => p.Column == "AccountNumber")?.MappedColumn,
				BranchCode = excelFormatDetails.FirstOrDefault(p => p.Column == "BranchCode")?.MappedColumn,
				CustomerAccountNumber = excelFormatDetails.FirstOrDefault(p => p.Column == "CustomerAccountNumber")?.MappedColumn,
				FollowupCode = excelFormatDetails.FirstOrDefault(p => p.Column == "FollowupCode")?.MappedColumn,
				FollowupCode2 = excelFormatDetails.FirstOrDefault(p => p.Column == "FollowupCode2")?.MappedColumn,
				RefrenceCode = excelFormatDetails.FirstOrDefault(p => p.Column == "RefrenceCode")?.MappedColumn,
				TransactionAmount = excelFormatDetails.FirstOrDefault(p => p.Column == "TransactionAmount")?.MappedColumn,
				TransactionAmountText = excelFormatDetails.FirstOrDefault(p => p.Column == "TransactionAmountText")?.MappedColumn,
				TransactionStatus = excelFormatDetails.FirstOrDefault(p => p.Column == "TransactionStatus")?.MappedColumn,
				TransactionType = excelFormatDetails.FirstOrDefault(p => p.Column == "TransactionType")?.MappedColumn,

			};
			return PartialView("_MapColumns", model);
		}

		[HttpPost]
		public async Task<IActionResult> MapColumns(TopupAccountExcelFormatViewModel model)
		{
			var excelFormatDetails = await _excelFormatDetailRepository.FindByHeaderIdAsync(model.HeaderId);
			_excelFormatDetailRepository.Delete(excelFormatDetails);

			var modelType = model.GetType();
			var props = new List<PropertyInfo>(modelType.GetProperties());
			var notMappedColumns = new List<string> { "Id", "Title", "Files", "HeaderId", "PropList" };

			foreach (var prop in props)
			{
				if (notMappedColumns.Contains(prop.Name))
					continue;

				var mappedColumn = prop.GetValue(model, null)?.ToString();

				if (mappedColumn != "لطفا انتخاب کنید...")
				{
					_excelFormatDetailRepository.Add(new TopupAccountExcelFormatDetail()
					{
						Column = prop.Name,
						MappedColumn = mappedColumn,
						HeaderId = model.HeaderId,
					});
				}
			}
			await _unitOfWork.SaveAsync();
			return Json(new
			{
				Message = Message.Show("اطلاعات  با موفقیت ثبت شد...", MessageType.Success),
				RefreshGrid = true
			});
		}

		[HttpDelete]
		[Permission]
		[DisplayName("حذف")]
		public async Task<IActionResult> Delete(List<int> ids)
		{
			if (!ids.Any()) return Json(false);

			_excelFormatHeaderRepository.Delete(ids);
			await _unitOfWork.SaveAsync();
			return Json(new
			{
				Message = Message.Show("حذف با موفقیت انجام شد...", MessageType.Success),
				RefreshGrid = true
			});
		}


		private string GetExcelHeaderColumns(IFormFile file, TopupAccountFileFormat fileKindId)
		{
			if (file.Length <= 0)
				return null;

			string columns = string.Empty;

			if (fileKindId == TopupAccountFileFormat.Excel)
			{
				var ms = new MemoryStream();
				file.CopyTo(ms);
				ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
				ExcelPackage package = new ExcelPackage(ms);

				var workSheet = package.Workbook.Worksheets.First();
				var noOfCol = workSheet.Dimension.End.Column;
				for (int i = 1; i <= noOfCol; i++)
				{
					columns += i.ToString() + ",";
				}
				columns = columns.Substring(0, columns.Length - 1);
			}
			else if (fileKindId == TopupAccountFileFormat.Csv || fileKindId == TopupAccountFileFormat.TXT)
			{
				using (StreamReader reader = new StreamReader(file.OpenReadStream()))
				{
					var line = reader.ReadLine();
					Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
					string[] result = CSVParser.Split(line);
					//columns = string.Join(",", result);
					for (int i = 1; i <= result.Length; i++)
					{
						columns += i.ToString() + ",";
					}
					columns = columns.Substring(0, columns.Length - 1);
				}
			}
			return columns;
		}
		//private async void SaveFileToServerAsync(string path, IFormFile postedFile)
		//{
		//    var fileName = postedFile.FileName.Split('.');
		//    path += fileName.First() + Guid.NewGuid() + "." + fileName.Last();

		//    if (postedFile.Length > 0)
		//    {
		//        using var stream = new FileStream(path, FileMode.Create);
		//        await postedFile.CopyToAsync(stream);
		//    }
		//}
	}
}