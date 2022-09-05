using ApplicationCommon;
using DomainContracts.AccessRangeAggregate;
using DomainContracts.Commons;
using DomainEntities.AccessRangeAggregate;
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
using Web.Core.AccessRanges.ViewModels;
using Web.Extensions.Attributes;

namespace Web.Core.AccessRanges
{
	[Authorize]
	[DisplayName("محدوده دسترسی")]
	public class AccessRangesController : Controller
	{
		private readonly IAccessRangeHeaderRepository _accessRangeHeaderRepository;
		private readonly IAccessRangeDetailRepository _accessRangeDetailRepository;
		private readonly IUnitOfWork _unitOfWork;
		public AccessRangesController(IAccessRangeHeaderRepository accessRangeHeaderRepository,
									  IAccessRangeDetailRepository accessRangeDetailRepository,
									  IUnitOfWork unitOfWork)
		{
			_accessRangeHeaderRepository = accessRangeHeaderRepository;
			_accessRangeDetailRepository = accessRangeDetailRepository;
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
			var list = _accessRangeHeaderRepository.GetList(request);
			return Json(list);
		}
		[HttpGet]
		[Permission]
		[DisplayName("مشاهده جزییات")]
		public async Task<IActionResult> GetDetail(int id)
		{
			var model = new AccessRangeViewModel();
			if (id > 0)
			{
				var accessRangeHeader = await _accessRangeHeaderRepository.FindByIdAsync(id);
				model.AccessRangeDetails = _accessRangeDetailRepository.FindByHeaderId(new List<int> { accessRangeHeader.Id });
				model.Id = accessRangeHeader.Id;
				model.Title = accessRangeHeader.Title;
			}
			return PartialView("_Detail", model);
		}

		[HttpPost]//, ValidateAntiForgeryToken
		[Permission]
		[DisplayName("افزودن")]
		public async Task<IActionResult> AddDetail(List<AccessRangeViewModel> model)
		{
			var accessRangeHeader = new AccessRangeHeader
			{
				Title = model[0].Title,
				RecordStatus = Convert.ToBoolean(RecordStatus.NotDeleted),
			};
			_accessRangeHeaderRepository.Add(accessRangeHeader);
			foreach (var item in model)
			{
				_accessRangeDetailRepository.Add(new AccessRangeDetail
				{
					IpFrom = item.IpFrom,
					IpTo = item.IpTo,
					AccessRangeHeader = accessRangeHeader
				});
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
		public async Task<IActionResult> EditDetail(AccessRangeViewModel model)
		{
			var accessRange = await _accessRangeHeaderRepository.FindByIdAsync(model.Id);
			accessRange.Title = model.Title;
			_accessRangeHeaderRepository.Update(accessRange);
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
				var accessRange = await _accessRangeHeaderRepository.FindByIdAsync(id);
				accessRange.RecordStatus = Convert.ToBoolean(RecordStatus.Deleted);
				_accessRangeHeaderRepository.Update(accessRange);
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