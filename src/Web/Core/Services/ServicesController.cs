using ApplicationCommon;
using DomainContracts.Commons;
using DomainContracts.ProtocolAggregate;
using DomainContracts.ServiceAggregate;
using DomainContracts.ServiceLevelAggregate;
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
using Web.Core.Services.ViewModels;
using Web.Extensions.Attributes;

namespace Web.Core.Services
{
	[Authorize]
	[DisplayName("سرویس(پورت)")]
	public class ServicesController : Controller
	{
		private readonly IServiceRepository _serviceRepository;
		private readonly IProtocolRepository _protocolRepository;
		private readonly IServiceLevelRepository _serviceLevelRepository;
		private readonly IUnitOfWork _unitOfWork;
		public ServicesController(IServiceRepository serviceRepository, IUnitOfWork unitOfWork,
			IProtocolRepository protocolRepository, IServiceLevelRepository serviceLevelRepository)
		{
			_serviceRepository = serviceRepository;
			_protocolRepository = protocolRepository;
			_serviceLevelRepository = serviceLevelRepository;
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
			var list = _serviceRepository.GetList(request);
			return Json(list);
		}
		[HttpGet]
		[Permission]
		[DisplayName("مشاهده جزییات")]
		public async Task<IActionResult> GetDetail(int id)
		{
			var model = new ServiceViewModel
			{
				ServiceLevelSelectList = new SelectList(await _serviceLevelRepository.GetDropDownList(),
				nameof(DropDownDto.Id), nameof(DropDownDto.Title)),
				ProtocolSelectList = new SelectList(await _protocolRepository.GetDropDownList(),
				nameof(DropDownDto.Id), nameof(DropDownDto.Title))
			};
			if (id > 0)
			{
				var service = await _serviceRepository.FindByIdAsync(id);
				model.Id = service.Id;
				model.Name = service.Name;
				model.Description = service.Description;
				model.Port = service.Port;
				model.ProtocolId = service.ProtocolId;
				model.ServiceLevelId = service.ServiceLevelId;
			}
			return PartialView("_Detail", model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		[Permission]
		[DisplayName("افزودن")]
		public async Task<IActionResult> AddDetail(ServiceViewModel model)
		{
			var service = new DomainEntities.ServiceAggregate.Service
			{
				Description = model.Description,
				Name = model.Name,
				Port = model.Port,
				RecordStatus = Convert.ToBoolean(RecordStatus.NotDeleted),
				ProtocolId = model.ProtocolId,
				ServiceLevelId = model.ServiceLevelId,
			};

			_serviceRepository.Add(service);
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
		public async Task<IActionResult> EditDetail(ServiceViewModel model)
		{
			var service = await _serviceRepository.FindByIdAsync(model.Id);

			service.Name = model.Name;
			service.Description = model.Description;
			service.ProtocolId = model.ProtocolId;
			service.ServiceLevelId = model.ServiceLevelId;
			service.Port = model.Port;
			_serviceRepository.Update(service);
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
				var service = await _serviceRepository.FindByIdAsync(id);
				service.RecordStatus = Convert.ToBoolean(RecordStatus.Deleted);
				_serviceRepository.Update(service);
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