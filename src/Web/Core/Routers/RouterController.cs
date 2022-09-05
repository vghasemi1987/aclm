using ApplicationCommon;
using DomainContracts.Commons;
using DomainContracts.RouterAggregate;
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
using Web.Core.Routers.ViewModels;
using Web.Extensions.Attributes;

namespace Web.Core.Routers
{
	[Authorize]
	[DisplayName("روتر")]
	public class RoutersController : Controller
	{
		private readonly IRouterRepository _routerRepository;
		private readonly IUnitOfWork _unitOfWork;
		public RoutersController(IRouterRepository routerRepository, IUnitOfWork unitOfWork)
		{
			_routerRepository = routerRepository;
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
			var list = _routerRepository.GetList(request);
			return Json(list);
		}
		[HttpGet]
		[Permission]
		[DisplayName("مشاهده جزییات")]
		public async Task<IActionResult> GetDetail(int id)
		{
			var model = new RouterViewModel();
			if (id > 0)
			{
				var router = await _routerRepository.FindByIdAsync(id);
				model.Id = router.Id;
				model.Name = router.Name;
				model.AccessNo = router.AccessNo;
			}
			return PartialView("_Detail", model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		[Permission]
		[DisplayName("افزودن")]
		public async Task<IActionResult> AddDetail(RouterViewModel model)
		{
			var router = new DomainEntities.RouterAggregate.Router
			{
				AccessNo = model.AccessNo,
				Name = model.Name,
				RecordStatus = Convert.ToBoolean(RecordStatus.NotDeleted),
			};

			_routerRepository.Add(router);
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
		public async Task<IActionResult> EditDetail(RouterViewModel model)
		{
			var router = await _routerRepository.FindByIdAsync(model.Id);

			router.Name = model.Name;
			router.AccessNo = model.AccessNo;
			_routerRepository.Update(router);
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
				var router = await _routerRepository.FindByIdAsync(id);
				router.RecordStatus = Convert.ToBoolean(RecordStatus.Deleted);
				_routerRepository.Update(router);
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