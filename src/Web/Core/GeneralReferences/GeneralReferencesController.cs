﻿using ApplicationCommon;
using AutoMapper;
using DomainContracts.BankBranchAggregate;
using DomainContracts.BroadcastAggregate;
using DomainContracts.Commons;
using DomainEntities.ApplicationUserAggregate;
using DomainEntities.BroadcastAggregate;
using KendoHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Web.ActionFilters;
using Web.Core.Separ.ViewModels;
using Web.Extensions;
using Web.Extensions.Attributes;

namespace Web.Core.GeneralReferences
{
	[DisplayName("ارسال اخبار")]
	public class GeneralReferencesController : Controller
	{
		private readonly IConfiguration _configuration;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IBroadCastRepository _broadCastRepository;
		private readonly IReferralBroadCastRepository _referralBroadCastRepository;
		private readonly IUnitOfWork _unitOfWork;

		private readonly IBankBranchRepository _bankBranchRepository;
		//لیست گروه ها
		private readonly IGroupingOfficeRepository _groupingOfficeRepository;
		private readonly IGroupingOfficeMemberRepository _groupingOfficeMemberRepository;

		private readonly IMapper _mapper;
		private readonly IUserLogMessageRepository _userLogMessageRepository;
		//لیست موضوعات
		private readonly IProtectionOfficeRepository _protectionOfficeRepositorySubject;
		private readonly IProtectionOfficeMemberRepository _protectionOfficeMemberRepositorySubject;
		//private readonly IProtectionOfficeUserRelationRepository _protectionOfficeUserRelationRepository;

