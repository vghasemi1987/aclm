using DomainContracts.AccessibilityAggregate;
using DomainContracts.AccessibilityLevelAggregate;
using DomainContracts.AccessTypeAggregate;
using DomainContracts.DepartmentAggregate;
using DomainContracts.ServiceAggregate;
using DomainContracts.SystemsAggregate;
using DomainEntities.AccessibilityAggregate;
using DomainEntities.ApplicationUserAggregate;
using DomainEntities.Commons;
using KendoHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Web.Core.Reports.ViewModels;
using Web.Extensions.Attributes;

namespace Web.Core.Reports
{
	[Authorize]
	[DisplayName("گزارشات")]
	public class ReportsController : Controller
	{
		private readonly ISystemsRepository _systemsRepository;
		private readonly IServiceRepository _serviceRepository;
		private readonly IDepartmentRepository _departmentRepository;
		private readonly IAccessTypeRepository _accessTypeRepository;
		private readonly IAccessibilityLevelRepository _accessibilityLevelRepository;
		private readonly IAccessibilityRepository _accessibilityRepository;
		private readonly UserManager<ApplicationUser> _userManager;

		public ReportsController(ISystemsRepository systemsRepository,
								IServiceRepository serviceRepository,
								IDepartmentRepository departmentRepository,
								IAccessTypeRepository accessTypeRepository,
								IAccessibilityLevelRepository accessibilityLevelRepository,
								IAccessibilityRepository accessibilityRepository,
								UserManager<ApplicationUser> userManager)
		{
			_systemsRepository = systemsRepository;
			_serviceRepository = serviceRepository;
			_departmentRepository = departmentRepository;
			_accessTypeRepository = accessTypeRepository;
			_accessibilityLevelRepository = accessibilityLevelRepository;
			_accessibilityRepository = accessibilityRepository;
			_userManager = userManager;
		}

		[HttpGet]
		[Permission]
		[DisplayName("گزارش دسترسی بر اساس تعداد")]
		public IActionResult ReportByCount()
		{
			return View("ReportByCount");
		}

		[HttpGet]
		public IActionResult GetReportByCount(string models, int count)
		{
			var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);
			var list = _accessibilityRepository.GetAccessibilityReportByCount(request, count);
			return Json(list);
		}

		[HttpGet]
		[Permission]
		[DisplayName("گزارش بر اساس پارامتر")]
		public IActionResult ReportByFilter(SearchViewModel model)
		{
			return View("ReportByFilter", model);
		}
		public async Task<IActionResult> Search(SearchViewModel model)
		{
			model.SystemList = new SelectList(await _systemsRepository.GetDropDownListAsync(), nameof(DropDownDto.Id), nameof(DropDownDto.Title));
			model.ServiceList = new SelectList(await _serviceRepository.GetDropDownList(), nameof(DropDownDto.Id), nameof(DropDownDto.Title));
			model.DepartmentList = new SelectList(await _departmentRepository.GetDropDownList(), nameof(DropDownDto.Id), nameof(DropDownDto.Title));
			model.AccessibilityTypeList = new SelectList(await _accessTypeRepository.GetDropDownList(), nameof(DropDownDto.Id), nameof(DropDownDto.Title));
			model.AccessibilityLeveList = new SelectList(await _accessibilityLevelRepository.GetDropDownList(), nameof(DropDownDto.Id), nameof(DropDownDto.Title));
			model.UserList = new SelectList(_userManager.Users.Select(p => new DropDownDto { Id = p.Id, Title = p.Name }).ToList(), nameof(DropDownDto.Id), nameof(DropDownDto.Title));
			return PartialView("_Search", model);
		}
		[HttpGet]
		public IActionResult GetReportByFilter(string models, SearchViewModel searchModel)
		{
			var list = new DataSourceResult();
			var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);
			list = searchModel.Search.HasValue ?
					_accessibilityRepository.GetReportByFilter(request,
					new AccessibilityFilter
					{
						SourceSystemId = searchModel.SourceSystemId,
						SourceIp = searchModel.SourceIp,
						DestinationIp = searchModel.DestinationIp,
						DestinationSystemId = searchModel.DestinationSystemId,
						ServiceId = searchModel.ServiceId,
						DestinationServiceId = searchModel.DestinationServiceId,
						ConfirmUserId = searchModel.ConfirmUserId,
						RequestingUserId = searchModel.RequestingUserId,
						UserDepartmentId = searchModel.UserDepartmentId,
						AccessibilityTypeId = searchModel.AccessibilityTypeId,
					})
				 : list;
			return Json(list);
		}
	}
}