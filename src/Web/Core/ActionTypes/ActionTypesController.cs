using ApplicationCommon;
using DomainContracts.ActionTypeAggregate;
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
using Web.Core.ActionTypes.ViewModels;
using Web.Extensions.Attributes;

namespace Web.Core.ActionTypes
{
	[Authorize]
	[DisplayName("نوع عملیات")]
	public class ActionTypesController : Controller
	{
		private readonly IActionTypeRepository _actionTypeRepository;
		private readonly IUnitOfWork _unitOfWork;
		public ActionTypesController(IActionTypeRepository actionTypeRepository, IUnitOfWork unitOfWork)
		{
			_actionTypeRepository = actionTypeRepository;
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
			var list = _actionTypeRepository.GetList(request);
			return Json(list);
		}
		[HttpGet]
		[Permission]
		[DisplayName("مشاهده جزییات")]
		public async Task<IActionResult> GetDetail(int id)
		{
			var model = new ActionTypeViewModel();
			if (id > 0)
			{
				var actionType = await _actionTypeRepository.FindByIdAsync(id);
				model.Id = actionType.Id;
				model.Title = actionType.Title;
			}
			return PartialView("_Detail", model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		[Permission]
		[DisplayName("افزودن")]
		public async Task<IActionResult> AddDetail(ActionTypeViewModel model)
		{
			var actionType = new DomainEntities.ActionTypeAggregate.ActionType
			{
				Title = model.Title,
				RecordStatus = Convert.ToBoolean(RecordStatus.NotDeleted),
			};

			_actionTypeRepository.Add(actionType);
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
		public async Task<IActionResult> EditDetail(ActionTypeViewModel model)
		{
			var actionType = await _actionTypeRepository.FindByIdAsync(model.Id);

			actionType.Title = model.Title;
			_actionTypeRepository.Update(actionType);
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
				var actionType = await _actionTypeRepository.FindByIdAsync(id);
				actionType.RecordStatus = Convert.ToBoolean(RecordStatus.Deleted);
				_actionTypeRepository.Update(actionType);
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