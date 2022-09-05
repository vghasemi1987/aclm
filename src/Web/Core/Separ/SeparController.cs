using System;
using System.IO;
using KendoHelper;
using System.Linq;
using Newtonsoft.Json;
using DomainContracts.Commons;
using DomainContracts.BroadcastAggregate;
using DomainEntities.ApplicationUserAggregate;
using DomainEntities.BroadcastAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Web.ActionFilters;
using Web.Core.Separ.ViewModels;
using Web.Extensions;
using Web.Extensions.Attributes;

namespace Web.Core.Separ
{
	[Authorize]
	[DisplayName("سپر")]
	[ServiceFilter(typeof(UserLogMessageAttribute))]
	public class SeparController : Controller
	{
		private readonly IConfiguration _configuration;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IBroadCastRepository _broadCastRepository;
		private readonly IReferralBroadCastRepository _referralBroadCastRepository;
		private readonly IGroupingOfficeRepository _groupingOfficeRepository;
		private readonly IGroupingOfficeMemberRepository _groupingOfficeMemberRepository;
		private readonly RoleManager<ApplicationRole> _roleManager;
		private readonly IUnitOfWork _unitOfWork;



		private readonly IMessageHandlerRepository _messageHandlerRepository;
		private IWebHostEnvironment _hostEnvironment;



