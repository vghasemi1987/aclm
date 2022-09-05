using ApplicationCommon;
using DomainContracts.AccessTypeAggregate;
using DomainContracts.Commons;
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
using Web.Core.AccessTypes.ViewModels;
using Web.Extensions.Attributes;

namespace Web.Core.AccessTypes
{
	[Authorize]
	[DisplayName("نوع دسترسی")]
	public class AccessTypesController : Controller
	{
		private readonly IAccessTypeRepository _accessTypeRepository;
		private readonly IUnitOfWork _unitOfWork;
		public AccessTypesController(IAccessTypeRepository accessTypeRepository, IUnitOfWork unitOfWork)
		{
			_accessTypeRepository = accessTypeRepository;
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
			var list = _accessTypeRepository.GetList(request);
			return Json(list);
		}

		[HttpGet]
		[Permission]
		[DisplayName("مشاهده جزییات")]
		public async Task<IActionResult> GetDetail(int id)
		{
			var model = new AccessTypeViewModel();
			if (id > 0)
			{
				var accessType = await _accessTypeRepository.FindByIdAsync(id);
				model.Id = accessType.Id;
				model.Title = accessType.Title;
				model.Description = accessType.Description;
			}
			return PartialView("_Detail", model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		[Permission]
		[DisplayName("افزودن")]
		public async Task<IActionResult> AddDetail(AccessTypeViewModel model)
		{
			var accessType = new DomainEntities.AccessTypeAggregate.AccessType
			{
				Description = model.Description,
				Title = model.Title,
				RecordStatus = Convert.ToBoolean(RecordStatus.NotDeleted),
			};

			_accessTypeRepository.Add(accessType);
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
		public async Task<IActionResult> EditDetail(AccessTypeViewModel model)
		{
			var accessType = await _accessTypeRepository.FindByIdAsync(model.Id);

			accessType.Title = model.Title;
			accessType.Description = model.Description;
			_accessTypeRepository.Update(accessType);
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
				var accessType = await _accessTypeRepository.FindByIdAsync(id);
				accessType.RecordStatus = Convert.ToBoolean(RecordStatus.Deleted);
				_accessTypeRepository.Update(accessType);
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