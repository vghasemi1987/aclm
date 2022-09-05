using DomainApplication.ExcelMapper;
using DomainContracts.Commons;
using DomainContracts.TopupChannelExcelFormatAggregate;
using DomainContracts.TopupChannelTransactionAggregate;
using DomainEntities.TopupChannelExcelFormatAggregate;
using DomainEntities.TopupChannelTransaction;
using ExcelMapper;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainApplication.Services.TransactionAggregate
{
	public class TopupChannelTransactionHeaderService : ITopupChannelTransactionHeaderService
	{
		private readonly ITopupChannelExcelFormatDetailRepository _excelFormatDetailRepository;
		private readonly ITopupChannelTransactionHeaderRepository _transactionHeaderRepository;
		private readonly ITopupChannelTransactionDetailRepository _transactionDetailRepository;
		private readonly IUnitOfWork _unitOfWork;
		public TopupChannelTransactionHeaderService(ITopupChannelExcelFormatDetailRepository excelFormatDetailRepository,
										ITopupChannelTransactionHeaderRepository transactionHeaderRepository,
										ITopupChannelTransactionDetailRepository transactionDetailRepository,
										IUnitOfWork unitOfWork)
		{
			_excelFormatDetailRepository = excelFormatDetailRepository;
			_transactionHeaderRepository = transactionHeaderRepository;
			_transactionDetailRepository = transactionDetailRepository;
			_unitOfWork = unitOfWork;
		}
		public async Task UploadExcelFile(TopupChannelTransactionHeader transactionHeader, int excelFormatId, IFormFile file, string webPath, TopupChannelFileFormat fileFormat)
		{
			var excelFormatDetail = await _excelFormatDetailRepository.FindByHeaderIdAsync(excelFormatId);
			var transactionDetails = GetExcelFile(file, excelFormatDetail, webPath, fileFormat);
			await Save(transactionHeader, transactionDetails);
		}


		private async Task Save(TopupChannelTransactionHeader transactionHeader, List<TopupChannelTransactionDetail> transactionDetails)
		{
			_transactionHeaderRepository.Add(transactionHeader);
			await _unitOfWork.SaveAsync();
			transactionDetails.ForEach(p => p.HeaderId = transactionHeader.Id);
			_transactionDetailRepository.BulkInsert(transactionDetails);
		}
		private List<TopupChannelTransactionDetail> GetExcelFile(IFormFile file, List<TopupChannelExcelFormatDetail> excelFormatDetail, string webPath, TopupChannelFileFormat fileFormat)
		{
			if (fileFormat == TopupChannelFileFormat.Excel)
			{
				Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
				using (var stream = file.OpenReadStream())
				{
					using (var importer = new ExcelImporter(stream))
					{
						return GetTransactionDetails(importer, excelFormatDetail);
					}
				}
			}
			else if (fileFormat == TopupChannelFileFormat.TXT)
			{
				var excelPath = ConvertTXTToExcel(file, webPath);
				using (var stream = new StreamReader(excelPath))
				{
					using (var importer = new ExcelImporter(stream.BaseStream))
					{
						return GetTransactionDetails(importer, excelFormatDetail);
					}
				}
			}
			else
			{
				var excelPath = ConvertCsvToExcel(file, webPath);
				using (var stream = new StreamReader(excelPath))
				{
					using (var importer = new ExcelImporter(stream.BaseStream))
					{
						return GetTransactionDetails(importer, excelFormatDetail);
					}
				}
			}
		}
		private string ConvertTXTToExcel(IFormFile file, string webPath)
		{
			string excelFileName = $"{webPath}\\uploads\\user-excel\\{Path.GetFileNameWithoutExtension(file.FileName)}.xls";
			string txtFileName = $"{webPath}\\uploads\\user-excel\\{file.FileName}";

			string worksheetsName = "TEST";

			bool firstRowIsHeader = false;
			eDataTypes[] dataTypes = new eDataTypes[4];

			dataTypes[0] = eDataTypes.String;
			dataTypes[1] = eDataTypes.String;
			dataTypes[2] = eDataTypes.String;
			dataTypes[3] = eDataTypes.String;
			var format = new ExcelTextFormat
			{
				Delimiter = ',',
				EOL = "\r",
				DataTypes = dataTypes,

			};
			if (File.Exists(excelFileName)) File.Delete(excelFileName);
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFileName)))
			{
				ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(worksheetsName);
				worksheet.Cells["A1"].LoadFromText(new FileInfo(txtFileName), format, OfficeOpenXml.Table.TableStyles.Medium27, firstRowIsHeader);
				package.Save();
			}
			return excelFileName;
		}
		private List<TopupChannelTransactionDetail> GetTransactionDetails(ExcelImporter importer, List<TopupChannelExcelFormatDetail> excelFormatDetail)
		{
			TopupChannelExcelColumnMap excelColumnMap = new TopupChannelExcelColumnMap(excelFormatDetail);
			importer.Configuration.RegisterClassMap(excelColumnMap);
			ExcelSheet sheet = importer.ReadSheet();
			var transDetailList = sheet.ReadRows<TopupChannelTransactionDetail>().ToList();
			//transDetailList.RemoveAll(q => string.IsNullOrEmpty(q.SourcePan?.Trim()) && string.IsNullOrEmpty(q.TargetPan?.Trim()));

			CorrectDateFormat(transDetailList);
			SetTransctionDate(transDetailList);
			return transDetailList;

		}
		private string ConvertCsvToExcel(IFormFile file, string webPath)
		{
			string excelFileName = $"{webPath}\\uploads\\user-excel\\{Path.GetFileNameWithoutExtension(file.FileName)}.xls";
			string csvFileName = $"{webPath}\\uploads\\user-excel\\{file.FileName}";

			string worksheetsName = "TEST";

			bool firstRowIsHeader = false;
			var format = new ExcelTextFormat
			{
				Delimiter = ',',
				EOL = "\r"
			};
			if (File.Exists(excelFileName)) File.Delete(excelFileName);
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFileName)))
			{
				ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(worksheetsName);
				worksheet.Cells["A1"].LoadFromText(new FileInfo(csvFileName), format, OfficeOpenXml.Table.TableStyles.Medium27, firstRowIsHeader);
				package.Save();
			}
			return excelFileName;
		}
		private void SetTransctionDate(List<TopupChannelTransactionDetail> transactionDetailList)
		{
			/*foreach (var item in transactionDetailList)
            {
                if (item.TransactionDate.StartsWith("13") || item.TransactionDate.StartsWith("14"))
                    item.TransactionDate = item.TransactionDate.Trim().ToMiladiDate();
                else
                    item.TransactionDate = Convert.ToDateTime(item.TransactionDate.Trim());
            }*/
		}
		private void CorrectDateFormat(List<TopupChannelTransactionDetail> transList)
		{
			if (!transList.Any() ||
				 transList.Any(q => !q.TransactionDate.StartsWith("13") && !q.TransactionDate.StartsWith("14")))
				return;

			var firstItem = transList.First().TransactionDate;
			if (firstItem.Contains("_"))
			{
				transList.ForEach(q => q.TransactionDate = q.TransactionDate.Replace('_', '/'));
			}
			else if (firstItem.Contains("-"))
			{
				transList.ForEach(q => q.TransactionDate = q.TransactionDate.Replace('-', '/'));
			}
			else if (!firstItem.Contains("/"))
			{
				transList.ForEach(q => q.TransactionDate = SeprateDate(q.TransactionDate));
			}
		}
		private string SeprateDate(string date) => $"{date.Substring(0, 4)}/{date.Substring(4, 2)}/{date.Substring(6, 2)}";

	}
}
