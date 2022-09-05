using ApplicationCommon;
using DomainContracts.AddressTypeAggregate;
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
using Web.Core.AddressTypes.ViewModels;
using Web.Extensions.Attributes;

namespace Web.Core.AddressTypes
{
	[Authorize]
	[DisplayName("نوع آدرس")]
	public class AddressTypesController : Controller
	{
		private readonly IAddressTypeRepository _addressTypeRepository;
		private readonly IUnitOfWork _unitOfWork;
		public AddressTypesController(IAddressTypeRepository addressTypeRepository, IUnitOfWork unitOfWork)
		{
			_addressTypeRepository = addressTypeRepository;
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
			var list = _addressTypeRepository.GetList(request);
			return Json(list);
		}
		[HttpGet]
		[Permission]
		[DisplayName("مشاهده جزییات")]
		public async Task<IActionResult> GetDetail(int id)
		{
			var model = new AddressTypeViewModel();
			if (id > 0)
			{
				var addressType = await _addressTypeRepository.FindByIdAsync(id);
				model.Id = addressType.Id;
				model.Title = addressType.Title;
				model.Description = addressType.Description;
			}
			return PartialView("_Detail", model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		[Permission]
		[DisplayName("افزودن")]
		public async Task<IActionResult> AddDetail(AddressTypeViewModel model)
		{
			var addressType = new DomainEntities.AddressTypeAggregate.AddressType
			{
				Description = model.Description,
				Title = model.Title,
				RecordStatus = Convert.ToBoolean(RecordStatus.NotDeleted),
			};

			_addressTypeRepository.Add(addressType);
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
		public async Task<IActionResult> EditDetail(AddressTypeViewModel model)
		{
			var addressType = await _addressTypeRepository.FindByIdAsync(model.Id);

			addressType.Title = model.Title;
			addressType.Description = model.Description;
			_addressTypeRepository.Update(addressType);
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
				var addressType = await _addressTypeRepository.FindByIdAsync(id);
				addressType.RecordStatus = Convert.ToBoolean(RecordStatus.Deleted);
				_addressTypeRepository.Update(addressType);
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