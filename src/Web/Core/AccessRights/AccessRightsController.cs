using ApplicationCommon;
using DomainContracts.AuthorityAggregate;
using DomainContracts.Commons;
using DomainEntities.ApplicationUserAggregate;
using Infrastructure.Data.ApplicationUserAggregate;
using KendoHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Core.Authorities.ViewModels;
using Web.Core.UserManagement.ViewModels;
using Web.Extensions;
using Web.Extensions.ControllerDiscovery;

namespace Web.Core.AccessRights
{
	//[Authorize]
	[DisplayName("مدیریت دسترسی ها")]
	public class AccessRightsController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly RoleManager<ApplicationRole> _roleManager;
		private readonly IRoleValidator<ApplicationRole> _roleValidator;
		private readonly ControllerDiscovery _controllerDiscovery;
		private readonly IAuthorityRepository _authorityRepository;
		private readonly IApplicationRoleAuthorityRepository _applicationRoleAuthorityRepository;
		private readonly IUnitOfWork _unitOfWork;

		public AccessRightsController(UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			RoleManager<ApplicationRole> roleManager,
			IRoleValidator<ApplicationRole> roleValidator,
			ControllerDiscovery controllerDiscovery,
			IAuthorityRepository authorityRepository,
			IApplicationRoleAuthorityRepository applicationRoleAuthorityRepository,
			IUnitOfWork unitOfWork)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
			_roleValidator = roleValidator;
			_controllerDiscovery = controllerDiscovery;
			_authorityRepository = authorityRepository;
			_applicationRoleAuthorityRepository = applicationRoleAuthorityRepository;
			_unitOfWork = unitOfWork;
		}

		//[Permission]
		[DisplayName("لیست نمایشی")]
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult RoleRecords(string models)
		{
			var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);

			var list = _roleManager.Roles.AsQueryable();

			return Json(list.AsQueryable().ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter));
		}

		[HttpPost]
		public async Task<JsonResult> ValidateRole(string name, int id)
		{
			var result = await _roleValidator.ValidateAsync(_roleManager, new ApplicationRole { Id = id, Name = name });
			return Json(result.Succeeded);
		}

		[HttpGet]
		//[Permission]
		[DisplayName("افزودن")]
		public IActionResult AddDetail()
		{
			var model = new RoleViewModel
			{
				ControllerInfos = _controllerDiscovery.GetControllers(),
				JsonMenu = GetDefaultMenu()
			};
			return PartialView("_Detail", model);
		}

		[HttpGet]
		//[Permission]
		[DisplayName("مشاهده جزئیات")]
		public async Task<IActionResult> GetDetail(int? id)
		{
			if (!id.HasValue)
			{
				return BadRequest();
			}

			var oData = await _roleManager.FindByIdAsync(id.ToString());
			var model = new RoleViewModel
			{
				ControllerInfos = _controllerDiscovery.GetControllers(),
				Name = oData.Name,
				Id = oData.Id,
				SelectedAccess = (await _roleManager.GetClaimsAsync(oData)).Select(o => o.Value).ToList(),
				JsonMenu = oData.PanelMenu
			};
			return PartialView("_Detail", model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		//[Permission]
		[DisplayName("افزودن")]
		public async Task<IActionResult> AddDetail(RoleViewModel model)
		{
			var result = await _roleManager.CreateAsync(new ApplicationRole(model.Name));
			if (result.Succeeded)
			{
				foreach (var item in model.SelectedAccess)
				{
					await _roleManager.AddClaimAsync(await _roleManager.FindByNameAsync(model.Name),
						new Claim("Permission", item));
				}

				//await _signInManager.SignInAsync(await _userManager.GetUserAsync(User), false);
				return Json(new
				{
					Message = Message.Show("اطلاعات نقش با موفقیت ویرایش شد...", MessageType.Success),
					RefreshGrid = true
				});
			}

			AddErrors(result);
			return Json(new
			{
				Message = Message.Show(ModelState.GetErrors(), MessageType.Warning)
			});
		}

		[HttpPost, ValidateAntiForgeryToken]
		[DisplayName("ویرایش")]
		//[Permission]
		public async Task<IActionResult> EditDetail(RoleViewModel model)
		{
			var role = await _roleManager.FindByIdAsync(model.Id.ToString());
			role.Name = model.Name;
			role.PanelMenu = model.JsonMenu;

			var result = await _roleManager.UpdateAsync(role);

			if (role.Name != model.Name)
			{
				role.Name = model.Name;
				result = await _roleManager.UpdateAsync(role);
			}

			if (result.Succeeded)
			{
				var roleClaimList = await _roleManager.GetClaimsAsync(role);
				foreach (var claim in roleClaimList)
				{
					await _roleManager.RemoveClaimAsync(role, claim);
				}

				foreach (var item in model.SelectedAccess)
				{
					await _roleManager.AddClaimAsync(role, new Claim("Permission", item));
				}

				await _signInManager.SignInAsync(await _userManager.GetUserAsync(User), false);
				return Json(new
				{
					Message = Message.Show("اطلاعات نقش با موفقیت ویرایش شد...", MessageType.Success),
					RefreshGrid = true
				});
			}

			AddErrors(result);
			return Json(new
			{
				Message = Message.Show(ModelState.GetErrors(), MessageType.Warning)
			});
		}

		private void AddErrors(IdentityResult result)
		{
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError("", error.Description);
			}
		}

		[HttpPost]
		//[Permission]
		[DisplayName("حذف")]
		public async Task<IActionResult> DeleteRows(List<int> ids)
		{
			if (!ids.Any()) return Json(false);
			await _roleManager.DeleteAsync(new ApplicationRole { Id = ids[0] });
			return Ok();
		}

		//[Permission]
		[DisplayName("تخصیص اختیار")]
		public async Task<IActionResult> AddAuthority(int? id)
		{
			var role = await _roleManager.FindByIdAsync(id.Value.ToString());
			var model = new AuthorityRoleViewModel()
			{
				RoleId = id.Value,
				RoleTitle = role.Name,
				AuthorityInfos = _authorityRepository.GetDropDownList(),
				SelectList = _applicationRoleAuthorityRepository.GetListByRoleId(id.Value).
								Select(q => q.AuthorityId.ToString()).ToList()
			};
			return PartialView("_Authority", model);
		}

		[HttpPost]
		public IActionResult AddAuthority(AuthorityRoleViewModel model)
		{
			foreach (var item in model.SelectList)
			{
				var applicationRoleAuthority = new ApplicationRoleAuthority
				{
					ApplicationRoleId = model.RoleId,
					AuthorityId = int.Parse(item),
				};
				_applicationRoleAuthorityRepository.Add(applicationRoleAuthority);
			}

			_unitOfWork.Save();
			return Json(new
			{
				Message = Message.Show("محدوده دسترسی با موفقیت ثبت شد...", MessageType.Success),
				RefreshGrid = true
			});
		}

		private string GetDefaultMenu()
		{
			return JsonConvert.SerializeObject(new List<UserPanelMenu>
			{
				new UserPanelMenu { Text = "خانه", Icon = "flaticon-line-graph", Link = "/home", Items = null }
			});
		}
	}
}