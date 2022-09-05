using ApplicationCommon;
using DomainApplication.ExcelMapper;
using DomainContracts.Commons;
using DomainContracts.ExcelFormatAggregate;
using DomainContracts.TransactionAggregate;
using DomainEntities.ExcelFormatAggregate;
using DomainEntities.Transaction;
using ExcelMapper;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainApplication.Services.TransactionAggregate
{
	public class TransactionHeaderService : ITransactionHeaderService
	{
		private readonly IExcelFormatDetailRepository _excelFormatDetailRepository;
		private readonly ITransactionHeaderRepository _transactionHeaderRepository;
		private readonly ITransactionDetailRepository _transactionDetailRepository;
		private readonly IUnitOfWork _unitOfWork;
		public TransactionHeaderService(IExcelFormatDetailRepository excelFormatDetailRepository,
										ITransactionHeaderRepository transactionHeaderRepository,
										ITransactionDetailRepository transactionDetailRepository,
										IUnitOfWork unitOfWork)
		{
			_excelFormatDetailRepository = excelFormatDetailRepository;
			_transactionHeaderRepository = transactionHeaderRepository;
			_transactionDetailRepository = transactionDetailRepository;
			_unitOfWork = unitOfWork;
		}
		public async Task<string> UploadExcelFile(TransactionHeader transactionHeader, ExcelFormatHeader excelFormatHeader
			, IFormFile file, string webPath, string path)
		{
			//مشخصات ستون های اکسل
			//بر اساس قالب
			var excelFormatDetail = await _excelFormatDetailRepository.FindByHeaderIdAsync(excelFormatHeader.Id);
			// مشخصات فایل آپلود شده
			List<TransactionDetailDto> transactionDetails = GetExcelFile(file, excelFormatDetail, webPath, excelFormatHeader.FileFormat, excelFormatHeader.Separator, path);
			return await Save(transactionHeader, transactionDetails);
		}

		//private string PersianToEnglishAndToNumber(string persianStr, Dictionary<char, char> lettersDictionary)
		//{
		//    if (string.IsNullOrEmpty(persianStr))
		//    {
		//        return "";
		//    }
		//    try
		//    {
		//        var str = Decimal.Parse(persianStr, NumberStyles.Float).ToString();
		//        return str;
		//    }
		//    catch (Exception)
		//    {
		//        foreach (var item in persianStr)
		//        {
		//            persianStr = persianStr.Replace(item, lettersDictionary[item]);
		//        }
		//        return persianStr;
		//    }
		//}

		Dictionary<char, char> lettersDictionary = new Dictionary<char, char>
		{
			['۰'] = '0',
			['۱'] = '1',
			['۲'] = '2',
			['۳'] = '3',
			['۴'] = '4',
			['۵'] = '5',
			['۶'] = '6',
			['۷'] = '7',
			['۸'] = '8',
			['۹'] = '9',
			['٠'] = '0',
			['١'] = '1',
			['٢'] = '2',
			['٣'] = '3',
			['٤'] = '4',
			['٥'] = '5',
			['٦'] = '6',
			['٧'] = '7',
			['٨'] = '8',
			['٩'] = '9',
			['0'] = '0',
			['1'] = '1',
			['2'] = '2',
			['3'] = '3',
			['4'] = '4',
			['5'] = '5',
			['6'] = '6',
			['7'] = '7',
			['8'] = '8',
			['9'] = '9',
			['x'] = 'x',
			['X'] = 'x',
			['*'] = 'x',
			['-'] = 'x'
		};
		private string PersianToEnglish(string persianStr)
		{
			try
			{
				if (persianStr == null || persianStr == "")
				{
					return "";
				}
				foreach (var item in persianStr)
				{
					persianStr = persianStr.Replace(item, lettersDictionary[item]);
				}
				return CharToX(persianStr);
			}
			catch (Exception e)
			{
				return persianStr;
			}
		}
		private string CharToX(string card)
		{
			try
			{
				if (card == null || card == "" || card.Length < 16)
				{
					return "";
				}
				card = card.Substring(0, 6) + "xxxxxx" + card.Substring(12, 4);
				return card;
			}
			catch (Exception e)
			{
				return "";
			}
		}

		private async Task<string> Save(TransactionHeader transactionHeader, List<TransactionDetailDto> transactionDetailDtos)
		{
			try
			{
				CorrectDateFormat(transactionDetailDtos);

				var details = await GetValue(transactionDetailDtos);

				if (details.Where(o => o.TransactionDate == null).Count() > 0)
				{
					return "در ستون تاریخ، اطلاعات به درستی وارد نشده است";
				}
				else if (details.Where(o => o.TransactionTime?.Length > 8).Count() > 0)
				{
					return "در ستون زمان، اطلاعات به درستی وارد نشده است";
				}
				else if (details.Where(o => o.Tel?.Length > 13).Count() > 0)
				{
					return "در ستون تلفن، اطلاعات به درستی وارد نشده است";
				}
				//else if (details.Where(o => o.Amount == null).Count() > 0)
				//{
				//    return "در ستون مبلغ، اطلاعات به درستی وارد نشده است";
				//}
				else if (details.Where(o => o.LogDate == null).Count() > 0)
				{
					return "در ستون وضعیت، اطلاعات به درستی وارد نشده است";
				}
				else if (details.Where(o => o.Status?.Length > 50).Count() > 0)
				{
					return "در ستون وضعیت، اطلاعات به درستی وارد نشده است";
				}
				else if (details.Where(o => o.RefNumber?.Length > 10).Count() > 0)
				{
					return "در ستون شماره مرجع، اطلاعات به درستی وارد نشده است";
				}
				else if (details.Where(o => o.UserAgent?.Length > 50).Count() > 0)
				{
					return "در ستون کاربر، اطلاعات به درستی وارد نشده است";
				}
				else if (details.Where(o => o.Application?.Length > 50).Count() > 0)
				{
					return "در ستون نرم افزار، اطلاعات به درستی وارد نشده است";
				}
				else
				{
					_transactionHeaderRepository.Add(transactionHeader);
					await _unitOfWork.SaveAsync();
					details.ForEach(o => o.HeaderId = transactionHeader.Id);
					_transactionDetailRepository.BulkInsert(details);
					return "";
				}
			}
			catch (Exception e)
			{
				return e.Message;
			}

		}
		private async Task<List<TransactionDetail>> GetValue(List<TransactionDetailDto> transactionDetailDtos)
		{
			var arrayDaya = new TransactionDetail[transactionDetailDtos.Count];

			for (int i = 0; i < transactionDetailDtos.Count; i++)
			{

				arrayDaya[i] = new TransactionDetail
				{
					SourcePanOrgianl = transactionDetailDtos[i]?.SourcePan,
					TargetPanOrgianl = transactionDetailDtos[i]?.TargetPan,
					IpAddress = transactionDetailDtos[i]?.IpAddress?.Length > 50 ? transactionDetailDtos[i]?.IpAddress.Substring(0, 50) : transactionDetailDtos[i]?.IpAddress,
					SourcePan = PersianToEnglish(transactionDetailDtos[i]?.SourcePan),
					TargetPan = PersianToEnglish(transactionDetailDtos[i]?.TargetPan),
					TransactionDate = SetTransctionDate(transactionDetailDtos[i]?.TransactionDateString),
					//Amount= o.Amount==""|| o.Amount==null?0:Int64.Parse(o.Amount),
					Amount = (transactionDetailDtos[i]?.Amount == "" || transactionDetailDtos[i]?.Amount == null) ? 0 : Convert.ToInt64(double.Parse(transactionDetailDtos[i]?.Amount)),
					//Amount = 0,
					TransactionTime = transactionDetailDtos[i]?.TransactionTime,
					Status = transactionDetailDtos[i]?.Status,
					RefNumber = transactionDetailDtos[i]?.RefNumber,
					UserAgent = transactionDetailDtos[i]?.UserAgent,
					Application = transactionDetailDtos[i]?.Application,
					LogDate = DateTime.Now,
					Tel = transactionDetailDtos[i]?.Tel,
				};


			}
			return await Task.FromResult(arrayDaya.ToList());
		}
		//private async Task InsertBulkAsync(List<TransactionDetail> details)
		//{
		//    await Task.FromResult(_transactionDetailRepository.BulkInsert(details)) ;
		//}
		private List<TransactionDetailDto> GetExcelFile(IFormFile file, List<ExcelFormatDetail> excelFormatDetail, string webPath, FileFormat fileFormat, string separator, string path)
		{
			if (fileFormat == FileFormat.Excel)
			{
				Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
				using (var stream = file.OpenReadStream())
				{
					using (var importer = new ExcelImporter(stream))
					{
						var result = GetTransactionDetails(importer, excelFormatDetail);
						return result;
					}
				}
			}
			else
			{

				//var excelPath = ConvertCsvToExcel(separator, path);
				var excelPath = ConvertCsvToExcel(file, separator, path);
				using (var stream = new StreamReader(excelPath))
				{
					using (var importer = new ExcelImporter(stream.BaseStream))
					{
						return GetTransactionDetails(importer, excelFormatDetail);
					}
				}
			}
		}
		/// <summary>
		/// فرایند خواندن اکسل
		/// </summary>
		/// <param name="importer">تنظیمات</param>
		/// <param name="excelFormatDetail">قالب ورودی اکسل</param>
		/// <returns>لیستی از داده های داخل اکسل</returns>
		private List<TransactionDetailDto> GetTransactionDetails(ExcelImporter importer, List<ExcelFormatDetail> excelFormatDetail)
		{
			ExcelColumnMap excelColumnMap = new ExcelColumnMap(excelFormatDetail);
			importer.Configuration.RegisterClassMap(excelColumnMap);
			ExcelSheet sheet = importer.ReadSheet();
			List<TransactionDetailDto> transDetailList;
			try
			{
				//تا اینجا می خونه !!!
				transDetailList = sheet.ReadRows<TransactionDetailDto>().ToList();
			}
			catch (Exception ex)
			{

				throw;
			}



			// اگر SourcePan خالی بود 
			//لیست  را خالی کن
			//TargetPan
			transDetailList.RemoveAll(q => string.IsNullOrEmpty(q.SourcePan?.Trim()) && string.IsNullOrEmpty(q.TargetPan?.Trim()));

			//CorrectDateFormat(transDetailList);
			//SetTransctionDate(transDetailList);
			return transDetailList;

		}
		//private string ConvertCsvToExcel(string separator, string path)
		//{
		//    var p = path.Split('.');
		//    p[p.Length - 1] = "xls";
		//    string excelFileName = string.Join(".", p);
		//    //string excelFileName = $"{webPath}\\uploads\\user-excel\\{Path.GetFileNameWithoutExtension(file.FileName)}.xls";
		//    string csvFileName = path;

		//    string worksheetsName = "TEST";

		//    bool firstRowIsHeader = false;
		//    var format = new ExcelTextFormat
		//    {
		//        Delimiter = separator[0],
		//        EOL = "\r",
		//        Encoding = Encoding.UTF8,
		//        //DataTypes = new eDataTypes[]  { eDataTypes.Unknown  }
		//    };
		//    if (File.Exists(excelFileName)) File.Delete(excelFileName);
		//    using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFileName)))
		//    {
		//        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(worksheetsName);
		//        worksheet.Cells["A1"].LoadFromText(new FileInfo(csvFileName), format, OfficeOpenXml.Table.TableStyles.Medium27, firstRowIsHeader);
		//        var len = (worksheet.Cells.Value as object[,]).GetLength(0);
		//        worksheet.DeleteRow(len);
		//        package.Save();
		//    }
		//    return excelFileName;
		//}

		private string ConvertCsvToExcel(IFormFile file, string separator, string path)
		{

			try
			{
				ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
				var p = path.Split('.');
				p[p.Length - 1] = "xls";
				string excelFileName = string.Join(".", p);
				//string csvFileName = path;

				string worksheetsName = "TEST";
				using (StreamReader reader = new StreamReader(file.OpenReadStream()))
				{


					using (var package = new ExcelPackage(new FileInfo(excelFileName)))
					{
						ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(worksheetsName);

						//int j = 1;
						//while (!reader.EndOfStream)
						//{

						var allLine = reader.ReadToEnd().Replace("\"", "");
						var line = allLine.Split('\r');
						for (int j = 1; j < line.Length; j++)
						{
							var values = line[j - 1].Split(separator[0]);
							for (int i = 1; i <= values.Length; i++)
							{
								//var c = values[i - 1].Replace("\n", "").Replace("\"","");
								worksheet.Cells[j, i].Value = i == 1 ? values[i - 1].Replace("\n", "") : values[i - 1];
							}
						}
						//j++;
						//}
						package.Save();
						return excelFileName;
					}
				}
			}
			catch (Exception ex)
			{
				string ee = ex.InnerException.Message;
				throw;
			}

		}
		//private void SetTransctionDate(List<TransactionDetail> transactionDetailList)
		//{
		//    foreach (var item in transactionDetailList)
		//    {
		//        if (item.TransactionDateString.StartsWith("13") || item.TransactionDateString.StartsWith("14"))
		//            item.TransactionDate = item.TransactionDateString.Trim().ToMiladiDate();
		//        else
		//            item.TransactionDate = Convert.ToDateTime(item.TransactionDateString.Trim());
		//    }
		//}
		private DateTime SetTransctionDate(string PersianDateTime)
		{
			if (PersianDateTime.Contains("_"))
				PersianDateTime = PersianDateTime.Replace("_", "-");

			if (PersianDateTime.StartsWith("13") || PersianDateTime.StartsWith("14"))
			{

				return PersianDateTime.Trim().ToMiladiDate();
			}

			else
			{
				return Convert.ToDateTime(PersianDateTime.Trim());
			}
		}
		private void CorrectDateFormat(List<TransactionDetailDto> transList)
		{

			// Null Reference Exception
			if (!transList.Any() || transList.Any(q => !q.TransactionDateString.StartsWith("13") && !q.TransactionDateString.StartsWith("14")))
			{
				return;
			}


			var firstItem = transList.First().TransactionDateString;
			if (firstItem.Contains("_"))
			{
				transList.ForEach(q => q.TransactionDateString = q.TransactionDateString.Replace('_', '/'));
			}
			else if (firstItem.Contains("-"))
			{
				transList.ForEach(q => q.TransactionDateString = q.TransactionDateString.Replace('-', '/'));
			}
			else if (!firstItem.Contains("/"))
			{
				transList.ForEach(q => q.TransactionDateString = SeprateDate(q.TransactionDateString));
			}
		}
		private string SeprateDate(string date) => $"{date.Substring(0, 4)}/{date.Substring(4, 2)}/{date.Substring(6, 2)}";
	}
}
