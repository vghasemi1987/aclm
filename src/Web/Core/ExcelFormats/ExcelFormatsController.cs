using ApplicationCommon;
using DomainContracts.Commons;
using DomainContracts.ExcelFormatAggregate;
using DomainEntities.Commons;
using DomainEntities.ExcelFormatAggregate;
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
using System.Threading.Tasks;
using Web.Core.ExcelFormats.ViewModels;
using Web.Extensions.Attributes;

namespace Web.Core.ExcelFormats
{
	[Authorize]
	[DisplayName("فرمت اکسل ها")]
	public class ExcelFormatsController : Controller
	{
		private readonly IExcelFormatHeaderRepository _excelFormatHeaderRepository;
		private readonly IExcelFormatDetailRepository _excelFormatDetailRepository;
		private readonly IUnitOfWork _unitOfWork;
		
		private readonly IWebHostEnvironment _hostingEnvironment;
		public ExcelFormatsController(IExcelFormatHeaderRepository excelFormatHeaderRepository,
									  IExcelFormatDetailRepository excelFormatDetailRepository,
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
			var model = new ExcelFormatHeaderViewModel();
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
		public async Task<IActionResult> AddDetail(ExcelFormatHeaderViewModel model, IFormFile postedFile)
		{
			try
			{
				//SaveFileToServerAsync($"{_hostingEnvironment.WebRootPath}\\uploads\\user-excel\\", postedFile);
				if (model.Separator == "Tab")
				{
					model.Separator = '\t'.ToString();
					//model.Separator[1] = '\t';
				}
				var excelFormatHeader = new ExcelFormatHeader
				{
					Title = model.Title,
					ExcelHeaders = GetExcelHeaderColumns(postedFile, model.FileFormat, model.Separator),
					RecordStatus = Convert.ToBoolean(RecordStatus.NotDeleted),
					FileFormat = model.FileFormat,
					Separator = model.Separator
				};
				_excelFormatHeaderRepository.Add(excelFormatHeader);
				await _unitOfWork.SaveAsync();
				return Json(new
				{
					Message = Message.Show("اطلاعات  با موفقیت ثبت شد...", MessageType.Success),
					RefreshGrid = true
				});
			}
			catch (Exception ex)
			{
				return Json(new
				{
					Message = Message.Show($"{ex.Message}", MessageType.Error),
					RefreshGrid = true
				});
			}

		}

		[HttpPost, ValidateAntiForgeryToken]
		[Permission]
		[DisplayName("ویرایش")]
		public async Task<IActionResult> EditDetail(ExcelFormatViewModel model)
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
			var model = new ExcelFormatViewModel()
			{
				HeaderId = id,
				Title = excelFormatHeader.Title,
				PropList = new SelectList(excelFormatHeader.ExcelHeaders.Split(',').ToList()),
				SourcePan = excelFormatDetails.FirstOrDefault(p => p.Column == "SourcePan")?.MappedColumn,
				TargetPan = excelFormatDetails.FirstOrDefault(p => p.Column == "TargetPan")?.MappedColumn,
				TransactionDate = excelFormatDetails.FirstOrDefault(p => p.Column == "TransactionDate")?.MappedColumn,
				TransactionTime = excelFormatDetails.FirstOrDefault(p => p.Column == "TransactionTime")?.MappedColumn,
				Tel = excelFormatDetails.FirstOrDefault(p => p.Column == "Tel")?.MappedColumn,
				Amount = excelFormatDetails.FirstOrDefault(p => p.Column == "Amount")?.MappedColumn,
				LogDate = excelFormatDetails.FirstOrDefault(p => p.Column == "LogDate")?.MappedColumn,
				IpAddress = excelFormatDetails.FirstOrDefault(p => p.Column == "IpAddress")?.MappedColumn,
				Status = excelFormatDetails.FirstOrDefault(p => p.Column == "Status")?.MappedColumn,
				RefNumber = excelFormatDetails.FirstOrDefault(p => p.Column == "RefNumber")?.MappedColumn,
				UserAgent = excelFormatDetails.FirstOrDefault(p => p.Column == "UserAgent")?.MappedColumn,
				Application = excelFormatDetails.FirstOrDefault(p => p.Column == "Application")?.MappedColumn,
			};
			return PartialView("_MapColumns", model);
		}

		[HttpPost]
		public async Task<IActionResult> MapColumns(ExcelFormatViewModel model)
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
					_excelFormatDetailRepository.Add(new ExcelFormatDetail()
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


		private string GetExcelHeaderColumns(IFormFile file, FileFormat fileKindId, string separator)
		{
			if (file.Length <= 0)
				return null;

			string columns = string.Empty;

			if (fileKindId == FileFormat.Excel)
			{
				using (MemoryStream ms = new MemoryStream())
				{
					file.CopyTo(ms);
					ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
					ExcelPackage package = new ExcelPackage(ms);

					var workSheet = package.Workbook.Worksheets.First();
					var noOfCol = workSheet.Dimension.End.Column;
					for (int i = 1; i <= noOfCol; i++)
					{
						columns += workSheet.Cells[1, i].Value.ToString() + ",";
					}
					columns = columns.Substring(0, columns.Length - 1);
				}
			}
			else if (fileKindId == FileFormat.Csv)
			{



				StreamReader reader = new StreamReader(file.OpenReadStream());
				reader.BaseStream.Position = 0;
				var line = reader.ReadLine();
				//Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
				//string[] result = CSVParser.Split(line);
				string[] result = line.Split(separator);
				columns = string.Join(",", result);
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