		public GeneralReferencesController(UserManager<ApplicationUser> userManager,
			IBroadCastRepository broadCastRepository,
			IUnitOfWork unitOfWork,
			IReferralBroadCastRepository referralBroadCastRepository,
			IGroupingOfficeRepository groupingOfficeRepository,
			IGroupingOfficeMemberRepository groupingOfficeMemberRepository,
			IMapper mapper,
			IUserLogMessageRepository userLogMessageRepository,
			IConfiguration configuration,
			IProtectionOfficeRepository protectionOfficeRepository,
			IProtectionOfficeMemberRepository protectionOfficeMemberRepository,
			IBankBranchRepository bankBranchRepository
			)
		{
			_userManager = userManager;
			_broadCastRepository = broadCastRepository;
			_unitOfWork = unitOfWork;

			_referralBroadCastRepository = referralBroadCastRepository;

			_groupingOfficeRepository = groupingOfficeRepository;
			_groupingOfficeMemberRepository = groupingOfficeMemberRepository;
			_mapper = mapper;
			_userLogMessageRepository = userLogMessageRepository;
			_configuration = configuration;

			_protectionOfficeRepositorySubject = protectionOfficeRepository;
			_protectionOfficeMemberRepositorySubject = protectionOfficeMemberRepository;
			_bankBranchRepository = bankBranchRepository;

		}
		[Permission]
		[DisplayName("خبر عمومی")]
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		[HttpGet]
		public async Task<IActionResult> SendMessage()
		{
			var subjects = await _protectionOfficeRepositorySubject.GetProtectionOfficeAll();
			ViewBag.Subjects = subjects;
			var clientIp = HttpContext.Connection.RemoteIpAddress;
			var ipTown = ApplicationCommon.TownIP.ValidateIPv4(clientIp.ToString());
			BroadCastViewModelGeneral model = new BroadCastViewModelGeneral();
			ViewBag.Town = ipTown;
			foreach (var pro in _groupingOfficeRepository.ListAll())
			{
				model.ProtectionOfficeViewModels.Add(new ViewModels.ProtectionOfficeViewModel { Id = pro.Id, Title = pro.Title });
			}
			return View(model);
		}
		[HttpPost]
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		public async Task<IActionResult> SendMessage(BroadCastViewModelGeneral inputModel)
		{
			var userDestinitionSubject = new List<ProtectionOfficeMember>();
			//Towns --> مقصد استان 
			string town = inputModel.Town;
			var branches = await _bankBranchRepository.GetBranchByName(town);

			if (branches == null)
			{
				ViewBag.Message = "حوزه انتخابی فاقد اعتبار می باشد";
				return View(inputModel);
			}
			var userDestinitionTown = _userManager.Users.Where(w => w.BankBranchId == branches.Id);

			var userProtectionDestination = await _protectionOfficeMemberRepositorySubject.GetByProtectionOfficeUserId(userDestinitionTown.Select(s => s.Id).ToList());

			if (userProtectionDestination == null)
			{
				ViewBag.Message = "حوزه انتخابی فاقد نفرات می باشد";
				return View(inputModel);
			}


			try
			{
				if (!ModelState.IsValid)
				{
					return View(inputModel);
				}

				// استان انتخاب شده است یا خیر؟
				// اگر استان انتخاب شد >>>> مسئول حفاظت ان استان (از جدول ریلیشن)
				// اگر استان انتخاب نشده بود >>>> مسئولین 4 بخش

				List<ReferralBroadCast> listRefBroadCast = new();
				ReferralBroadCast addRefferal = new();
				var usernameSender = string.Empty;
				if (!string.IsNullOrWhiteSpace(inputModel.PersonnelCode))
				{
					var result = _userManager.Users?.Where(w => w.PersonnelCode == inputModel.PersonnelCode)?.FirstOrDefault();
					if (result != null) usernameSender = result.UserName;
				}



				//اگر مقصد انتخاب شده بود

				if (userProtectionDestination.Count != 0)
				{
					foreach (var item in userProtectionDestination)
					{
						listRefBroadCast.Add(new ReferralBroadCast { DstUserID = item.ApplicationUserId });
					}
				}


				//if (addRefferal != null)
				//	listRefBroadCast.Add(addRefferal);


				var broadCast = new BroadCast
				{
					FirstName = inputModel.FirstName,
					LastName = inputModel.LastName,
					PersonnelCode = inputModel.PersonnelCode,
					Subject = inputModel.Subject,
					Text = inputModel.Text,
					BroadCastType = DomainEntities.BroadcastAggregate.BroadCastTypeEnum.General,
					ReferralBroadCasts = listRefBroadCast,
					CreateDate = DateTime.Now,
					UserNameSender = "",

				};

				_broadCastRepository.Add(broadCast);
				await _unitOfWork.SaveAsync();
				ViewBag.Message = "اطلاعات مشتری با موفقیت ثبت گردید";


				var subjects = await _protectionOfficeRepositorySubject.GetProtectionOfficeAll();
				ViewBag.Subjects = subjects;
				var clientIp = HttpContext.Connection.RemoteIpAddress;
				var ipTown = ApplicationCommon.TownIP.ValidateIPv4(clientIp.ToString());
				BroadCastViewModelGeneral model = new BroadCastViewModelGeneral();
				ViewBag.Town = ipTown;
				//BroadCastViewModelGeneral model = new BroadCastViewModelGeneral();
				foreach (var pro in _groupingOfficeRepository.ListAll())
				{
					model.ProtectionOfficeViewModels.Add(new ViewModels.ProtectionOfficeViewModel { Id = pro.Id, Title = pro.Title });
				}
				return View(model);
			}
			catch (Exception ex)
			{
				ViewBag.Message = "اطلاعات مشتری با موفقیت ثبت نگردید" + "\n" + ex.Message;
				BroadCastViewModelGeneral model = new BroadCastViewModelGeneral();
				foreach (var pro in _groupingOfficeRepository.ListAll())
				{
					model.ProtectionOfficeViewModels.Add(new ViewModels.ProtectionOfficeViewModel { Id = pro.Id, Title = pro.Title });
				}
				return View();
			}
		}
		//777
		[Authorize]
		[Permission]
		[DisplayName("لیست مراکز حراست")]
		[HttpGet]
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		public async Task<IActionResult> ListTowns(string message = "")
		{
			ViewBag.Message = message;
			var model = await _groupingOfficeRepository.GetGroupingOfficeAll();
			//var model = await _groupingOfficeRepository.GetGroupingOfficeAll();
			return View(model);
		}
		[HttpPost]
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		public async Task<IActionResult> AddTowns(string title)
		{
			_groupingOfficeRepository.Add(new GroupingOffice { Title = title });
			await _unitOfWork.SaveAsync();
			return RedirectToAction("ListTowns");
		}
		[HttpPost]
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		public async Task<IActionResult> DeleteTowns(int? id)
		{
			if (id.GetValueOrDefault(0) == 0)
				return RedirectToAction("ListTowns");
			var model = await _groupingOfficeRepository.GetById(id);
			_groupingOfficeRepository.Delete(model);
			await _unitOfWork.SaveAsync();
			return RedirectToAction("ListTowns");
		}
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		public async Task<IActionResult> EditListTowns(int? id)
		{
			if (id == null)
				return RedirectToAction("ListTowns");
			var townGroup = await _groupingOfficeRepository.GetById(id);
			return Json(townGroup);
		}
		//Update
		[HttpPost]
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		public async Task<IActionResult> UpdateTowns(int? id, string title)
		{
			if (id == null)
				return RedirectToAction("ListTowns");
			var townGroup = await _groupingOfficeRepository.GetById(id);
			townGroup.Title = title;
			_groupingOfficeRepository.Update(townGroup);
			await _unitOfWork.SaveAsync();
			return RedirectToAction("ListTowns");
		}
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		public async Task<IActionResult> DeleteTown(int? id)
		{
			if (id == null)
				return RedirectToAction("ListTowns");
			_groupingOfficeRepository.Delete(await _groupingOfficeRepository.GetById(id));
			await _unitOfWork.SaveAsync();
			return RedirectToAction("ListTowns");
		}
		[Authorize]
		[Permission]
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		[DisplayName("نمایش تمام پیام ها")]
		//[UserLogMessageAttribute]
		public async Task<IActionResult> ShowAllBroadCast()
		{
			try
			{
				//Admin
				if (User.IsInRole("admin"))
				{
					int countUnRead = await _referralBroadCastRepository.CountUnRead5DayLast();
					ViewBag.CountAdameMoshahede = countUnRead;

					var model = await _broadCastRepository.ListAllBroadCast();

					return View(model);
				}
				// !Admin
				else
				{
					var listBroadCast = new List<BroadCast>();
					var model = (await _broadCastRepository.ListAllBroadCast()).Where(w => w.UserNameSender == User.Identity.Name).ToList();
					listBroadCast.AddRange(model);
					var userList = (await _referralBroadCastRepository.GetListtByIdDstUser(User.GetUserId())).Select(s => s.BroadCast);
					listBroadCast.AddRange(userList);

					return View(listBroadCast);
				}
			}
			catch (Exception ex)
			{

				return View(new List<BroadCast>());
			}
		}
		[Authorize]
		[Permission]
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		[DisplayName("نمایش پیام های خوانده نشده")]
		public async Task<IActionResult> ShowAllUnReadMessage()
		{
			if (User.IsInRole("admin"))
			{
				var model = await _referralBroadCastRepository.GetListAllTakhir();
				return View(model);
			}
			return View(null);
		}
		[HttpGet]
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		public async Task<IActionResult> GetDataReferralBroadCasts(int id)
		{
			var model = await _referralBroadCastRepository.GetListtById(id);
			var broadCastViewModel = new List<ReferralBroadCastViewModel>();
			foreach (var pc in model)
			{
				broadCastViewModel.Add(new ReferralBroadCastViewModel
				{
					BroadCast = new BroadCastViewModel { UserNameSender = pc.BroadCast.UserNameSender, CreateDate = pc.BroadCast.CreateDate.Value, Text = pc.BroadCast.Text },
					ApplicationUser = pc.ApplicationUser,
					Status = (ReferralStatusBroadCastEnumViewModel)pc.Status,
					DstUserID = (int)pc.DstUserID,
					DeadLine = pc.DeadLine,
					//SrcUser= (int)pc.SrcUser,
					IsImmediate = pc.IsImmediate,
					ActionDescription = pc.ActionDescription,
				});
			}
			return Json(broadCastViewModel.OrderByDescending(c => c.StatusString));
		}
		[HttpGet]
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		public async Task<IActionResult> UsersInfo()
		{

			string roleName = _configuration.GetSection("Separ").GetSection("Role").Value;
			//لیست کاربرانی که رول آنها سپر باشد
			var separ = await _userManager.GetUsersInRoleAsync(roleName);


			//var result = _userManager.Users.ToList();
			return Json(separ);
		}
		[HttpGet]
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		public async Task<IActionResult> UsersInfoExist(int id)
		{
			var result = await _groupingOfficeRepository.GetById(id);
			return Json(result);
		}
		[HttpPost]
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		public async Task<IActionResult> AddUser(int protectionMemberId, int[] states)
		{
			try
			{
				GroupingOfficeMember protectionOfficeMember = new();
				protectionOfficeMember.GroupingOfficeId = protectionMemberId;

				foreach (var item in states)
				{
					var add = new GroupingOfficeMember()
					{
						GroupingOfficeId = protectionMemberId,
						ApplicationUserId = item,
					};
					_groupingOfficeMemberRepository.Add(add);
					await _unitOfWork.SaveAsync();
				}
				return RedirectToAction("ListTowns");
			}
			catch (System.Exception)
			{
				return RedirectToAction("ListTowns", new { message = "این کاربر قبلاً به اداره دیگری اضافه گردیده است" });
			}

		}
		//GroupingOffice
		//777 --> begin
		[Authorize]
		[Permission]
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		[DisplayName("لیست گروه های پیام")]
		public async Task<IActionResult> ListTownsGroup(/*string message = ""*/)
		{
			//ViewBag.Message = message;
			var model = await _groupingOfficeRepository.GetGroupingOfficeAll();
			return View(model);
		}
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		//AddTownsGroup
		public async Task<IActionResult> AddTownsGroup(string title)
		{
			_groupingOfficeRepository.Add(new GroupingOffice { Title = title });
			await _unitOfWork.SaveAsync();
			return RedirectToAction("ListTownsGroup");
		}
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		public async Task<IActionResult> EditListTownsGroup(int? id)
		{
			if (id == null)
				return RedirectToAction("ListTownsGroup");
			var townGroup = await _groupingOfficeRepository.GetById(id);
			return Json(townGroup);
		}
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		//Update
		[HttpPost]
		public async Task<IActionResult> UpdateTownsGroup(int? id, string title)
		{
			if (id == null)
				return RedirectToAction("ListTownsGroup");
			var townGroup = await _groupingOfficeRepository.GetById(id);
			townGroup.Title = title;
			_groupingOfficeRepository.Update(townGroup);
			await _unitOfWork.SaveAsync();
			return RedirectToAction("ListTownsGroup");
		}
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		public async Task<IActionResult> DeleteTownGroup(int? id)
		{
			if (id == null)
				return RedirectToAction("ListTownsGroup");
			_groupingOfficeRepository.Delete(await _groupingOfficeRepository.GetById(id));
			await _unitOfWork.SaveAsync();
			return RedirectToAction("ListTownsGroup");
		}
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		public async Task<IActionResult> AddUserGroup(int protectionMemberId, int[] states)
		{
			try
			{
				//ProtectionOfficeMember protectionOfficeMember = new();
				GroupingOfficeMember groupingOfficeMember = new();
				//protectionOfficeMember.ProtectionOfficeId = protectionMemberId;
				groupingOfficeMember.GroupingOfficeId = protectionMemberId;

				foreach (var item in states)
				{
					var add = new GroupingOfficeMember()
					{
						GroupingOfficeId = protectionMemberId,
						ApplicationUserId = item,
					};
					//_protectionOfficeMemberRepository.Add(add);
					_groupingOfficeMemberRepository.Add(add);
					await _unitOfWork.SaveAsync();
				}
				return RedirectToAction("ListTownsGroup");
			}
			catch (System.Exception)
			{
				return RedirectToAction("ListTownsGroup", new { message = "این کاربر قبلاً به اداره دیگری اضافه گردیده است" });
			}

		}
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		[HttpGet]
		public async Task<IActionResult> UsersInfoGroup()
		{
			string roleName = _configuration.GetSection("Separ").GetSection("Role").Value;
			//لیست کاربرانی که رول آنها سپر باشد
			var separ = await _userManager.GetUsersInRoleAsync(roleName);


			//var result = _userManager.Users.ToList();
			return Json(separ);
		}
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		[HttpGet]
		public async Task<IActionResult> UsersInfoExistGroup(int id)
		{
			var result = await _groupingOfficeMemberRepository.GetById(id);
			return Json(result);
		}

