using ApplicationCommon;
using DomainContracts.Commons;
using DomainContracts.ServiceGroupAggregate;
using DomainEntities.Commons;
using KendoHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Web.Core.ServiceGroups.ViewModels;
using Web.Extensions.Attributes;

namespace Web.Core.ServiceGroups
{
	[Authorize]
	[DisplayName("گروهبندی سرویس")]
	public class ServiceGroupsController : Controller
	{
		private readonly IServiceGroupRepository _serviceGroupRepository;
		private readonly IUnitOfWork _unitOfWork;
		public ServiceGroupsController(IServiceGroupRepository serviceGroupRepository, IUnitOfWork unitOfWork)
		{
			_serviceGroupRepository = serviceGroupRepository;
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
			var list = _serviceGroupRepository.GetList(request);
			return Json(list);
		}
		[HttpGet]
		[Permission]
		[DisplayName("مشاهده جزییات")]
		public async Task<IActionResult> GetDetail(int id)
		{
			var model = new ServiceGroupViewModel();
			if (id > 0)
			{
				var serviceGroup = await _serviceGroupRepository.FindByIdAsync(id);
				model.Id = serviceGroup.Id;
				model.Name = serviceGroup.Name;
				model.Description = serviceGroup.Description;
			}
			return PartialView("_Detail", model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		[Permission]
		[DisplayName("افزودن")]
		public async Task<IActionResult> AddDetail(ServiceGroupViewModel model)
		{
			var serviceGroup = new DomainEntities.ServiceGroupAggregate.ServiceGroup
			{
				Description = model.Description,
				Name = model.Name,
				RecordStatus = Convert.ToBoolean(RecordStatus.NotDeleted),
			};

			_serviceGroupRepository.Add(serviceGroup);
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
		public async Task<IActionResult> EditDetail(ServiceGroupViewModel model)
		{
			var serviceGroup = await _serviceGroupRepository.FindByIdAsync(model.Id);

			serviceGroup.Name = model.Name;
			serviceGroup.Description = model.Description;
			_serviceGroupRepository.Update(serviceGroup);
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
				var serviceGroup = await _serviceGroupRepository.FindByIdAsync(id);
				serviceGroup.RecordStatus = Convert.ToBoolean(RecordStatus.Deleted);
				_serviceGroupRepository.Update(serviceGroup);
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