		public SeparController(
			IConfiguration configuration,
			UserManager<ApplicationUser> userManager,
			IBroadCastRepository broadCastRepository,
			IUnitOfWork unitOfWork,
			IReferralBroadCastRepository referralBroadCastRepository,
			IGroupingOfficeRepository groupingOfficeRepository,
			IGroupingOfficeMemberRepository groupingOfficeMemberRepository,
			RoleManager<ApplicationRole> roleManager,
			IMessageHandlerRepository messageHandlerRepository,
			IWebHostEnvironment hostEnvironment
			)
		{
			_userManager = userManager;
			_broadCastRepository = broadCastRepository;
			_unitOfWork = unitOfWork;
			_groupingOfficeRepository = groupingOfficeRepository;
			_referralBroadCastRepository = referralBroadCastRepository;
			_groupingOfficeMemberRepository = groupingOfficeMemberRepository;
			_roleManager = roleManager;
			_configuration = configuration;
			_messageHandlerRepository = messageHandlerRepository;
			_hostEnvironment = hostEnvironment;
		}
		[Permission]
		[DisplayName("لیست نمایشی")]
		public IActionResult Index()
		{
			return View();
		}
		public async Task<IActionResult> GetRecords(string models)
		{
			var UserId = User.GetUserId();
			var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);
			var list = await _referralBroadCastRepository.GetList(request, UserId);
			return Json(list);
		}
		[Permission]
		[DisplayName("خبر فوری")]
		[HttpGet]
		public async Task<IActionResult> SendImmediateMessage()
		{
			BroadCastViewModelGet model = new();

			string roleName = _configuration.GetSection("Separ").GetSection("Role").Value;
			//لیست کاربرانی که رول آنها سپر باشد
			var separ = await _userManager.GetUsersInRoleAsync(roleName);
			model.Users = separ;
			model.GroupingOffices = await _groupingOfficeRepository.GetGroupingOfficeAll();
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> SendImmediateMessage(BroadCastViewModelPost inputModel)
		{
			BroadCastViewModelGet model = new();
			string roleName = _configuration.GetSection("Separ").GetSection("Role").Value;

			//لیست کاربرانی که رول آنها سپر باشد
			var separ = await _userManager.GetUsersInRoleAsync(roleName);

			//var serparUsers = _roleManager.Roles.ToList().Where(w=>w.ApplicationUserRoles == _userManager.Getuser);

			model.Users = separ;

			model.GroupingOffices = await _groupingOfficeRepository.GetGroupingOfficeAll();
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			// find user is send message by extention method

			var UserId = User.GetUserId();

			var loginUser = _userManager.Users.Where(w => w.Id == UserId).SingleOrDefault();
			//
			//Set ReferralBroadCast 01
			//var referralBroadCasts01 = new List<ReferralBroadCast>(inputModel.SelectedUsers.Select(c => new ReferralBroadCast
			//{
			//    SrcUser = UserId,
			//    DstUserID = c,
			//    DeadLine = inputModel.DateTimeSubject != null ? ApplicationCommon.CustomDateTime.ToMiladiDate(inputModel.DateTimeSubject) : null,
			//    IsImmediate = true,
			//}));
			//
			//Set ReferralBroadCast 02
			List<int> usersId = new List<int>();

			foreach (var item in inputModel.SelectedUsers)
			{
				usersId.Add(item);
			}


			foreach (int item in inputModel.SelectedGroup)
			{
				var enumerableUserId = (await _groupingOfficeMemberRepository.ListAllAsync()).Where(w => w.GroupingOfficeId == item).Select(c => c.ApplicationUserId);
				usersId.AddRange(enumerableUserId);
			}
			if (usersId.Count == 0)
			{
				ViewBag.Message = "خطا ، کاربری انتخاب نگردیده است !";
				return View(model);
			}



			var dis = usersId.Distinct();
			//var referralBroadCasts02 = new List<ReferralBroadCast>(inputModel.SelectedGroup.Select(c => new ReferralBroadCast
			//{
			//    SrcUser = UserId,
			//    DstUserID = c,
			//    DeadLine = inputModel.DateTimeSubject != null ? ApplicationCommon.CustomDateTime.ToMiladiDate(inputModel.DateTimeSubject) : null,
			//    IsImmediate = true,
			//}));



			//referralBroadCasts01.AddRange(referralBroadCasts02);

			//end

			var broadCast = new BroadCast
			{
				UserNameSender = User.Identity.Name,
				FirstName = loginUser.FirstName,
				LastName = loginUser.LastName,
				PersonnelCode = loginUser.PersonnelCode,
				BroadCastType = DomainEntities.BroadcastAggregate.BroadCastTypeEnum.Immediate,
				Subject = inputModel.Subject,
				Text = inputModel.Text,
				CreateDate = DateTime.Now,

				//ReferralBroadCasts = referralBroadCasts01,



				ReferralBroadCasts = new List<ReferralBroadCast>(dis.Select(c => new ReferralBroadCast
				{
					SrcUser = UserId,
					DstUserID = c,
					DeadLine = inputModel.DateTimeSubject != null ? ApplicationCommon.CustomDateTime.ToMiladiDate(inputModel.DateTimeSubject) : null,
					IsImmediate = true,
				}))
			};

			_broadCastRepository.Add(broadCast);
			await _unitOfWork.SaveAsync();
			ViewBag.Message = "اطلاعات با موفقیت ثبت گردید";
			return View(model);
		}
		[Permission]
		[DisplayName("خبر عمومی")]
		[HttpGet]
		public async Task<IActionResult> SendCommonMessage()
		{
			BroadCastViewModelGet model = new();
			model.GroupingOffices = await _groupingOfficeRepository.GetGroupingOfficeAll();
			string roleName = _configuration.GetSection("Separ").GetSection("Role").Value;
			var separ = await _userManager.GetUsersInRoleAsync(roleName);
			model.Users = separ;
			//await Task.FromResult(model.Users = _userManager.Users);
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> SendCommonMessage(BroadCastViewModelPost inputModel)
		{
			BroadCastViewModelGet model = new();
			string roleName = _configuration.GetSection("Separ").GetSection("Role").Value;
			var separ = await _userManager.GetUsersInRoleAsync(roleName);
			model.Users = separ;

			model.GroupingOffices = await _groupingOfficeRepository.GetGroupingOfficeAll();
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			// find user is send message by extention method

			var UserId = User.GetUserId();

			var loginUser = _userManager.Users.Where(w => w.Id == UserId).SingleOrDefault();


			//Set ReferralBroadCast 02
			List<int> usersId = new List<int>();

			foreach (var item in inputModel.SelectedUsers)
			{
				usersId.Add(item);
			}


			foreach (int item in inputModel.SelectedGroup)
			{
				var enumerableUserId = (await _groupingOfficeMemberRepository.ListAllAsync()).Where(w => w.GroupingOfficeId == item).Select(c => c.ApplicationUserId);
				usersId.AddRange(enumerableUserId);
			}
			if (usersId.Count == 0)
			{
				ViewBag.Message = "خطا ، کاربری انتخاب نگردیده است !";
				return View(model);
			}
			var dis = usersId.Distinct();

			var broadCast = new BroadCast
			{
				UserNameSender = User.Identity.Name,
				FirstName = loginUser.FirstName,
				LastName = loginUser.LastName,
				PersonnelCode = loginUser.PersonnelCode,
				BroadCastType = DomainEntities.BroadcastAggregate.BroadCastTypeEnum.General,
				Subject = inputModel.Subject,
				Text = inputModel.Text,
				CreateDate = DateTime.Now,
				//ReferralBroadCasts = new List<ReferralBroadCast>(inputModel.SelectedUsers.Select(c => new ReferralBroadCast
				//{
				//	SrcUser = UserId,
				//	DstUserID = c,
				//	DeadLine = inputModel.DateTimeSubject != null ? ApplicationCommon.CustomDateTime.ToMiladiDate(inputModel.DateTimeSubject) : null,
				//	IsImmediate = false,
				//}))
				ReferralBroadCasts = new List<ReferralBroadCast>(dis.Select(c => new ReferralBroadCast
				{
					SrcUser = UserId,
					DstUserID = c,
					DeadLine = inputModel.DateTimeSubject != null ? ApplicationCommon.CustomDateTime.ToMiladiDate(inputModel.DateTimeSubject) : null,
					IsImmediate = false,
				}))
			};

			_broadCastRepository.Add(broadCast);
			await _unitOfWork.SaveAsync();
			ViewBag.Message = "اطلاعات با موفقیت ثبت گردید";
			return View(model);

		}
		[HttpGet]
		[Permission]
		[DisplayName("مشاهده جزییات")]
		public async Task<IActionResult> ShowDetails(int id)
		{
			var result = await _referralBroadCastRepository.GetById(id);
			// Update Status
			result.Status = ReferralStatusBroadCastEnum.MoshahedeShode;
			_referralBroadCastRepository.Update(result);
			await _unitOfWork.SaveAsync();

			var model = new ReferralBroadCastViewModel();

			//bind Model
			model.BroadCastId = result.Id;
			model.BroadCast.Subject = result.BroadCast.Subject;
			model.BroadCast.Text = result.BroadCast.Text;
			model.DeadLine = result.DeadLine;
			model.ActionDescription = result.ActionDescription;
			model.DeadLine = result.DeadLine;

			return PartialView("_Detail", model);
		}
		public async Task<IActionResult> ShowDetailsEgdam(int id)
		{
			var result = await _referralBroadCastRepository.GetById(id);
			return Json(result.ActionDescription);
		}
		[HttpPost]
		[Permission]
		public async Task<IActionResult> ActionEghdam(int BroadCastId, string ActionDescription)
		{
			var result = await _referralBroadCastRepository.GetByIdUpdate(BroadCastId);
			result.ActionDescription = ActionDescription;
			result.Status = ReferralStatusBroadCastEnum.EghdamShode;
			_referralBroadCastRepository.Update(result);
			await _unitOfWork.SaveAsync();
			return RedirectToAction("Index");
		}
		[HttpGet]
		[Permission/*(Roles = "admin")*/]
		[DisplayName("مدیریت پیام ها")]
		public async Task<IActionResult> MessageDetails(string message = "")
		{
			var messageAll = await _messageHandlerRepository.GetMessageAll();
			if (!string.IsNullOrWhiteSpace(message))
			{
				ViewData["Message"] = message;
			}
			if (messageAll.Count > 0)
			{
				return View(messageAll);
			}
			return View(new List<MessageHandler>());

		}
		[HttpPost]
		[Permission/*(Roles = "admin")*/]
		public async Task<IActionResult> MessageDetails(MessageHandler messageHandler, IFormFile file)
		{
			if (!ModelState.IsValid)
			{
				return View(await _messageHandlerRepository.GetMessageAll());
			}
			var messageData = new MessageHandler
			{
				MessageType = messageHandler.MessageType,
				Description = messageHandler.Description,
				Title = messageHandler.Title,
				Address = "",
				FileName = "",

			};
			if (file != null)
			{
				string uploads = Path.Combine(_hostEnvironment.WebRootPath, "uploads\\MessageDocuments");
				if (!Directory.Exists(uploads))
				{
					Directory.CreateDirectory(uploads);
				}
				string filePath = Path.Combine(uploads, file.FileName);

				using (Stream fileStream = new FileStream(filePath, FileMode.Create))
				{
					messageData.Address = filePath;
					messageData.FileName = file.FileName;
					await file.CopyToAsync(fileStream);
				}
			}
			_messageHandlerRepository.Add(messageData);
			await _unitOfWork.SaveAsync();
			ViewData["Message"] = "پیام با موفقیت ذخیره گردید";
			return View(await _messageHandlerRepository.GetMessageAll());
		}

		public FileResult DownloadFile(string path)
		{
			string uploads = Path.Combine(_hostEnvironment.WebRootPath, "uploads\\MessageDocuments");
			string filePath = Path.Combine(uploads, path);
			return new PhysicalFileResult(filePath, "application/pdf");
		}
		[HttpPost]
		[Permission/*(Roles = "admin")*/]
		[DisplayName(" حذف مدیریت پیام ها")]
		public async Task<IActionResult> DeleteMessage(int id)
		{
			var entity = await _messageHandlerRepository.GetById(id);
			if (entity != null)
			{
				_messageHandlerRepository.Delete(entity);
				await _unitOfWork.SaveAsync();

				return RedirectToAction("MessageDetails", new { message = "اطلاعات با موفقیت حذف گردید" });
			}
			else
			{
				return RedirectToAction("MessageDetails", new { message = "مشکلی برای حذف مقدار پیش آمده است" });
			}
		}
		[HttpGet]
		[Permission]
		[DisplayName("مشاهده اطلاع رسانی")]
		public async Task<IActionResult> ShowMessgeBoard()
		{
			var model = (await _messageHandlerRepository.GetMessageAll()).Where(w => w.MessageType == MessageType.General);
			return View(model.ToList());
		}
	}
}
