using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Web.Core.RegisterEmployee.ViewModels;
using Web.Extensions.Attributes;

namespace Web.Core.RegisterEmployee
{
	[Authorize]
	[DisplayName("ثبت کارمندان کرونا")]
	public class RegisterEmployeeController : Controller
	{
		private readonly IConfiguration _configuration;
		private readonly IWebHostEnvironment _hostingEnvironment;
		public RegisterEmployeeController(IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
		{
			_configuration = configuration;
			_hostingEnvironment = hostingEnvironment;
		}
		[Permission]
		[DisplayName("ثبت کارمندان")]
		[HttpGet]
		public IActionResult Index()
		{
			//FileUploadViewModel model = new FileUploadViewModel();
			return View();//model
		}
		[HttpPost]
		public async Task<IActionResult> Index(FileUploadViewModel xls, int empStatus)
		{
			try
			{
				var readUspass = ReadUserAndPass();
				#region [ExcelDataShow]
				string rootFolder = _hostingEnvironment.WebRootPath;
				string fileName = Guid.NewGuid().ToString() + xls.XlsFile.FileName;
				string path = rootFolder + "/uploads/Covid19Excell";

				if (!Directory.Exists(path))
					Directory.CreateDirectory(path);

				FileInfo file = new FileInfo(Path.Combine(path, fileName));

				using (var stream = new MemoryStream())
				{
					await xls.XlsFile.CopyToAsync(stream);
					ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
					using (var package = new ExcelPackage(stream))
					{
						package.SaveAs(file);
						//save excel file in your wwwroot folder and get this excel file from wwwroot
					}
				}

				ViewBag.AddressFile = file.FullName;
				ViewBag.Ui1 = readUspass.usernameHeader;
				ViewBag.Pw1 = readUspass.passwordHeader;

				ViewBag.Ui2 = readUspass.usernameBody;
				ViewBag.Pw2 = readUspass.passwordBody;
				ViewBag.EmpStatus = empStatus;

				//After save excel file in wwwroot and then

				List<Employee> employees = new List<Employee>();
				ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
				using (ExcelPackage package = new ExcelPackage(file))
				{
					ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
					if (worksheet == null)
					{
						ViewBag.MessageFaild = "مشکلی در بار گزاری فایل بوجود آمده است";
					}

					//read excel file data and add data in  model.StaffInfoViewModel.StaffList
					var rowCount = worksheet.Dimension.Rows;

					for (int row = 2; row <= rowCount; row++)
					{
						var employee = new Employee();

						employee.employeeStatus = empStatus;
						employee.nationalCode = (worksheet.Cells[row, 1].Value ?? string.Empty).ToString().Trim();

						employees.Add(employee);
					}
				}
				//return same view and  pass view model 
				return View(employees);
				#endregion
			}
			catch (Exception)
			{
				ViewBag.MessageFaild = "قالب ورودی اکسل مشکل دارد";
				return View();
			}


		}
		[HttpPost]
		public async Task<IActionResult> SendExcellData(string addressFile, int empStatus)
		{
			var readUspass = ReadUserAndPass();
			string code = "";
			//Read Excel File
			var empList = new List<Employee>();
			try
			{
				//http://www.dotnet-tutorials.net/Article/read-an-excel-file-in-csharp

				// path to your excel file
				FileInfo fileInfo = new FileInfo(addressFile);
				ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
				ExcelPackage package = new ExcelPackage(fileInfo);
				//ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

				ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
				// get number of rows and columns in the sheet
				int rows = worksheet.Dimension.Rows;
				int columns = worksheet.Dimension.Columns;


				// loop through the worksheet rows and columns
				for (int i = 2; i <= rows; i++)
				{
					var NationalCode1 = worksheet?.Cells[i, 1]?.Value?.ToString();
					//   var EmployeeStatus2 = int.Parse(worksheet?.Cells[i, 2]?.Value?.ToString());
					empList.Add(new Employee
					{
						nationalCode = NationalCode1,
						employeeStatus = empStatus
					});
				}
			}
			catch (Exception ex)
			{
				ViewBag.Error = ex.Message + "\n" + "فرمت فایل اکسل منطبق نمی باشد.";
				return View();
			}

			try
			{
				var readUrlFromAppSettings = _configuration.GetSection("CovidUrlAddress").GetSection("Register").Value;
				var url = readUrlFromAppSettings;

				var saveEmployeeListDtoRequest = new SaveEmployeeListDtoRequest
				{
					username = readUspass.usernameBody,
					password = readUspass.passwordBody,
					Employees = empList
				};
				var json = JsonConvert.SerializeObject(saveEmployeeListDtoRequest);
				var data = new StringContent(json, Encoding.UTF8, "application/json");
				var client = new HttpClient();
				var authToken = Encoding.ASCII.GetBytes($"{readUspass.usernameHeader}:{readUspass.passwordHeader}");
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
				var result = await client.PostAsync(url, data);

				if (result.StatusCode != HttpStatusCode.OK)
				{
					ViewBag.Error = code;
					return View();
				}
				else
				{
					code = result.StatusCode.ToString();
					ViewBag.Error = code;

					var response = await result.Content.ReadAsStringAsync();
					var obj = JsonConvert.DeserializeObject<List<SaveEmployeeListDtoResponse>>(response);
					ViewBag.Response = response;
					ViewBag.StatusCode = result.StatusCode;
					return View(obj);
				}
			}
			catch (Exception ex)
			{
				ViewBag.Error = ex.Message;
				return View();
			}
		}
		[HttpPost]
		public async Task<IActionResult> SendSimpleData(string nationalCode, int statusCode)
		{
			var readUspass = ReadUserAndPass();
			var empList = new List<Employee>
			{
				new Employee
				{
					nationalCode =nationalCode,
					employeeStatus = statusCode
				}
			};
			try
			{
				var readUrlFromAppSettings = _configuration.GetSection("CovidUrlAddress").GetSection("Register").Value;
				var url = readUrlFromAppSettings;

				var saveEmployeeListDtoRequest = new SaveEmployeeListDtoRequest
				{
					username = readUspass.usernameBody,
					password = readUspass.passwordBody,
					Employees = empList
				};
				var json = JsonConvert.SerializeObject(saveEmployeeListDtoRequest);
				var data = new StringContent(json, Encoding.UTF8, "application/json");
				var client = new HttpClient();
				var authToken = Encoding.ASCII.GetBytes($"{readUspass.usernameHeader}:{readUspass.passwordHeader}");
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
				var result = await client.PostAsync(url, data);

				if (result.StatusCode != HttpStatusCode.OK)
				{
					return View();
				}
				else
				{
					var response = await result.Content.ReadAsStringAsync();
					var obj = JsonConvert.DeserializeObject<List<SaveEmployeeListDtoResponse>>(response);
					ViewBag.Response = response;
					ViewBag.StatusCode = result.StatusCode;
					return View(obj);
				}
			}
			catch (Exception ex)
			{
				ViewBag.Error = ex.Message;
				return View();
			}
			return View();
		}
		[Permission]
		[DisplayName("ایجاد نام کاربری و رمز ورود")]
		[HttpGet]
		public IActionResult CreateUser()
		{
			return View();
		}
		[Permission]
		[HttpPost]
		public async Task<IActionResult> CreateUser(SaveEmployeeListDtoRequestDto inputVM)
		{
			if (!ModelState.IsValid)
				return View(inputVM);
			using (var sw = new StreamWriter(_hostingEnvironment.WebRootPath + "\\uploads\\refahWriterRegister.txt", false, Encoding.UTF8))
			{
				await sw.WriteLineAsync(inputVM.usernameHeader);
				await sw.WriteLineAsync(inputVM.passwordHeader);
				await sw.WriteLineAsync(inputVM.usernameBody);
				await sw.WriteLineAsync(inputVM.passwordBody);
				ViewBag.Message = "اطلاعات با موفقیت ذخیره گردید";
			}
			return View();
		}
		public void Writhe(string ui1, string ui2, string pw1, string pw2)
		{
			using (var write = new StreamWriter(_hostingEnvironment.WebRootPath + "\\uploads\\refahReader.txt", true, Encoding.UTF8))
			{
				write.WriteLine("From Register.");
				write.WriteLine($"User01={ui1}\t Pass01={pw1}");
				write.WriteLine($"User02={ui2}\t Pass02={pw2}");
				write.WriteLine("------------------");
			}
		}
		private SaveEmployeeListDtoRequestDto ReadUserAndPass()
		{
			int counter = 0;
			string line = "";
			string[] values = new string[4];
			SaveEmployeeListDtoRequestDto model = new SaveEmployeeListDtoRequestDto();
			string path = _hostingEnvironment.WebRootPath + "\\uploads\\refahWriterRegister.txt";
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
	}
}