		//777 --> End

		//888 --> begin------------------------------------------------------------------------


		[Authorize]
		[Permission]
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		[DisplayName("لیست گروه های پیام")]
		public async Task<IActionResult> ListTownsSubject(/*string message = ""*/)
		{
			//ViewBag.Message = message;
			var model = await _protectionOfficeRepositorySubject.GetProtectionOfficeAll(); ;
			return View(model);
		}
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		//AddTownsGroup
		public async Task<IActionResult> AddTownsSubject(string title)
		{
			_protectionOfficeRepositorySubject.Add(new ProtectionOffice { Title = title });
			await _unitOfWork.SaveAsync();
			return RedirectToAction("ListTownsSubject");
		}
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		public async Task<IActionResult> EditListTownsSubject(int? id)
		{
			if (id == null)
				return RedirectToAction("ListTownsSubject");
			var townGroup = await _protectionOfficeRepositorySubject.GetById(id);
			return Json(townGroup);
		}
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		//Update
		[HttpPost]
		public async Task<IActionResult> UpdateTownsSubject(int? id, string title)
		{
			if (id == null)
				return RedirectToAction("ListTownsSubject");
			var townGroup = await _protectionOfficeRepositorySubject.GetById(id);
			townGroup.Title = title;
			_protectionOfficeRepositorySubject.Update(townGroup);
			await _unitOfWork.SaveAsync();
			return RedirectToAction("ListTownsSubject");
		}
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		public async Task<IActionResult> DeleteTownSubject(int? id)
		{
			if (id == null)
				return RedirectToAction("ListTownsSubject");
			_protectionOfficeRepositorySubject.Delete(await _protectionOfficeRepositorySubject.GetById(id));
			await _unitOfWork.SaveAsync();
			return RedirectToAction("ListTownsSubject");
		}
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		public async Task<IActionResult> AddUserSubject(int protectionMemberId, int[] states)
		{
			try
			{
				ProtectionOfficeMember protectionOfficeMember = new();
				//GroupingOfficeMember groupingOfficeMember = new();
				protectionOfficeMember.ProtectionOfficeId = protectionMemberId;
				//groupingOfficeMember.GroupingOfficeId = protectionMemberId;

				foreach (var item in states)
				{
					var add = new ProtectionOfficeMember()//GroupingOfficeMember()
					{
						//GroupingOfficeId = protectionMemberId,
						ProtectionOfficeId = protectionMemberId,
						ApplicationUserId = item,
					};
					//_protectionOfficeMemberRepository.Add(add);
					_protectionOfficeMemberRepositorySubject.Add(add);
					await _unitOfWork.SaveAsync();
				}
				return RedirectToAction("ListTownsSubject");
			}
			catch (System.Exception)
			{
				return RedirectToAction("ListTownsSubject", new { message = "این کاربر قبلاً به اداره دیگری اضافه گردیده است" });
			}

		}
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		[HttpGet]
		public async Task<IActionResult> UsersInfoSubject()
		{
			string roleName = _configuration.GetSection("Separ").GetSection("Role").Value;
			//لیست کاربرانی که رول آنها سپر باشد
			var separ = await _userManager.GetUsersInRoleAsync(roleName);


			//var result = _userManager.Users.ToList();
			return Json(separ);
		}
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		[HttpGet]
		public async Task<IActionResult> UsersInfoExistSubject(int id)
		{
			var result = await _protectionOfficeMemberRepositorySubject.GetById(id);
			return Json(result);
		}


