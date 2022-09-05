using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.Core.InquiryEmployee.ViewModels;
using Web.Core.RegisterEmployee.ViewModels;
using Web.Extensions.Attributes;

namespace Web.Core.InquiryEmployee
{
	[Authorize]
	[DisplayName("استعلام کارمندان کرونا")]
	public class InquiryEmployeeController : Controller
	{
		//private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IConfiguration _configuration;
		public InquiryEmployeeController(/*IWebHostEnvironment hostingEnvironment, */IConfiguration configuration,IWebHostEnvironment webHostEnvironment)
		{
			//_hostingEnvironment = hostingEnvironment;
			_configuration = configuration;
			_webHostEnvironment = webHostEnvironment;
		}
		[Permission]
		[DisplayName("استعلام کارمندان")]
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
		public async Task<IActionResult> ResponseServicrSrc()
		{
			try
			{
				//var result = await Task.FromResult(MockDataAsync().Result.result);

				//یادت نره کامنت برداری !!!!!!!!!!
				var result = await Task.FromResult(GetLastStatusOfEmployees().result);

				await SaveInqueryResult(result);
				await SaveInqueryPositive(result.Where(w => w.isContagious == true).ToList());
				return RedirectToAction("ShowAllInquery");
			}
			catch (Exception ex)
			{
				ViewBag.Message = ex.Message;
				return View();
			}
		}
		private SaveEmployeeListDtoRequestDto ReadUserAndPass()
		{
			int counter = 0;
			string line = "";
			string[] values = new string[4];
			SaveEmployeeListDtoRequestDto model = new SaveEmployeeListDtoRequestDto();
			string path = /*_hostingEnvironment*/_webHostEnvironment.WebRootPath + "\\uploads\\refahWriterRegister.txt";
			if (System.IO.File.Exists(path))
			{
				StreamReader file = new StreamReader(path);
				while ((line = file.ReadLine()) != null)
				{
					values[counter] = line;
					counter++;
				}
				model.usernameHeader = values[0];
				model.passwordHeader = values[1];
				model.usernameBody = values[2];
				model.passwordBody = values[3];
				file.Close();
			}
			return model;
		}
		public ResultDto GetLastStatusOfEmployees()
		{
			var readUspass = ReadUserAndPass();
			var client = new RestClient("https://gsbservices.iran.gov.ir/behdasht/GetLastStatusOfEmployees");
			client.Timeout = -1;
			var request = new RestRequest(Method.POST);
			client.Authenticator = new HttpBasicAuthenticator(readUspass.usernameHeader, readUspass.passwordHeader);
			//request.AddHeader("Authorization", "Basic cmVmYWhfZ3NiOkgzQGZONjkjZA==");
			request.AddHeader("Content-Type", "application/json");
			request.AddJsonBody(new { username = readUspass.usernameBody, password = readUspass.passwordBody });
			//request.AddParameter($"application/json", "{\"username\":\"{}\",\n\"password\":\"bTSZBsTyob\"\n}", ParameterType.RequestBody);
			IRestResponse response = client.Execute(request);
			return JsonConvert.DeserializeObject<ResultDto>(response.Content);
		}
		private async Task<ResultDto> MockDataAsync()
		{
			List<Result> results = new List<Result>();
			var query = "SELECT FirstName,LastName,Code,NationalCode,ProvinceName,OrganizationName,UnitName FROM [dbo].[Personnel]";
			using (var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection")))
			{
				connection.Open();
				var personnel = await connection.QueryAsync<Personnel>(query);


				foreach (var item in personnel)
				{
					results.Add(new Result
					{
						nationalCode = item.NationalCode,
						lastStatusCheck = DateTime.Now.AddHours(2 * (-1)).ToString(),
						isContagious = new Random().Next(2, 8) % 2 == 0 ? true : false,
						organization = "Test",
						quarantineEndDate = ""
					});
				}
			}
			var data = new ResultDto
			{
				isError = false,
				message = "successfule",
				responseException = "No Exeption",
				statusCode = 200,
				version = "1.0",
				result = results
			};
			return await Task.FromResult(data);
		}
		private async Task SaveInqueryResult(List<Result> results)
		{
			if (results == null)
				return;

			using (var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection")))
			{
				await connection.OpenAsync();
				var sqlStatement = @"DELETE [dbo].[SaveInqueryEmployee]";
				await connection.ExecuteAsync(sqlStatement);

			}
			using (var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection")))
			{
				await connection.OpenAsync();

				foreach (var item in results)
				{
					var sqlStatement = @"INSERT INTO SaveInqueryEmployee VALUES (@organization,@nationalCode,@lastStatusCheck,@lastStatusCheckFa,@isContagious,@quarantineEndDate,@quarantineEndDateFa,GETDATE())";

					await connection.ExecuteAsync(sqlStatement, item);
				}
			}
		}
		[Permission]
		[DisplayName("آرشیو استعلام")]
		public async Task<IActionResult> ShowAllInquery()
		{
			var query = @"SELECT FirstName,LastName,Code,Sav.nationalCode,ProvinceName,OrganizationName,UnitName,
                                                organization,
                                                lastStatusCheck,
                                                lastStatusCheckFa,
                                                isContagious,
                                                quarantineEndDate,
                                                quarantineEndDateFa
                                                FROM [dbo].[SaveInqueryEmployee] Sav
                                                LEFT JOIN [dbo].[Personnel] Per ON  Per.NationalCode = Sav.nationalCode";

			using (var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection")))
			{
				connection.Open();
				var personnel = await connection.QueryAsync<PersonnelResult>(query);
				return View(personnel);
			}
		}
		[Permission]
		[DisplayName("آرشیو (جستجو)")]
		public async Task<IActionResult> SearchAllInquery(string search = "")
		{
			if (string.IsNullOrEmpty(search))
				return View();

			var query = @"SELECT FirstName,LastName,Code,Sav.nationalCode,ProvinceName,OrganizationName,UnitName,
                            organization,
                            lastStatusCheck,
                            lastStatusCheckFa,
                            isContagious,
                            quarantineEndDate,
                            quarantineEndDateFa
                            FROM [dbo].[SaveInqueryEmployee] Sav
                            LEFT JOIN [dbo].[Personnel] Per ON  Per.NationalCode = Sav.nationalCode
                            WHERE Sav.nationalCode = @ids OR Per.Code = @ids";

			using (var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection")))
			{
				connection.Open();
				var personnel = await connection.QueryAsync<PersonnelResult>(query, new { ids = search });
				return Json(personnel);
			}
		}
		public async Task SaveInqueryPositive(List<Result> inputPositive)
		{
			var listPositive = new List<Result>();


			using (var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection")))
			{
				var readQuery = @"SELECT Id,nationalCode,[quarantineEndDate]
                                  FROM [dbo].[SaveInqueryPositive]";
				var lastPositive = (await connection.QueryAsync<PersonnelResult>(readQuery)).ToList();

				listPositive = (from input in inputPositive
								where !lastPositive.Any(x => x.NationalCode.Equals(input.nationalCode) && x.quarantineEndDateFa.Equals(input.quarantineEndDateFa))
								select input).ToList();

			}

			using (var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection")))
			{
				await connection.OpenAsync();

				foreach (var item in listPositive)
				{
					var sqlStatement = @"INSERT INTO [dbo].[SaveInqueryPositive] VALUES (@organization,@nationalCode,@lastStatusCheck,@lastStatusCheckFa,@isContagious,@quarantineEndDate,@quarantineEndDateFa,GETDATE())";

					await connection.ExecuteAsync(sqlStatement, item);
				}
			}
		}
		[Permission]
		[DisplayName("آرشیو نتایج مثبت")]
		public async Task<IActionResult> ShowPositiveInquery()
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection")))
			{
				var query = @"SELECT FirstName,LastName,Code,Sav.nationalCode,ProvinceName,OrganizationName,UnitName,
                                                organization,
                                                lastStatusCheck,
                                                lastStatusCheckFa,
                                                isContagious,
                                                quarantineEndDate,
                                                quarantineEndDateFa
                                                FROM [dbo].[SaveInqueryPositive] Sav
                                                LEFT JOIN [dbo].[Personnel] Per ON  Per.NationalCode = Sav.nationalCode";
				connection.Open();
				var personnel = await connection.QueryAsync<PersonnelResult>(query);
				return View(personnel);
			}
		}

		public async Task<IActionResult> ExportExcel()
		{
			var query = @"SELECT FirstName,LastName,Code,Sav.nationalCode,ProvinceName,OrganizationName,UnitName,
                          organization,
                          lastStatusCheck,
                          lastStatusCheckFa,
                          isContagious,
                          quarantineEndDate,
                          quarantineEndDateFa
                          FROM [dbo].[SaveInqueryEmployee] Sav
                          LEFT JOIN [dbo].[Personnel] Per ON  Per.NationalCode = Sav.nationalCode";
			var data = GenerateExcelReport(query).Result;
			data.Position = 0;
			return await Task.FromResult(new FileStreamResult(data, System.Net.Mime.MediaTypeNames.Application.Octet)
			{
				FileDownloadName = $"Export_{Guid.NewGuid().ToString().Substring(0, 5)}.xlsx"
			});
		}
		public async Task<IActionResult> ExportExcelPositive()
		{
			var query = @"SELECT FirstName,LastName,Code,Sav.nationalCode,ProvinceName,OrganizationName,UnitName,
                                                organization,
                                                lastStatusCheck,
                                                lastStatusCheckFa,
                                                isContagious,
                                                quarantineEndDate,
                                                quarantineEndDateFa
                                                FROM [dbo].[SaveInqueryPositive] Sav
                                                LEFT JOIN [dbo].[Personnel] Per ON  Per.NationalCode = Sav.nationalCode";
			var data = GenerateExcelReport(query).Result;
			data.Position = 0;
			return await Task.FromResult(new FileStreamResult(data, System.Net.Mime.MediaTypeNames.Application.Octet)
			{
				FileDownloadName = $"Export_{Guid.NewGuid().ToString().Substring(0, 5)}.xlsx"
			});
		}
		public async Task<MemoryStream> GenerateExcelReport(string query)
		{
			var transactionToExelAll = new List<PersonnelResult>();
			using (var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection")))
			{
				connection.Open();
				transactionToExelAll = (await connection.QueryAsync<PersonnelResult>(query)).ToList();
			}

			MemoryStream content = new MemoryStream();
			ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
			using (ExcelPackage package = new ExcelPackage(content))
			{

				var worksheet = package.Workbook.Worksheets.Add("DataSheet");

				worksheet.View.RightToLeft = true;
				worksheet.Cells["A:Z"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

				worksheet.Cells["A1:O1"].Merge = true;
				worksheet.Cells["A1:O1"].Value = "داده های استعلام مربوط به کویید 19";
				System.Drawing.Color colFromHex01 = System.Drawing.ColorTranslator.FromHtml("#B7DEE8");
				worksheet.Cells["A1:O1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
				worksheet.Cells["A1:O1"].Style.Fill.BackgroundColor.SetColor(colFromHex01);


				//worksheet.Cells["I1:U1"].Merge = true;
				//worksheet.Cells["I1:U1"].Value = "اطلاعات شعبه عامل و ارسال فایل";
				//System.Drawing.Color colFromHex02 = System.Drawing.ColorTranslator.FromHtml("#82BDD9");
				//worksheet.Cells["I1:U1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
				//worksheet.Cells["I1:U1"].Style.Fill.BackgroundColor.SetColor(colFromHex02);

				//worksheet.Cells["V1:W1"].Merge = true;
				//worksheet.Cells["V1:W1"].Value = "اقدام انجام شده";
				//System.Drawing.Color colFromHex03 = System.Drawing.ColorTranslator.FromHtml("#5CC454");
				//worksheet.Cells["V1:W1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
				//worksheet.Cells["V1:W1"].Style.Fill.BackgroundColor.SetColor(colFromHex03);

				//System.Drawing.Color colFromHex04 = System.Drawing.ColorTranslator.FromHtml("#B9B9ED");
				//worksheet.Cells["A2:W2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
				//worksheet.Cells["A2:W2"].Style.Fill.BackgroundColor.SetColor(colFromHex04);

				worksheet.Cells["A1:W1"].Style.Font.Name = "Tahoma";
				//worksheet.Cells["A1:W1"].Style.Font.Bold = true;
				//worksheet.Cells["A2:W2"].Style.Font.Name = "Tahoma";
				//worksheet.View.FreezePanes(3, 1);

				worksheet.Cells[2, 1].Value = "ردیف";
				worksheet.Cells[2, 2].Value = "نام";
				worksheet.Cells[2, 3].Value = "نام خانوادگی";
				worksheet.Cells[2, 4].Value = "کد پرسنلی";
				worksheet.Cells[2, 5].Value = "کدملی";
				worksheet.Cells[2, 6].Value = "محل خدمت";
				worksheet.Cells[2, 7].Value = "امور شعب";
				worksheet.Cells[2, 8].Value = "شعبه یا واحد";

				worksheet.Cells[2, 9].Value = "سازمان";
				worksheet.Cells[2, 10].Value = "تاریخ استعلام";

				worksheet.Cells[2, 11].Value = "تاریخ استعلام فارسی";
				worksheet.Cells[2, 12].Value = "وضعیت ابتلا";
				worksheet.Cells[2, 13].Value = "پایان قرنطینه میلادی";
				worksheet.Cells[2, 14].Value = "پایان قرنطینه شمسی";

				worksheet.Cells[2, 15].Value = "تاریخ ارجاع متصدی";

				int i = 0;

				for (var row = 3; row <= transactionToExelAll.Count + 1; row++)
				{
					worksheet.Cells[row, 1].Value = i;
					worksheet.Cells[row, 2].Value = transactionToExelAll[i].FirstName;
					worksheet.Cells[row, 3].Value = transactionToExelAll[i].LastName;
					worksheet.Cells[row, 4].Value = transactionToExelAll[i].Code;
					worksheet.Cells[row, 5].Value = transactionToExelAll[i].NationalCode;
					worksheet.Cells[row, 6].Value = transactionToExelAll[i].ProvinceName;
					worksheet.Cells[row, 7].Value = transactionToExelAll[i].OrganizationName;
					worksheet.Cells[row, 8].Value = transactionToExelAll[i].UnitName;

					//Hob 9

					worksheet.Cells[row, 9].Value = transactionToExelAll[i].organization;
					worksheet.Cells[row, 10].Value = transactionToExelAll[i].lastStatusCheck;
					worksheet.Cells[row, 11].Value = transactionToExelAll[i].lastStatusCheckFa;
					worksheet.Cells[row, 12].Value = transactionToExelAll[i].isContagious;

					worksheet.Cells[row, 13].Value = transactionToExelAll[i].quarantineEndDate;
					worksheet.Cells[row, 14].Value = transactionToExelAll[i].quarantineEndDateFa;

					i++;
				}



				worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
				package.Save();
			}
			return await Task.FromResult(content);

		}
		#region[Comment]
		//[HttpPost]
		//public async Task<IActionResult> ExportExcel()
		//{
		//    try
		//    {
		//        var data = await GenerateExcelReport();

		//        data.Position = 0;
		//        return new FileStreamResult(data, System.Net.Mime.MediaTypeNames.Application.Octet)
		//        {
		//            FileDownloadName = $"Export_{System.Guid.NewGuid().ToString().Substring(0, 5)}.xlsx"
		//        };
		//    }
		//    catch (System.Exception ex)
		//    {
		//        throw;
		//    }

		//}


		//public async Task<MemoryStream> GenerateExcelReport()
		//{
		//    var data = new List<GetLastStatusOfEmployeesDtoResponseVM>()
		//    {
		//        new GetLastStatusOfEmployeesDtoResponseVM
		//        {
		//               IsError = false,
		//        Message = "پیام و توضیحات",
		//        StatusCode = 200,
		//        Version = "1.0",
		//        ResultResponse = new List<ResultResponseVM>()
		//        {
		//            new ResultResponseVM
		//            {
		//                IsContagious = true,
		//                LastStatusCheck = "2020/01/01",
		//                LastStatusCheckFa = "1400/01/01",
		//                NationalCode ="1234567890",
		//                Organization = "سازمان",
		//                QuarantineEndDate = "2021/01/01",
		//                QuarantineEndDateFa = "1400/01/01"
		//            }
		//        }
		//        }
		//    };
		//    System.IO.MemoryStream content = new System.IO.MemoryStream();
		//    using var package = new ExcelPackage(content);
		//    var worksheet = package.Workbook.Worksheets.Add("استعلام وضعیت ابتلا");
		//    worksheet.View.RightToLeft = true;
		//    worksheet.Cells["A:Z"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

		//    System.Drawing.Color colFromHex01 = System.Drawing.ColorTranslator.FromHtml("#B7DEE8");
		//    worksheet.Cells["A1:H1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
		//    worksheet.Cells["A1:H1"].Style.Fill.BackgroundColor.SetColor(colFromHex01);
		//    worksheet.Cells["A1:H1"].Style.Font.Name = "Tahoma";
		//    worksheet.View.FreezePanes(1, 4);



		//    worksheet.Cells[1, 1].Value = "نسخه سرویس";
		//    worksheet.Cells[1, 2].Value = "توضیحات";
		//    worksheet.Cells[1, 3].Value = "وضعیت فراخوانی سرویس ";
		//    worksheet.Cells[1, 4].Value = "آیا فراخوانی با خطا مواجه شد ؟ ";
		//    worksheet.Cells[1, 5].Value = "تعداد نتایج (Result)";

		//    worksheet.Cells.AutoFitColumns();

		//    int i = 0;
		//    for (var row = 2; row < data.Count + 2; row++)
		//    {
		//        worksheet.Cells[row, 1].Value = data[i].Version;
		//        worksheet.Cells[row, 2].Value = data[i].Message;
		//        worksheet.Cells[row, 3].Value = data[i].StatusCode;
		//        worksheet.Cells[row, 4].Value = data[i].IsError;
		//        worksheet.Cells[row, 5].Value = data[i].ResultResponse.Count;
		//        if (data[i].ResultResponse.Count > 0)
		//        {
		//            var worksheetChild = package.Workbook.Worksheets.Add("استعلام وضعیت ابتلا نتایج");
		//            worksheetChild.View.RightToLeft = true;
		//            worksheetChild.Cells["A:Z"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

		//            worksheetChild.Cells["A1:H1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
		//            worksheetChild.Cells["A1:H1"].Style.Fill.BackgroundColor.SetColor(colFromHex01);
		//            worksheetChild.Cells["A1:H1"].Style.Font.Name = "Tahoma";
		//            worksheetChild.View.FreezePanes(1, 7);

		//            worksheetChild.Cells[1, 1].Value = "نام کاربری سازمان";
		//            worksheetChild.Cells[1, 2].Value = "کد ملی کارمند";
		//            worksheetChild.Cells[1, 3].Value = "تاریخ و ساعت بررسی وضعیت ";
		//            worksheetChild.Cells[1, 4].Value = "تاریخ و ساعت بررسی وضعیت شمسی";
		//            worksheetChild.Cells[1, 5].Value = "آیا ناقل است؟";
		//            worksheetChild.Cells[1, 6].Value = "تاریخ اتمام قرنطینه";
		//            worksheetChild.Cells[1, 7].Value = "تاریخ شمسی اتمام قرنطینه";
		//            worksheetChild.Cells.AutoFitColumns();
		//            int y = 0;
		//            for (int x = 2; x < data[i].ResultResponse.Count + 2; x++)
		//            {
		//                worksheetChild.Cells[x, 1].Value = data[i].ResultResponse[y].Organization;
		//                worksheetChild.Cells[x, 2].Value = data[i].ResultResponse[y].NationalCode;
		//                worksheetChild.Cells[x, 3].Value = data[i].ResultResponse[y].LastStatusCheck;
		//                worksheetChild.Cells[x, 4].Value = data[i].ResultResponse[y].LastStatusCheckFa;
		//                worksheetChild.Cells[x, 5].Value = data[i].ResultResponse[y].IsContagious;
		//                worksheetChild.Cells[x, 6].Value = data[i].ResultResponse[y].QuarantineEndDate;
		//                worksheetChild.Cells[x, 7].Value = data[i].ResultResponse[y].QuarantineEndDateFa;
		//                y++;
		//            }

		//        }
		//        i++;
		//    }
		//    package.Save();
		//    return await Task.FromResult(content);
		//}

		//[HttpGet("InquiryEmployee/ResponseServicr/{pageNumber?}")]
		//[HttpGet("InquiryEmployee/ResponseServicr/{isTransporter?}/{pageNumber?}")]
		//public async Task<IActionResult> ResponseServicr(bool isTransporter = false, int pageNumber = 1)
		//{
		//    //var serviceResult = await MockDataAsync(); //await Task.FromResult(GetLastStatusOfEmployees());
		//    var serviceResult = await Task.FromResult(GetLastStatusOfEmployees());
		//    int totalPages = 0;
		//    int count = 0;
		//    var info = new List<Result>();
		//    var resultData = new List<Result>();

		//    if (isTransporter)
		//    {
		//        info = serviceResult.result.Where(w => w.isContagious == true).ToList();
		//        ViewBag.FlagIsContagious = true;
		//    }
		//    else
		//        info = serviceResult.result.ToList();

		//    count = info.Count;
		//    totalPages = (int)Math.Ceiling((decimal)(count) / 10);
		//    if (pageNumber <= 0)
		//        pageNumber = 1;
		//    if (pageNumber >= totalPages)
		//        pageNumber = totalPages;
		//    resultData = info.Skip(10 * (pageNumber - 1)).Take(10).ToList();

		//    var model = new ResultDtoPager
		//    {
		//        ResultDto = serviceResult,
		//        ResultListDto = resultData,
		//        PagingInfo = new PagingInfo
		//        {
		//            CurrentPage = pageNumber,
		//            ItemsPerPage = 10,
		//            TotalItems = count
		//        }
		//    };
		//    return View(model);
		//}
		//[HttpGet("InquiryEmployee/ResponseServicrSearch/{search?}")]
		//public async Task<IActionResult> ResponseServicrSearch(string search = "")
		//{
		//    if (string.IsNullOrWhiteSpace(search))
		//        return View();

		//    //var serviceResult = await MockDataAsync();//await Task.FromResult(GetLastStatusOfEmployees());
		//    var serviceResult = await Task.FromResult(GetLastStatusOfEmployees());

		//    var resultData = serviceResult.result.Where(c => c.nationalCode == search).ToList();
		//    var model = new ResultDtoPager
		//    {
		//        ResultDto = serviceResult,
		//        ResultListDto = resultData,
		//        PagingInfo = new PagingInfo
		//        {
		//            CurrentPage = 1,
		//            ItemsPerPage = 10,
		//            TotalItems = 1
		//        }
		//    };
		//    return View(model);
		//}
		#endregion
	}
}