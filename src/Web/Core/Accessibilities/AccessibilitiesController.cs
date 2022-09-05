using ApplicationCommon;
using DomainContracts.AccessibilityAggregate;
using DomainContracts.ActionTypeAggregate;
using DomainContracts.Commons;
using DomainContracts.ProtocolAggregate;
using DomainContracts.RouterAggregate;
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
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Web.Core.Accessibilities.ViewModels;
using Web.Extensions;
using Web.Extensions.Attributes;

namespace Web.Core.Accessibilities
{
	[Authorize]
	[DisplayName("دسترسی")]
	public class AccessibilitiesController : Controller
	{
		private readonly IAccessibilityRepository _accessibilityRepository;
		private readonly ISystemsRepository _systemsRepository;
		private readonly IProtocolRepository _protocolRepository;
		private readonly IRouterRepository _routerRepository;
		private readonly IServiceRepository _serviceRepository;
		private readonly IActionTypeRepository _actionTypeRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<ApplicationUser> _userManager;
		public AccessibilitiesController(IAccessibilityRepository accessibilityRepository,
										ISystemsRepository systemsRepository,
										IProtocolRepository protocolRepository,
										IRouterRepository routerRepository,
										IServiceRepository serviceRepository,
										IActionTypeRepository actionTypeRepository,
										IUnitOfWork unitOfWork,
										UserManager<ApplicationUser> userManager)
		{
			_accessibilityRepository = accessibilityRepository;
			_systemsRepository = systemsRepository;
			_protocolRepository = protocolRepository;
			_routerRepository = routerRepository;
			_serviceRepository = serviceRepository;
			_actionTypeRepository = actionTypeRepository;
			_unitOfWork = unitOfWork;
			_userManager = userManager;
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
			var list = _accessibilityRepository.GetList(request);
			return Json(list);
		}
		[HttpGet]
		[Permission]
		[DisplayName("مشاهده جزییات")]
		public async Task<IActionResult> GetDetail(int id)
		{
			var model = new AccessibilityViewModel()
			{
				ProtocolList = new SelectList(await _protocolRepository.GetDropDownList(), nameof(DropDownDto.Id), nameof(DropDownDto.Title)),
				RouterList = new SelectList(await _routerRepository.GetDropDownList(), nameof(DropDownDto.Id), nameof(DropDownDto.Title)),
				ActionTypeList = new SelectList(await _actionTypeRepository.GetDropDownList(), nameof(DropDownDto.Id), nameof(DropDownDto.Title)),
				SystemList = new SelectList(await _systemsRepository.GetDropDownListAsync(), nameof(DropDownDto.Id), nameof(DropDownDto.Title)),
				ServiceList = new SelectList(await _serviceRepository.GetDropDownList(), nameof(DropDownDto.Id), nameof(DropDownDto.Title)),
				SystemIpList = _systemsRepository.GetSystemIpList()
			};

			if (id > 0)
			{
				var accessibility = await _accessibilityRepository.FindByIdAsync(id);
				model.Id = accessibility.Id;
				model.IsTemp = accessibility.IsTemp;
				model.AclFilesUploadId = accessibility.AclFilesUploadId;
				model.ActionTypeId = accessibility.ActionTypesId;
				model.ProtocolId = accessibility.ProtocolsId;
				model.RouterId = accessibility.RouterId;
				model.ServiceId = accessibility.ServiceId;
				model.DestinationServiceId = accessibility.ServiceId;
				model.SourceSystemId = accessibility.SourceSystemId;
				model.DestinationSystemId = accessibility.DestinationSystemId;
				model.SourceIp = accessibility.SourceIp;
				model.DestinationIp = accessibility.DestinationIp;
			}
			return PartialView("_Detail", model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		[Permission]
		[DisplayName("افزودن")]
		public async Task<IActionResult> AddDetail(AccessibilityViewModel model)
		{
			var accessibility = new Accessibility
			{
				Id = model.Id,
				DestinationIp = model.DestinationIp,
				SourceIp = model.SourceIp,
				AclFilesUploadId = model.AclFilesUploadId,
				ActionTypesId = model.ActionTypeId,
				DestinationSystemId = model.DestinationSystemId,
				ProtocolsId = model.ProtocolId,
				RouterId = model.RouterId,
				ServiceId = model.ServiceId,
				DestinationServiceId = model.DestinationServiceId,
				SourceSystemId = model.SourceSystemId,
				RecordStatus = Convert.ToBoolean(RecordStatus.NotDeleted),
				UserId = User.GetUserId(),
				IsTemp = true,
			};

			_accessibilityRepository.Add(accessibility);
			await _unitOfWork.SaveAsync();

			return Json(new
			{
				Message = Message.Show("اطلاعات مشتری با موفقیت ثبت شد...", MessageType.Success),
				RefreshGrid = true
			});
		}

		[HttpPost, ValidateAntiForgeryToken]
		[Permission]
		[DisplayName("ویرایش")]
		public async Task<IActionResult> EditDetail(AccessibilityViewModel model)
		{
			var accessibility = await _accessibilityRepository.FindByIdAsync(model.Id);
			accessibility.Id = model.Id;
			accessibility.DestinationIp = model.DestinationIp;
			accessibility.SourceIp = model.SourceIp;
			//accessibility.DestinationPort = model.DestinationPort;
			// accessibility.SourcePort = model.SourcePort;
			accessibility.AclFilesUploadId = model.AclFilesUploadId;
			accessibility.ActionTypesId = model.ActionTypeId;
			accessibility.DestinationSystemId = model.DestinationSystemId;
			accessibility.ProtocolsId = model.ProtocolId;
			accessibility.RouterId = model.RouterId;
			accessibility.ServiceId = model.ServiceId;
			accessibility.DestinationServiceId = model.DestinationServiceId;
			accessibility.SourceSystemId = model.SourceSystemId;
			accessibility.RecordStatus = Convert.ToBoolean(RecordStatus.NotDeleted);
			_accessibilityRepository.Update(accessibility);
			await _unitOfWork.SaveAsync();
			return Json(new
			{
				Message = Message.Show("اطلاعات مشتری با موفقیت ویرایش شد...", MessageType.Success),
				RefreshGrid = true
			});
		}

		[HttpPost]
		[Permission]
		[DisplayName("حذف")]
		public async Task<IActionResult> DeleteRows(int id)
		{
			var accessibility = await _accessibilityRepository.FindByIdAsync(id);
			accessibility.RecordStatus = Convert.ToBoolean(RecordStatus.Deleted);
			_accessibilityRepository.Update(accessibility);
			await _unitOfWork.SaveAsync();
			return Ok();
		}

		[Permission]
		[DisplayName("تایید دسترسی")]
		public IActionResult ConfirmAccessibilities()
		{
			_accessibilityRepository.ConfirmAccessibilities(User.GetUserId());
			return Json(new
			{
				Message = Message.Show("تایید دسترسی با موفقیت انجام شد...", MessageType.Success),
				RefreshGrid = true
			});
		}
	}
}