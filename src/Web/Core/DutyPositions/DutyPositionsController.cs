using ApplicationCommon;
using DomainContracts.Commons;
using DomainContracts.DutyPositionAggregate;
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
using Web.Core.DutyPositions.ViewModels;
using Web.Extensions.Attributes;

namespace Web.Core.DutyPositions
{
	[Authorize]
	[DisplayName("محل خدمت")]
	public class DutyPositionsController : Controller
	{
		private readonly IDutyPositionRepository _dutyPositionRepository;
		private readonly IUnitOfWork _unitOfWork;
		public DutyPositionsController(IDutyPositionRepository dutyPositionRepository, IUnitOfWork unitOfWork)
		{
			_dutyPositionRepository = dutyPositionRepository;
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
			var list = _dutyPositionRepository.GetList(request);
			return Json(list);
		}
		[HttpGet]
		[Permission]
		[DisplayName("مشاهده جزییات")]
		public async Task<IActionResult> GetDetail(int id)
		{
			var model = new DutyPositionViewModel();
			if (id > 0)
			{
				var dutyPosition = await _dutyPositionRepository.FindByIdAsync(id);
				model.Id = dutyPosition.Id;
				model.Title = dutyPosition.Title;
			}
			return PartialView("_Detail", model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		[Permission]
		[DisplayName("افزودن")]
		public async Task<IActionResult> AddDetail(DutyPositionViewModel model)
		{
			var dutyPosition = new DomainEntities.DutyPositionAggregate.DutyPosition
			{
				Title = model.Title,
				Code = model.Code,
				RecordStatus = Convert.ToBoolean(RecordStatus.NotDeleted),
			};

			_dutyPositionRepository.Add(dutyPosition);
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
		public async Task<IActionResult> EditDetail(DutyPositionViewModel model)
		{
			var dutyPosition = await _dutyPositionRepository.FindByIdAsync(model.Id);

			dutyPosition.Title = model.Title;
			dutyPosition.Code = model.Code;
			_dutyPositionRepository.Update(dutyPosition);
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
				var dutyPosition = await _dutyPositionRepository.FindByIdAsync(id);
				dutyPosition.RecordStatus = Convert.ToBoolean(RecordStatus.Deleted);
				_dutyPositionRepository.Update(dutyPosition);
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