		//88 --> end----------------------------------------------------------------------------
		[Permission]
		[Authorize(Roles = "admin,developer")]
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		[HttpGet]
		[DisplayName("لیست نمایشی لاگ")]
		public IActionResult UserLogMessage()
		{
			return View();
		}
		public IActionResult GetUserLogMessageData(string models)
		{
			var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);
			var model = (_userLogMessageRepository.GetList(request));
			return Json(model);
		}
		//[HttpGet]
		[Permission]
		[DisplayName("مشاهده جزییات")]
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		public async Task<IActionResult> ShowDetails(int id)
		{
			var result = await _referralBroadCastRepository.GetReferralBroadCastFromBroadCast(id);
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

			return PartialView("_Details", model);
		}
		//ActionEghdam
		[HttpPost]
		[Permission]
		[ServiceFilter(typeof(UserLogMessageAttribute))]
		public async Task<string> ActionEghdam(int broadCastId, string actionDescription)
		{
			var result = await _referralBroadCastRepository.GetByIdUpdate(broadCastId);
			result.ActionDescription = actionDescription;
			result.Status = ReferralStatusBroadCastEnum.EghdamShode;
			_referralBroadCastRepository.Update(result);
			await _unitOfWork.SaveAsync();
			//return RedirectToAction("ShowAllBroadCast");
			return "اقدام مورد نظر با موفقیت ثبت گردید";
		}
	}
}