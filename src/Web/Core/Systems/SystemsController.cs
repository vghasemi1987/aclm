using ApplicationCommon;
using DomainContracts.AccessibilityLevelAggregate;
using DomainContracts.Commons;
using DomainContracts.DepartmentAggregate;
using DomainContracts.SystemsAggregate;
using DomainEntities.Commons;
using KendoHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Web.Core.Systems.ViewModels;
using Web.Extensions.Attributes;

namespace Web.Core.Systems
{
	[Authorize]
	[DisplayName("موجودیت ها")]
	public class SystemsController : Controller
	{
		private readonly ISystemsRepository _systemRepository;
		private readonly IDepartmentRepository _departmentRepository;
		private readonly IAccessibilityLevelRepository _accessibilityLevelRepository;
		private readonly IUnitOfWork _unitOfWork;
		public SystemsController(ISystemsRepository systemRepository,
								 IAccessibilityLevelRepository accessibilityLevelRepository,
								 IDepartmentRepository departmentRepository,
								 IUnitOfWork unitOfWork)
		{
			_systemRepository = systemRepository;
			_accessibilityLevelRepository = accessibilityLevelRepository;
			_departmentRepository = departmentRepository;
			_unitOfWork = unitOfWork;
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
			var list = _systemRepository.GetList(request);
			return Json(list);
		}
		[HttpGet]
		[Permission]
		[DisplayName("مشاهده جزییات")]
		public async Task<IActionResult> GetDetail(int id)
		{
			var model = new SystemViewModel()
			{
				AccessibilityLevelList = new SelectList(await _accessibilityLevelRepository.GetDropDownList(),
				nameof(DropDownDto.Id), nameof(DropDownDto.Title)),
				DepartmentList = new SelectList(await _departmentRepository.GetDropDownList(),
				nameof(DropDownDto.Id), nameof(DropDownDto.Title)),
			};
			if (id > 0)
			{
				var system = _systemRepository.FindByIdAsync(id);
				model.Id = system.Id;
				model.SystemName = system.SystemName;
				model.IpAddress = system.IpAddress;
				model.InformationAccessibilityLevelId = system.InformationAccessibilityLevelId;
				model.AccessibilityLevelId = system.AccessibilityLevelId;
				model.DepartmentId = system.DepartmentId;
				model.ImportanceFactor = system.ImportanceFactor;
				model.PersonelCode = system.PersonelCode;
				model.FirstName = system.FirstName;
				model.LastName = system.LastName;
				model.KindId = system.KindId;
				model.IpFrom = system.IpFrom;
				model.IpTo = system.IpTo;
			}
			return PartialView("_Detail", model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		[Permission]
		[DisplayName("افزودن")]
		public async Task<IActionResult> AddDetail(SystemViewModel model)
		{
			var system = new DomainEntities.SystemsAggregate.Systems
			{
				SystemName = model.SystemName,
				IpAddress = model.IpAddress,
				InformationAccessibilityLevelId = model.InformationAccessibilityLevelId,
				AccessibilityLevelId = model.AccessibilityLevelId,
				DepartmentId = model.DepartmentId,
				ImportanceFactor = model.ImportanceFactor,
				FirstName = model.FirstName,
				LastName = model.LastName,
				PersonelCode = model.PersonelCode,
				KindId = model.KindId,
				IpFrom = model.IpFrom,
				IpTo = model.IpTo,
				RecordStatus = Convert.ToBoolean(RecordStatus.NotDeleted),
			};

			_systemRepository.Add(system);
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
		public async Task<IActionResult> EditDetail(SystemViewModel model)
		{
			var system = _systemRepository.FindByIdAsync(model.Id);

			system.Id = model.Id;
			system.SystemName = model.SystemName;
			system.IpAddress = model.IpAddress;
			system.InformationAccessibilityLevelId = model.InformationAccessibilityLevelId;
			system.AccessibilityLevelId = model.AccessibilityLevelId;
			system.ImportanceFactor = model.ImportanceFactor;
			system.DepartmentId = model.DepartmentId;
			system.FirstName = model.FirstName;
			system.LastName = model.LastName;
			system.PersonelCode = model.PersonelCode;
			system.KindId = model.KindId;
			system.IpFrom = model.IpFrom;
			system.IpTo = model.IpTo;
			_systemRepository.Update(system);
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
				var system = _systemRepository.FindByIdAsync(id);
				system.RecordStatus = Convert.ToBoolean(RecordStatus.Deleted);
				_systemRepository.Update(system);
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