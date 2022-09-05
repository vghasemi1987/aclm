using ApplicationCommon;
using DomainContracts.AccessibilityAggregate;
using DomainContracts.AccessibilityLevelAggregate;
using DomainContracts.AccessibilityRequestAggregate;
using DomainContracts.AccessTypeAggregate;
using DomainContracts.Commons;
using DomainContracts.DepartmentAggregate;
using DomainContracts.ProtocolAggregate;
using DomainContracts.ServiceAggregate;
using DomainContracts.SystemsAggregate;
using DomainEntities.AccessibilityRequestAggregate;
using DomainEntities.ApplicationUserAggregate;
using DomainEntities.Commons;
using KendoHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Web.Core.AccessibilityRequests.ViewModels;
using Web.Extensions.Attributes;

namespace Web.Core.AccessibilityRequests
{
	[Authorize]
	[DisplayName("درخواست دسترسی")]
	public class AccessibilityRequestsController : Controller
	{
		private readonly IAccessibilityRequestRepository _accessibilityRequestRepository;
		private readonly ISystemsRepository _systemsRepository;
		private readonly IProtocolRepository _protocolRepository;
		private readonly IServiceRepository _serviceRepository;
		private readonly IDepartmentRepository _departmentRepository;
		private readonly IAccessibilityRepository _accessibilityRepository;
		private readonly IAccessTypeRepository _accessTypeRepository;
		private readonly IAccessibilityLevelRepository _accessibilityLevelRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<ApplicationUser> _userManager;
		public AccessibilityRequestsController(IAccessibilityRequestRepository accessibilityRequestRepository,
												ISystemsRepository systemsRepository,
												IProtocolRepository protocolRepository,
												IServiceRepository serviceRepository,
												IDepartmentRepository departmentRepository,
												IAccessibilityRepository accessibilityRepository,
												IAccessTypeRepository accessTypeRepository,
												IAccessibilityLevelRepository accessibilityLevelRepository,
												IUnitOfWork unitOfWork,
												UserManager<ApplicationUser> userManager)
		{
			_accessibilityRequestRepository = accessibilityRequestRepository;
			_systemsRepository = systemsRepository;
			_protocolRepository = protocolRepository;
			_serviceRepository = serviceRepository;
			_departmentRepository = departmentRepository;
			_accessibilityRepository = accessibilityRepository;
			_accessTypeRepository = accessTypeRepository;
			_accessibilityLevelRepository = accessibilityLevelRepository;
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
			var list = _accessibilityRequestRepository.GetList(request);
			return Json(list);
		}
		[HttpGet]
		[Permission]
		[DisplayName("مشاهده جزییات")]
		public async Task<IActionResult> GetDetail(int id)
		{
			var model = new AccessibilityRequestViewModel()
			{
				ProtocolList = new SelectList(await _protocolRepository.GetDropDownList(), nameof(DropDownDto.Id), nameof(DropDownDto.Title)),
				SystemList = new SelectList(await _systemsRepository.GetDropDownListAsync(), nameof(DropDownDto.Id), nameof(DropDownDto.Title)),
				ServiceList = new SelectList(await _serviceRepository.GetDropDownList(), nameof(DropDownDto.Id), nameof(DropDownDto.Title)),
				DepartmentList = new SelectList(await _departmentRepository.GetDropDownList(), nameof(DropDownDto.Id), nameof(DropDownDto.Title)),
				AccessibilityTypeList = new SelectList(await _accessTypeRepository.GetDropDownList(), nameof(DropDownDto.Id), nameof(DropDownDto.Title)),
				AccessibilityLeveList = new SelectList(await _accessibilityLevelRepository.GetDropDownList(), nameof(DropDownDto.Id), nameof(DropDownDto.Title)),
				UserList = new SelectList(_userManager.Users.Select(p => new DropDownDto { Id = p.Id, Title = p.Name }).ToList(), nameof(DropDownDto.Id), nameof(DropDownDto.Title)),
				SystemIpList = _systemsRepository.GetSystemIpList()
			};
			if (id > 0)
			{
				var accessibilityRequest = await _accessibilityRequestRepository.FindByIdAsync(id);
				model.Id = accessibilityRequest.Id;
				model.SourceSystemId = accessibilityRequest.SourceSystemId;
				model.DestinationSystemId = accessibilityRequest.DestinationSystemId;
				model.SourceProtocolId = accessibilityRequest.SourceProtocolId;
				model.DestinationProtocolId = accessibilityRequest.DestinationProtocolId;
				model.ServiceId = accessibilityRequest.ServiceId;
				model.DestinationServiceId = accessibilityRequest.DestinationServiceId;
				model.LetterNo = accessibilityRequest.LetterNo;
				model.LetterDate = accessibilityRequest.LetterDate.Value.ToString().ToPersianDateTime();
				model.Description = accessibilityRequest.Description;
				model.PhoneNumber = accessibilityRequest.PhoneNumber;
				model.RequestDepartmentId = accessibilityRequest.RequestDepartmentId;
				model.UserDepartmentId = accessibilityRequest.UserDepartmentId;
				model.RequestingUserId = accessibilityRequest.RequestingUserId;
				model.ConfirmUserId = accessibilityRequest.ConfirmUserId;
				model.SourceAccessibilityLevelId = accessibilityRequest.SourceAccessibilityLevelId;
				model.DestAccessibilityLevelId = accessibilityRequest.DestAccessibilityLevelId;
				model.AccessibilityTypeId = accessibilityRequest.AccessibilityTypeId;
				model.AccessEndDate = accessibilityRequest.AccessEndDate.Value.ToString().ToPersianDateTime();
				model.AccessStartDate = accessibilityRequest.AccessStartDate.Value.ToString().ToPersianDateTime();
				model.SourceIp = accessibilityRequest.SourceIp;
				model.DestinationIp = accessibilityRequest.DestinationIp;
			}
			return PartialView("_Detail", model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		[Permission]
		[DisplayName("افزودن")]
		public async Task<IActionResult> AddDetail(AccessibilityRequestViewModel model)
		{
			var accessibilityRequest = new AccessibilityRequest
			{
				Id = model.Id,
				SourceSystemId = model.SourceSystemId,
				SourceIp = model.SourceIp,
				DestinationIp = model.DestinationIp,
				DestinationSystemId = model.DestinationSystemId,
				SourceProtocolId = model.SourceProtocolId,
				DestinationProtocolId = model.DestinationProtocolId,
				ServiceId = model.ServiceId,
				DestinationServiceId = model.DestinationServiceId,
				LetterNo = model.LetterNo,
				Description = model.Description,
				PhoneNumber = model.PhoneNumber,
				RequestDepartmentId = model.RequestDepartmentId,
				UserDepartmentId = model.UserDepartmentId,
				RequestingUserId = model.RequestingUserId,
				ConfirmUserId = model.ConfirmUserId,
				SourceAccessibilityLevelId = model.SourceAccessibilityLevelId,
				DestAccessibilityLevelId = model.DestAccessibilityLevelId,
				AccessibilityTypeId = model.AccessibilityTypeId,
				LetterDate = model.LetterDate.ToMiladiDate(),
				AccessEndDate = model.AccessEndDate?.ToMiladiDate(),
				AccessStartDate = model.AccessStartDate?.ToMiladiDate(),
				RecordStatus = Convert.ToBoolean(RecordStatus.NotDeleted),
			};
			_accessibilityRequestRepository.Add(accessibilityRequest);
			//TODO Update ImportanceFactor
			if (model.SourceSystemId.HasValue)
			{
				var sourceSystem = _systemsRepository.FindByIdAsync(model.SourceSystemId.Value);
				sourceSystem.ImportanceFactor = sourceSystem.ImportanceFactor.HasValue ? (sourceSystem.ImportanceFactor + model.ImportanceFactor) / 2 : model.ImportanceFactor;
				_systemsRepository.Update(sourceSystem);
			}
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
		public async Task<IActionResult> EditDetail(AccessibilityRequestViewModel model)
		{
			var accessibilityRequest = await _accessibilityRequestRepository.FindByIdAsync(model.Id);
			accessibilityRequest.Id = model.Id;
			accessibilityRequest.SourceSystemId = model.SourceSystemId;
			accessibilityRequest.DestinationSystemId = model.DestinationSystemId;
			accessibilityRequest.SourceIp = model.SourceIp;
			accessibilityRequest.DestinationIp = model.DestinationIp;
			accessibilityRequest.SourceProtocolId = model.SourceProtocolId;
			accessibilityRequest.DestinationProtocolId = model.DestinationProtocolId;
			accessibilityRequest.ServiceId = model.ServiceId;
			accessibilityRequest.DestinationServiceId = model.DestinationServiceId;
			accessibilityRequest.LetterNo = model.LetterNo;
			accessibilityRequest.LetterDate = model.LetterDate.ToMiladiDate();
			accessibilityRequest.Description = model.Description;
			accessibilityRequest.PhoneNumber = model.PhoneNumber;
			accessibilityRequest.RequestDepartmentId = model.RequestDepartmentId;
			accessibilityRequest.UserDepartmentId = model.UserDepartmentId;
			accessibilityRequest.RequestingUserId = model.RequestingUserId;
			accessibilityRequest.ConfirmUserId = model.ConfirmUserId;
			accessibilityRequest.SourceAccessibilityLevelId = model.SourceAccessibilityLevelId;
			accessibilityRequest.DestAccessibilityLevelId = model.DestAccessibilityLevelId;
			accessibilityRequest.AccessibilityTypeId = model.AccessibilityTypeId;
			accessibilityRequest.AccessEndDate = model.AccessEndDate?.ToMiladiDate();
			accessibilityRequest.AccessStartDate = model.AccessStartDate?.ToMiladiDate();
			accessibilityRequest.RecordStatus = Convert.ToBoolean(RecordStatus.NotDeleted);

			_accessibilityRequestRepository.Update(accessibilityRequest);
			await _unitOfWork.SaveAsync();
			return Json(new
			{
				Message = Message.Show("اطلاعات مشتری با موفقیت ویرایش شد...", MessageType.Success),
				RefreshGrid = true
			});
		}

		[HttpDelete]
		[Permission]
		[DisplayName("حذف")]
		public async Task<IActionResult> Delete(List<int> ids)
		{
			if (!ids.Any()) return Json(false);
			foreach (var id in ids)
			{
				var accessibilityRequest = await _accessibilityRequestRepository.FindByIdAsync(id);
				accessibilityRequest.RecordStatus = Convert.ToBoolean(RecordStatus.Deleted);
				_accessibilityRequestRepository.Update(accessibilityRequest);
			}

			await _unitOfWork.SaveAsync();
			return Json(new
			{
				Message = Message.Show("حذف با موفقیت انجام شد...", MessageType.Success),
				RefreshGrid = true
			});
		}
	}
}