using ApplicationCommon;
using DomainContracts.Commons;
using DomainContracts.ServiceLevelAggregate;
using DomainEntities.Commons;
using KendoHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Web.Core.ServiceLevels.ViewModels;
using Web.Extensions.Attributes;

namespace Web.Core.ServiceLevels
{
	[Authorize]
	[DisplayName("طبقه بندی سرویس")]
	public class ServiceLevelsController : Controller
	{
		private readonly IServiceLevelRepository _serviceLevelRepository;
		private readonly IUnitOfWork _unitOfWork;
		public ServiceLevelsController(IServiceLevelRepository serviceLevelRepository, IUnitOfWork unitOfWork)
		{
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
			var list = _serviceLevelRepository.GetList(request);
			return Json(list);
		}
		[HttpGet]
		[Permission]
		[DisplayName("مشاهده جزییات")]
		public async Task<IActionResult> GetDetail(int id)
		{
			var model = new ServiceLevelViewModel();
			if (id > 0)
			{
				var serviceLevel = await _serviceLevelRepository.FindByIdAsync(id);
				model.Id = serviceLevel.Id;
				model.Title = serviceLevel.Title;
			}
			return PartialView("_Detail", model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		[Permission]
		[DisplayName("افزودن")]
		public async Task<IActionResult> AddDetail(ServiceLevelViewModel model)
		{
			var serviceLevel = new DomainEntities.ServiceLevelAggregate.ServiceLevel
			{
				Title = model.Title,
				RecordStatus = Convert.ToBoolean(RecordStatus.NotDeleted),
			};

			_serviceLevelRepository.Add(serviceLevel);
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
		public async Task<IActionResult> EditDetail(ServiceLevelViewModel model)
		{
			var serviceLevel = await _serviceLevelRepository.FindByIdAsync(model.Id);

			serviceLevel.Title = model.Title;
			_serviceLevelRepository.Update(serviceLevel);
			await _unitOfWork.SaveAsync();
			return Json(new
			{
				Message = Message.Show("اطلاعات مشتری با موفقیت ویرایش شد...", MessageType.Success),
				RefreshGrid = true
			});
		}

		[HttpPost]
		[Permission]
		[DisplayName("حذف")]
		public async Task<IActionResult> DeleteRows(int id)
		{
			var serviceLevel = await _serviceLevelRepository.FindByIdAsync(id);
			serviceLevel.RecordStatus = Convert.ToBoolean(RecordStatus.Deleted);
			_serviceLevelRepository.Update(serviceLevel);
			await _unitOfWork.SaveAsync();
			return Ok();
		}
	}
}