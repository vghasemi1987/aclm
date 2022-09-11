using ApplicationCommon;
using DomainContracts.AccessibilityAggregate;
using DomainContracts.AccessRangeAggregate;
using DomainContracts.ApplicationUserAggregate;
using DomainContracts.BankBranchAggregate;
using DomainContracts.Commons;
using DomainContracts.DepartmentAggregate;
using DomainContracts.DutyPositionAggregate;
using DomainContracts.PositionAggregate;
using DomainEntities.ApplicationUserAggregate;
using DomainEntities.Commons;
using DomainEntities.EmailAggregate;
using KendoHelper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Core.AccessRanges.ViewModels;
using Web.Core.UserManagement.ViewModels;
using Web.Extensions;
using Web.Extensions.Attributes;
using Tools = ApplicationCommon.Tools;
using System.Windows;
using System.Reflection;

namespace Web.Core.UserManagement
{
	[Authorize]
	[DisplayName("مدیریت کاربران")]
	public class UserManagementController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly RoleManager<ApplicationRole> _roleManager;
		private readonly IUserValidator<ApplicationUser> _userValidator;
		private readonly IEmailService _emailService;
		private readonly /*IWebHostEnvironment*/IWebHostEnvironment _env;

		private readonly IDepartmentRepository _departmentRepository;
		private readonly IFileService _fileService;
		private readonly IPositionRepository _positionRepository;
		private readonly IBankBranchRepository _bankBranchRepository;
		private readonly IDutyPositionRepository _dutyPositionRepository;
		private readonly IAccessibilityRepository _accessibilityRepository;
		private readonly IAccessRangeHeaderRepository _accessRangeHeaderRepository;
		private readonly IApplicationUserAccessRageHeaderRepository _applicationUserAccessRageHeaderRepository;
		private readonly IUnitOfWork _unitOfWork;
		public UserManagementController(UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			/*IWebHostEnvironment*/IWebHostEnvironment env,
			RoleManager<ApplicationRole> roleManager,
			IUserValidator<ApplicationUser> userValidator,
			IFileService fileService,
			IDepartmentRepository departmentRepository,
			IEmailService emailService,
			IPositionRepository positionRepository,
			IBankBranchRepository bankBranchRepository,
			IAccessibilityRepository accessibilityRepository,
			IAccessRangeHeaderRepository accessRangeHeaderRepository,
			IApplicationUserAccessRageHeaderRepository applicationUserAccessRageHeaderRepository,
			IDutyPositionRepository dutyPositionRepository,
			IUnitOfWork unitOfWork)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
			_userValidator = userValidator;
			_fileService = fileService;
			_departmentRepository = departmentRepository;
			_emailService = emailService;
			_positionRepository = positionRepository;
			_bankBranchRepository = bankBranchRepository;
			_accessibilityRepository = accessibilityRepository;
			_accessRangeHeaderRepository = accessRangeHeaderRepository;
			_applicationUserAccessRageHeaderRepository = applicationUserAccessRageHeaderRepository;
			_dutyPositionRepository = dutyPositionRepository;
			_unitOfWork = unitOfWork;
			_env = env;
		}

		[HttpGet]
		[Permission]
		[DisplayName("لیست نمایشی")]
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		[Permission]
		[DisplayName("لیست نمایشی کاربران")]
		public IActionResult IndexUsers()
		{
			var list = _userManager.Users
				.AsNoTracking().Select(o => new UserDetailViewModel
				{
					Id = o.Id,
					Picture = o.Picture ?? "no_image.png",
					UserName = o.UserName,
					Name = o.Name,
					RegDateTime = o.RegDateTime,
					Roles = string.Join(",", o.ApplicationUserRoles.Select(x => x.ApplicationRole.Name).ToArray()),
					LockoutState = o.LockoutEnd.HasValue,
					DepartmentName = o.Department.Name,
					PositionName = o.Position.Title,
					BankBranchName = o.BankBranch.Title,
					DutyPositionId = o.DutyPositionId.GetValueOrDefault(),
					DutyPositionTitle = o.DutyPosition.Title
				});
			return View(list);
		}
		[HttpGet]
		public IActionResult GetRecords(string models)
		{
			var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);
			var list = _userManager.Users
				.AsNoTracking()
				.Select(o => new UserDetailViewModel
				{
					Id = o.Id,
					Picture = o.Picture ?? "no_image.png",
					UserName = o.UserName,
					Name = o.Name,
					FirstName = o.FirstName,
					LastName = o.LastName,
					RegDateTime = o.RegDateTime,
					Roles = string.Join(",", o.ApplicationUserRoles.Select(x => x.ApplicationRole.Name).ToArray()),
					LockoutState = o.LockoutEnd.HasValue,
					DepartmentName = o.Department.Name,
					PositionName = o.Position.Title,
					BankBranchName = o.BankBranch.Title,
					DutyPositionId = o.DutyPositionId.GetValueOrDefault(),
					DutyPositionTitle = o.DutyPosition.Title
				})
				.ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);

			return Json(list);
		}

		[HttpPost]
		public async Task<JsonResult> ValidateUsername(string username, int userid)
		{
			var result = await _userValidator.ValidateAsync(
				_userManager, new ApplicationUser { UserName = username, Id = userid });
			return Json(result.Succeeded ? "true" : null);
		}

		private void AddErrors(IdentityResult result)
		{
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError("", error.Description);
			}
		}

		[HttpGet]
		[Permission]
		[DisplayName("مشاهده جزئیات")]
		public async Task<IActionResult> GetDetail(int? id)
		{
			var userData = new UserFormViewModel
			{
				RolesList = new SelectList(_roleManager.Roles.ToList(), nameof(ApplicationRole.Name), nameof(ApplicationRole.Name)),
				DepartmentList = new SelectList(await _departmentRepository.GetDropDownList(), nameof(DropDownDto.Id),
												 nameof(DropDownDto.Title)),
				PositionList = new SelectList(await _positionRepository.GetDropDownList(), nameof(DropDownDto.Id),
					nameof(DropDownDto.Title)),
				BankBranchList = new SelectList(await _bankBranchRepository.GetDropDownBranchHeadsList(), nameof(DropDownDto.Id),
					nameof(DropDownDto.Title)),
				DutyPositionList = new SelectList(await _dutyPositionRepository.GetDropDownList(), nameof(DropDownDto.Id),
					nameof(DropDownDto.Title)),
				////roleUsers  
				RoleIds = new SelectList(_roleManager.Roles.ToList(), nameof(RoleDto.Id), nameof(RoleDto.Name)).Select(obj => int.Parse(obj.Value)).ToList(),
			};

			var user = await _userManager.Users
				.Where(o => o.Id.Equals(id))
				.Include(o => o.ApplicationUserRoles)
				.ThenInclude(o => o.ApplicationRole)
				.FirstOrDefaultAsync();

			userData.SelectedRoles = new List<RoleJsonCheckBoxDto>();
			///////////////////
			foreach (int item in userData.RoleIds)
			{
				var Roles = await _roleManager.FindByIdAsync(item.ToString());
				userData.SelectedRoles.Add(new RoleJsonCheckBoxDto()
				{
					Id = Roles.Id,
					Text = Roles?.Name,
					Checked =


				user != null ? user.ApplicationUserRoles.Select(o => o.ApplicationRole).Select(obj => obj.Id).ToList().Contains(Roles.Id) : false,
				});
			}

			if (!id.HasValue)
			{
				return PartialView("_Detail", userData);
			}


			if (user == null)
			{
				return NotFound();
			}
			//var userRoles = await _userManager.GetRolesAsync(user);
			//var model = new UserFormViewModel();
			userData.PersonnelCode = user.PersonnelCode;
			userData.FirstName = user.FirstName;
			userData.LastName = user.LastName;
			userData.PhoneNumber = user.PhoneNumber;
			userData.ExpertPerformance = user.ExpertPerformance;
			userData.ExpertPerformanceToken = user.ExpertPerformanceToken;
			userData.Qualifications = user.Qualifications;
			userData.QualificationsToken = user.QualificationsToken;

			userData.Qualifications_Second = user.Qualifications_Second;
			userData.QualificationsToken_Second = user.QualificationsToken_Second;

			userData.Picture = user.Picture;
			userData.UserId = user.Id;
			userData.UserName = user.UserName;
			//userData.SelectedRole = user.ApplicationUserRoles.FirstOrDefault()?.ApplicationRole.Name;

			userData.DepartmentId = user.DepartmentId;
			userData.LockState = !user.LockoutEnd.HasValue;
			userData.PositiontId = user.PositionId.GetValueOrDefault();
			userData.BankBranchId = user.BankBranchId.GetValueOrDefault();
			userData.DutyPositionId = user.DutyPositionId.GetValueOrDefault();


			//userData.SelectedRoles = new List<RoleJsonCheckBoxDto>();
			///////////////////
			//foreach (int item in userData.RoleIds)
			//{
			//	var Roles = await _roleManager.FindByIdAsync(item.ToString());
			//	userData.SelectedRoles.Add(new RoleJsonCheckBoxDto() { Id = Roles.Id, Text = Roles.Name, Checked = user.ApplicationUserRoles.Select(o => o.ApplicationRole).Select(obj => obj.Id).ToList().Contains(Roles.Id) });

			//}


			return PartialView("_Detail", userData);
		}

		[HttpGet]
		[Permission]
		[DisplayName("تخصیص محدوده دسترسی")]
		public IActionResult AccessRange(int? id)
		{
			var model = new UserAccessRangeViewModel
			{
				UserId = id.Value,
				UserName = User.GetUserFullName(),
				AccessRangeInfos = _accessRangeHeaderRepository.GetDropDownList().Select(q => new AccessRangeInfo
				{
					Id = q.Id,
					Title = q.Title,
				}).ToList(),
				SelectList = _applicationUserAccessRageHeaderRepository.GetListByUserId(id.Value).
								Select(q => q.AccessRangeHeaderId.ToString()).ToList()
			};
			return PartialView("_AccessRange", model);
		}
		[HttpPost]
		public IActionResult AddAccessRange(UserAccessRangeViewModel model)
		{
			var insertedList = _applicationUserAccessRageHeaderRepository.GetListByUserId(model.UserId).
								Select(q => q.AccessRangeHeaderId.ToString()).ToList();

			var diffList = model.SelectList.Where(x => !insertedList.Contains(x)).ToList();
			foreach (var item in diffList)
			{
				var userAccessHeader = new ApplicationUserAccessRageHeader
				{
					ApplicationUserId = model.UserId,
					AccessRangeHeaderId = int.Parse(item),

				};
				_applicationUserAccessRageHeaderRepository.Add(userAccessHeader);
			}

			_unitOfWork.Save();
			return Json(new
			{
				Message = Message.Show("محدوده دسترسی با موفقیت ثبت شد...", MessageType.Success),
				RefreshGrid = true
			});
		}

		[HttpGet]
		[Permission]
		[DisplayName("ویرایش پروفایل")]
		public async Task<IActionResult> UpdateProfile(string id)
		{
			var user = await _userManager.Users
				.Where(o => o.UserName.Equals(id))
				.FirstOrDefaultAsync();

			if (user == null)
			{
				return NotFound();
			}
			var model = new UpdateProfileViewModel
			{
				Email = user.Email,
				//PersonnelCode = user.PersonnelCode,
				FirstName = user.FirstName,
				LastName = user.LastName,
				PhoneNumber = user.PhoneNumber,
				Picture = user.Picture,
				UserId = user.Id,
				UserName = user.UserName,
				//OrganizationList = new SelectList(await _organizationalChartService.GetAllAsync(), nameof(OrganizationalChart.Id),
				//    nameof(OrganizationalChart.Title), user.OrganizationalChartId)
			};
			return PartialView("_UpdateProfile", model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		[Permission]
		[DisplayName("ثبت")]
		public async Task<IActionResult> AddDetail(UserFormViewModel model)
		{
			if (model.PostedFile != null)
			{
				model.Picture = model.Picture ?? Tools.NewFileName(model.PostedFile.FileName);
				var path = $"{_env.WebRootPath}\\uploads\\user-image\\{model.Picture}";
				await _fileService.SaveFileToServerAsync(path, model.PostedFile);
			}

			var user = new ApplicationUser
			{
				//Email = model.Email,
				PersonnelCode = model.PersonnelCode,
				FirstName = model.FirstName,
				LastName = model.LastName,
				PhoneNumber = model.PhoneNumber,
				ExpertPerformance = model.ExpertPerformance,
				ExpertPerformanceToken = model.ExpertPerformanceToken,
				Qualifications = model.Qualifications,
				QualificationsToken = model.QualificationsToken,

				Qualifications_Second = model.Qualifications_Second,
				QualificationsToken_Second = model.QualificationsToken_Second,

				Picture = model.Picture,
				UserName = model.UserName,
				DepartmentId = model.DepartmentId,
				PositionId = model.PositiontId,
				BankBranchId = model.BankBranchId,
				DutyPositionId = model.DutyPositionId,
				LockoutEnd = model.LockState.GetValueOrDefault() ? (DateTimeOffset?)null : DateTimeOffset.MaxValue
			};
			var result = await _userManager.CreateAsync(user, model.Password);
			if (!result.Succeeded)
			{
				AddErrors(result);
				return Json(new
				{
					Message = Message.Show(ModelState.GetErrors(), MessageType.Warning)
				});
			}

			foreach (var item in model.SelectedRoles.Where(q => q.Checked == true))
			{

				await _userManager.AddToRoleAsync(user, item.Text);
			}
			return Json(new
			{
				Message = Message.Show("اطلاعات کاربر با موفقیت ثبت شد...", MessageType.Success),
				RefreshGrid = true
			});
		}

		[HttpPost, ValidateAntiForgeryToken]
		[Permission]
		[DisplayName("ویرایش")]
		public async Task<IActionResult> EditDetail(UserFormViewModel model)
		{
			if (model.PostedFile != null)
			{
				model.Picture = model.Picture ?? Tools.NewFileName(model.PostedFile.FileName);
				var path = $"{_env.WebRootPath}\\uploads\\user-image\\{model.Picture}";
				await _fileService.SaveFileToServerAsync(path, model.PostedFile);
			}

			var user = await _userManager.FindByIdAsync(model.UserId.ToString());
			//user.Email = model.Email;
			user.PersonnelCode = model.PersonnelCode;
			user.FirstName = model.FirstName;
			user.LastName = model.LastName;
			user.PhoneNumber = model.PhoneNumber;
			user.ExpertPerformance = model.ExpertPerformance;
			user.ExpertPerformanceToken = model.ExpertPerformanceToken;
			user.Qualifications = model.Qualifications;
			user.QualificationsToken = model.QualificationsToken;

			user.Qualifications_Second = model.Qualifications_Second;
			user.QualificationsToken_Second = model.QualificationsToken_Second;

			user.Picture = model.Picture;
			user.DepartmentId = model.DepartmentId;
			user.LockoutEnd = model.LockState.GetValueOrDefault() ? (DateTimeOffset?)null : DateTimeOffset.MaxValue;
			user.PositionId = model.PositiontId;
			user.BankBranchId = model.BankBranchId;
			user.DutyPositionId = model.DutyPositionId;
			if (!string.IsNullOrEmpty(model.Password))
			{
				user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
			}

			var result = await _userManager.UpdateAsync(user);
			if (!result.Succeeded)
			{
				AddErrors(result);
				return Json(new
				{
					Message = Message.Show(ModelState.GetErrors(), MessageType.Warning)
				});
			}

			var roles = await _userManager.GetRolesAsync(user);
			await _userManager.RemoveFromRolesAsync(user, roles.ToArray());
			foreach (var item in model.SelectedRoles.Where(q => q.Checked == true))
			{
				await _userManager.AddToRoleAsync(user, item.Text);
			}
			return Json(new
			{
				Message = Message.Show("اطلاعات کاربر با موفقیت ثبت شد...", MessageType.Success),
				RefreshGrid = true
			});
		}


		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> UpdateProfile(UpdateProfileViewModel model)
		{
			if (model.PostedFile != null)
			{
				model.Picture = model.Picture ?? Tools.NewFileName(model.PostedFile.FileName);
				var path = $"{_env.WebRootPath}\\uploads\\user-image\\{model.Picture}";
				await _fileService.SaveFileToServerAsync(path, model.PostedFile);
			}

			var user = await _userManager.FindByIdAsync(model.UserId.ToString());
			//user.Email = model.Email;
			//if (model.Gender != null) user.Gender = model.Gender.Value;
			user.FirstName = model.FirstName;
			user.LastName = model.LastName;
			user.PhoneNumber = model.PhoneNumber;
			user.Picture = model.Picture;

			var result = await _userManager.UpdateAsync(user);
			if (result.Succeeded)
				return Json(new
				{
					Message = Message.Show("اطلاعات شما با موفقیت ویرایش شد...", MessageType.Success),
					RefreshGrid = true
				});
			AddErrors(result);
			return Json(new
			{
				Message = Message.Show(ModelState.GetErrors(), MessageType.Warning)
			});
		}

		[HttpPost]
		[Permission]
		[DisplayName("حذف")]
		public async Task<IActionResult> DeleteRows(List<int> ids)
		{
			if (!ids.Any()) return Json(false);
			//var pictureList = new List<string>();
			var usersList = _userManager.Users.Where(o => ids.Contains(o.Id));

			foreach (var user in usersList)
			{
				//await _userManager.SetLockoutEnabledAsync(user, true);
				await _userManager.SetLockoutEndDateAsync(user, DateTime.MaxValue);
				//pictureList.Add(user.Picture);
				await _userManager.SetLockoutEnabledAsync(user, true);
			}

			//if (pictureList.Any())
			//{
			//    _fileService.DeleteFileListFromServer(pictureList, _env + @"uploads\user-image\");
			//}
			return Ok();
		}

		[AllowAnonymous]
		public async Task<IActionResult> Signin(string returnUrl = null)
		{
			await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
			ViewData["ReturnUrl"] = returnUrl;
			if (User.Identity.IsAuthenticated)
				return RedirectToAction("Index", "Home");
			return View();
		}

		[HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
		//[ServiceFilter(typeof(UserActivityLogAttribute))]
		[DisplayName("ورود")]
		public async Task<IActionResult> Signin(SigninViewModel model)
		{
			if (HttpContext.Session.GetString(key: "Code").ToLower() == model.Captcha?.ToLower())
			{
				var user = _userManager.Users
				.Include(o => o.ApplicationUserRoles)
				.FirstOrDefault(o => o.UserName.Equals(model.UserName));
				if (user == null)
				{
					return Json(new { Message = Message.Show("نام کاربری یا رمز عبور صحیح نمی باشد", MessageType.Error) });
				}

				var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);

				if (result.Succeeded)
				{
					ViewData["UserId"] = user.Id;

					var menus = user.ApplicationUserRoles.ToList().Select(q => q.ApplicationRole.PanelMenu);
					List<MenuGenerataionViewModel> menulst = new List<MenuGenerataionViewModel>();
					foreach (var item in menus)
					{
						try
						{
							var menu = JsonConvert.DeserializeObject<List<MenuGenerataionViewModel>>(item);
							menulst.AddRange(menu);
						}
						catch (Exception ex) { }
					}


					var dataResult = new
					{
						Redirect = Url.IsLocalUrl(model.ReturnUrl) && !string.IsNullOrEmpty(model.ReturnUrl) ? model.ReturnUrl : Url.Action("Index", "Home"),
						Success = true,
						//PanelMenu = user.ApplicationUserRoles.FirstOrDefault()?.ApplicationRole.PanelMenu,
						PanelMenu = JsonConvert.SerializeObject(menulst.Distinct()),
						ExpertPerformance = user.ExpertPerformance,
						ExpertPerformanceToken = user.ExpertPerformance ? user.ExpertPerformanceToken : "",
						Qualifications = user.Qualifications,
						QualificationsToken = user.Qualifications ? user.QualificationsToken : "",
						Qualifications_Second = user.Qualifications_Second,
						QualificationsToken_Second = user.Qualifications_Second ? user.QualificationsToken_Second : ""
					};

					return Json(dataResult);
				}
				if (result.IsLockedOut)
				{
					return Json(new
					{
						Redirect = Url.Action(nameof(Lockout)),
						Success = true
					});
				}
				ModelState.AddModelError(string.Empty, "نام کاربری یا رمز عبور صحیح نمی باشد!");
				return Json(new { Message = Message.Show(ModelState.GetErrors(), MessageType.Error) });
			}
			else
			{
				ViewBag.CaptchaError = "error";
				ModelState.AddModelError(string.Empty, "عبارت امنیتی صحیح نمی باشد");
				return Json(new { Message = Message.Show(ModelState.GetErrors(), MessageType.Error) });

			}
		}

		[AllowAnonymous]
		public IActionResult Lockout()
		{
			return View();
		}

		public async Task<IActionResult> Signout()
		{
			_accessibilityRepository.DeleteTemp(User.GetUserId());
			await _signInManager.SignOutAsync();

			return RedirectToAction(nameof(Signin), "UserManagement");
		}

		[AllowAnonymous]
		public IActionResult ForgotPassword()
		{
			return View();
		}

		[HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
		public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null) /*|| !(await UserManager.IsEmailConfirmedAsync(user.Id)))*/
			{
				return Json(Message.Show("پست الکترونیکی وارد شده یافت نشد!", MessageType.Error));
			}

			var code = await _userManager.GeneratePasswordResetTokenAsync(user);
			await _emailService.SendResetPasswordAsync(user.Email, Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme));
			return Json(Message.Show("برای تغییر رمز عبور، پست الکترونیکی خور را بررسی کنید...", MessageType.Success));
		}

		[AllowAnonymous]
		public IActionResult ResetPassword(string code = null)
		{
			if (code == null)
			{
				throw new ApplicationException("A code must be supplied for password reset.");
			}
			var model = new ResetPasswordViewModel { Code = code };
			return View(model);
		}

		[HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null)
			{
				// Don't reveal that the user does not exist
				return Json(Message.Show("پست الکترونیکی یافت نشد!", MessageType.Error));
			}
			var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
			if (result.Succeeded)
			{
				return Json(new
				{
					Message = Message.Show("رمز عبور با موفقیت تغییر یافت"),
					Redirect = Url.Action("Signin", "UserManagement"),
					Success = true
				});
			}
			AddErrors(result);
			return Json(Message.Show(ModelState.GetErrors(), MessageType.Warning));
		}

		public IActionResult ChangePassword()
		{
			return PartialView("_ChangePassword");
		}
		[HttpPost]
		public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
		{

			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
			}
			var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
			if (!result.Succeeded)
			{
				AddErrors(result);
				return Json(new
				{
					Message = Message.Show(ModelState.GetErrors(), MessageType.Warning)
				});
			}
			await _signInManager.SignInAsync(user, isPersistent: false);
			return Json(new
			{
				Message = Message.Show("رمز عبور شما با موفقیت تغییر کرد...", MessageType.Success),
				Success = true
			});
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> ConfirmEmail(string userId, string code)
		{
			if (userId == null || code == null)
			{
				return RedirectToAction(nameof(SignIn));
			}
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				throw new ApplicationException($"Unable to load user with ID '{userId}'.");
			}
			var result = await _userManager.ConfirmEmailAsync(user, code);
			return View(result.Succeeded ? "ConfirmEmail" : "Error");
		}

		[Permission]
		[DisplayName("ورود به عنوان")]
		public async Task<IActionResult> ImpersonateUser()
		{
			var users = (await _userManager.Users.ToListAsync()).Where(o => o.Id != User.GetUserId());
			var model = new ImpersonateUserViewModel
			{
				Users = new SelectList(users,
					nameof(ApplicationUser.Id), nameof(ApplicationUser.Name))
			};
			return PartialView("_ImpersonateUser", model);
		}
		[HttpPost]
		[Permission]
		public async Task<IActionResult> ImpersonateUser(ImpersonateUserViewModel model)
		{
			var currentUserId = User.GetUserId();
			var impersonatedUser = await _userManager.FindByIdAsync(model.UserId);
			var userPrincipal = await _signInManager.CreateUserPrincipalAsync(impersonatedUser);
			userPrincipal.Identities.First().AddClaim(new Claim("OriginalUserId", currentUserId.ToString()));
			userPrincipal.Identities.First().AddClaim(new Claim("IsImpersonating", "true"));
			await _signInManager.SignOutAsync();
			await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, userPrincipal);
			return Json(new { Redirect = Url.Action(nameof(Index), "Home") });
		}

		public async Task<IActionResult> StopImpersonation()
		{
			if (!User.IsImpersonating())
			{
				throw new Exception("You are not impersonating now. Can't stop impersonation");
			}
			var originalUserId = User.FindFirst("OriginalUserId").Value;
			var originalUser = await _userManager.FindByIdAsync(originalUserId);
			await _signInManager.SignOutAsync();
			await _signInManager.SignInAsync(originalUser, isPersistent: true);
			return RedirectToAction("Index", "Home");
		}

		public async Task<IActionResult> UsersList()
		{
			var users = await _userManager.Users
				.Select(o => new { o.Name, o.UserName, Picture = o.Picture ?? "no_image.png" })
				.AsNoTracking()
				.ToArrayAsync();
			return Json(users.Select(user => $"{user.UserName},{user.Name},{user.Picture}").ToList());
		}



		////after data table  ////////////////////////////

		[HttpGet]
		[Permission]
		[DisplayName("لیست نمایشی")]
		public IActionResult Index02(string message = "")
		{
			ViewBag.Message = message;

			var model = _userManager.Users
				.AsNoTracking()
				.Select(o => new UserDetailViewModel
				{
					Id = o.Id,
					//Picture = o.Picture ?? "no_image.png",
					UserName = o.UserName,
					Name = o.Name,
					FirstName = o.FirstName,
					LastName = o.LastName,
					RegDateTime = o.RegDateTime,
					Roles = string.Join(",", o.ApplicationUserRoles.Select(x => x.ApplicationRole.Name).ToArray()),
					LockoutState = o.LockoutEnd.HasValue,
					DepartmentName = o.Department.Name,
					PositionName = o.Position.Title,
					BankBranchName = o.BankBranch.Title,
					DutyPositionId = o.DutyPositionId.GetValueOrDefault(),
					DutyPositionTitle = o.DutyPosition.Title,
				}).ToList();

			return View(model);
		}
		public async Task<IActionResult> GetDetailDataTable(int? id)
		{
			var userData = new UserFormViewModel
			{
				RolesList = new SelectList(_roleManager.Roles.ToList(), nameof(ApplicationRole.Name), nameof(ApplicationRole.Name)),
				DepartmentList = new SelectList(await _departmentRepository.GetDropDownList(), nameof(DropDownDto.Id),
												 nameof(DropDownDto.Title)),
				PositionList = new SelectList(await _positionRepository.GetDropDownList(), nameof(DropDownDto.Id),
					nameof(DropDownDto.Title)),
				BankBranchList = new SelectList(await _bankBranchRepository.GetDropDownBranchHeadsList(), nameof(DropDownDto.Id),
					nameof(DropDownDto.Title)),
				DutyPositionList = new SelectList(await _dutyPositionRepository.GetDropDownList(), nameof(DropDownDto.Id),
					nameof(DropDownDto.Title))
			};
			if (!id.HasValue)
			{
				return PartialView("_Detail02", userData);
			}
			var user = await _userManager.Users
				.Where(o => o.Id.Equals(id))
				.Include(o => o.ApplicationUserRoles)
				.ThenInclude(o => o.ApplicationRole)
				.FirstOrDefaultAsync();

			if (user == null)
			{
				return NotFound();
			}
			//var userRoles = await _userManager.GetRolesAsync(user);
			//var model = new UserFormViewModel();
			userData.PersonnelCode = user.PersonnelCode;
			userData.FirstName = user.FirstName;
			userData.LastName = user.LastName;
			userData.PhoneNumber = user.PhoneNumber;
			userData.ExpertPerformance = user.ExpertPerformance;
			userData.ExpertPerformanceToken = user.ExpertPerformanceToken;
			userData.Qualifications = user.Qualifications;
			userData.QualificationsToken = user.QualificationsToken;

			userData.Qualifications_Second = user.Qualifications_Second;
			userData.QualificationsToken_Second = user.QualificationsToken_Second;

			userData.Picture = user.Picture;
			userData.UserId = user.Id;
			userData.UserName = user.UserName;
			userData.SelectedRole = user.ApplicationUserRoles.FirstOrDefault()?.ApplicationRole.Name;
			userData.DepartmentId = user.DepartmentId;
			userData.LockState = !user.LockoutEnd.HasValue;
			userData.PositiontId = user.PositionId.GetValueOrDefault();
			userData.BankBranchId = user.BankBranchId.GetValueOrDefault();
			userData.DutyPositionId = user.DutyPositionId.GetValueOrDefault();

			return PartialView("_Detail02", userData);
		}

		[HttpPost, ValidateAntiForgeryToken]
		[Permission]
		[DisplayName("ثبت")]
		public async Task<IActionResult> AddDetail02(UserFormViewModel model)
		{
			if (model.PostedFile != null)
			{
				model.Picture = model.Picture ?? Tools.NewFileName(model.PostedFile.FileName);
				var path = $"{_env.WebRootPath}\\uploads\\user-image\\{model.Picture}";
				await _fileService.SaveFileToServerAsync(path, model.PostedFile);
			}

			var user = new ApplicationUser
			{
				//Email = model.Email,
				PersonnelCode = model.PersonnelCode,
				FirstName = model.FirstName,
				LastName = model.LastName,
				PhoneNumber = model.PhoneNumber,
				ExpertPerformance = model.ExpertPerformance,
				ExpertPerformanceToken = model.ExpertPerformanceToken,
				Qualifications = model.Qualifications,
				QualificationsToken = model.QualificationsToken,

				Qualifications_Second = model.Qualifications_Second,
				QualificationsToken_Second = model.QualificationsToken_Second,

				Picture = model.Picture,
				UserName = model.UserName,
				DepartmentId = model.DepartmentId,
				PositionId = model.PositiontId,
				BankBranchId = model.BankBranchId,
				DutyPositionId = model.DutyPositionId,
				LockoutEnd = model.LockState.GetValueOrDefault() ? (DateTimeOffset?)null : DateTimeOffset.MaxValue
			};
			var result = await _userManager.CreateAsync(user, model.Password);
			if (!result.Succeeded)
			{
				AddErrors(result);
				return RedirectToAction("Index02", new
				{
					message = ModelState.GetErrors()
				});
			}

			await _userManager.AddToRoleAsync(user, model.SelectedRole);
			return RedirectToAction("Index02", new
			{
				message = "اطلاعات کاربر با موفقیت ثبت شد..."
			});
		}

		[HttpPost, ValidateAntiForgeryToken]
		[Permission]
		[DisplayName("ویرایش")]
		public async Task<IActionResult> EditDetail02(UserFormViewModel model)
		{
			if (model.PostedFile != null)
			{
				model.Picture = model.Picture ?? Tools.NewFileName(model.PostedFile.FileName);
				var path = $"{_env.WebRootPath}\\uploads\\user-image\\{model.Picture}";
				await _fileService.SaveFileToServerAsync(path, model.PostedFile);
			}

			var user = await _userManager.FindByIdAsync(model.UserId.ToString());
			//user.Email = model.Email;
			user.PersonnelCode = model.PersonnelCode;
			user.FirstName = model.FirstName;
			user.LastName = model.LastName;
			user.PhoneNumber = model.PhoneNumber;
			user.ExpertPerformance = model.ExpertPerformance;
			user.ExpertPerformanceToken = model.ExpertPerformanceToken;
			user.Qualifications = model.Qualifications;
			user.QualificationsToken = model.QualificationsToken;

			user.Qualifications_Second = model.Qualifications_Second;
			user.QualificationsToken_Second = model.QualificationsToken_Second;

			user.Picture = model.Picture;
			user.DepartmentId = model.DepartmentId;
			user.LockoutEnd = model.LockState.GetValueOrDefault() ? (DateTimeOffset?)null : DateTimeOffset.MaxValue;
			user.PositionId = model.PositiontId;
			user.BankBranchId = model.BankBranchId;
			user.DutyPositionId = model.DutyPositionId;
			if (!string.IsNullOrEmpty(model.Password))
			{
				user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
			}

			var result = await _userManager.UpdateAsync(user);
			if (!result.Succeeded)
			{
				AddErrors(result);
				return RedirectToAction("Index02", new
				{
					message = ModelState.GetErrors()
				});
			}

			var roles = await _userManager.GetRolesAsync(user);
			await _userManager.RemoveFromRolesAsync(user, roles.ToArray());
			await _userManager.AddToRoleAsync(user, model.SelectedRole);
			return RedirectToAction("Index02", new
			{
				message = "اطلاعات کاربر با موفقیت ثبت شد..."
			});
		}
		[HttpGet]
		[Permission]
		[DisplayName("تخصیص محدوده دسترسی")]
		public IActionResult AccessRange02(int? id)
		{
			var model = new UserAccessRangeViewModel
			{
				UserId = id.Value,
				UserName = User.GetUserFullName(),
				AccessRangeInfos = _accessRangeHeaderRepository.GetDropDownList().Select(q => new AccessRangeInfo
				{
					Id = q.Id,
					Title = q.Title,
				}).ToList(),
				SelectList = _applicationUserAccessRageHeaderRepository.GetListByUserId(id.Value).
								Select(q => q.AccessRangeHeaderId.ToString()).ToList()
			};
			return PartialView("_AccessRange02", model);
		}
		[HttpPost]
		public IActionResult AddAccessRange02(UserAccessRangeViewModel model)
		{
			var insertedList = _applicationUserAccessRageHeaderRepository.GetListByUserId(model.UserId).
								Select(q => q.AccessRangeHeaderId.ToString()).ToList();

			var diffList = model.SelectList.Where(x => !insertedList.Contains(x)).ToList();
			foreach (var item in diffList)
			{
				var userAccessHeader = new ApplicationUserAccessRageHeader
				{
					ApplicationUserId = model.UserId,
					AccessRangeHeaderId = int.Parse(item),

				};
				_applicationUserAccessRageHeaderRepository.Add(userAccessHeader);
			}

			_unitOfWork.Save();
			return RedirectToAction("Index02", new
			{
				message = "محدوده دسترسی با موفقیت ثبت شد"
			});
		}
		//if for repeat number slove
		[HttpGet, AllowAnonymous]
		public IActionResult Captcha()
		{
			string code = GenerateCoupon(6, new Random());

			HttpContext.Session.SetString("Code", code);

			Bitmap bitmap = new Bitmap(300, 150);

			var graphics = Graphics.FromImage(bitmap);

			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
			graphics.InterpolationMode = InterpolationMode.Low;


			graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, 700, 450));
			int x = new Random().Next(20, 40);
			int y = new Random().Next(20, 40);

			graphics.FillRectangle
				(new HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.Gray)
				, new Rectangle(0, 0, 700, 450));

			Dictionary<int, string> fonts = new Dictionary<int, string>();
			fonts.Add(3, "Arial");
			fonts.Add(0, "Tahoma");
			fonts.Add(1, "Verdana");
			fonts.Add(4, "Calibri");
			fonts.Add(2, "Algerian");
			fonts.Add(5, "Bernard MT Condensed");

			var numFont = new Random().Next(0, 5);

			Dictionary<int, FontStyle> styleFont = new Dictionary<int, FontStyle>();

			styleFont.Add(0, FontStyle.Bold);
			styleFont.Add(1, FontStyle.Italic);
			styleFont.Add(2, FontStyle.Strikeout);
			styleFont.Add(3, FontStyle.Underline);

			var numFontStyle = new Random().Next(0, 3);
			graphics.DrawString(code, new Font(fonts[numFont], 50, styleFont[numFontStyle])
				, Brushes.Gray,
				new PointF(x, y));



			MemoryStream stream = new MemoryStream();
			graphics.Save();
			bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
			return new FileContentResult(stream.GetBuffer(), "image/jpg");
		}
		public static string GenerateCoupon(int length, Random random)
		{
			string number_characters = "0897564231";
			string alpha_characters = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuYyZz";

			StringBuilder result = new StringBuilder(length);
			for (int i = 0; i < length; i++)
			{
				var turn = new Random().Next(0, 9);
				if (turn % 3 == 0)
					result.Append(number_characters[random.Next(number_characters.Length)]);
				else
					result.Append(alpha_characters[random.Next(alpha_characters.Length)]);

			}
			return result.ToString();
		}
	}
}