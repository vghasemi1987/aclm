using ApplicationCommon;
using DomainContracts.Commons;
using DomainContracts.PositionAggregate;
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
using Web.Core.Positions.ViewModels;
using Web.Extensions.Attributes;

namespace Web.Core.Positions
{
	[Authorize]
	[DisplayName("جایگاه")]
	public class PositionsController : Controller
	{
		private readonly IPositionRepository _positionRepository;
		private readonly IUnitOfWork _unitOfWork;
		public PositionsController(IPositionRepository positionRepository, IUnitOfWork unitOfWork)
		{
			_positionRepository = positionRepository;
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
			var list = _positionRepository.GetList(request);
			return Json(list);
		}
		[HttpGet]
		[Permission]
		[DisplayName("مشاهده جزییات")]
		public async Task<IActionResult> GetDetail(int id)
		{
			var model = new PositionViewModel();
			if (id > 0)
			{
				var position = await _positionRepository.FindByIdAsync(id);
				model.Id = position.Id;
				model.Title = position.Title;
				model.Description = position.Description;
			}
			return PartialView("_Detail", model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		[Permission]
		[DisplayName("افزودن")]
		public async Task<IActionResult> AddDetail(PositionViewModel model)
		{
			var position = new DomainEntities.PositionAggregate.Position
			{
				Description = model.Description,
				Title = model.Title,
				RecordStatus = Convert.ToBoolean(RecordStatus.NotDeleted),
			};

			_positionRepository.Add(position);
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
		public async Task<IActionResult> EditDetail(PositionViewModel model)
		{
			var position = await _positionRepository.FindByIdAsync(model.Id);

			position.Title = model.Title;
			position.Description = model.Description;
			_positionRepository.Update(position);
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
				var position = await _positionRepository.FindByIdAsync(id);
				position.RecordStatus = Convert.ToBoolean(RecordStatus.Deleted);
				_positionRepository.Update(position);
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