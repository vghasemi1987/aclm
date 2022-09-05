using ApplicationCommon;
using DomainContracts.Commons;
using DomainContracts.DepartmentAggregate;
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
using Web.Core.Departments.ViewModels;
using Web.Extensions.Attributes;

namespace Web.Core.Departments
{
	[Authorize]
	[DisplayName("واحد")]
	public class DepartmentsController : Controller
	{
		private readonly IDepartmentRepository _departmentRepository;
		private readonly IUnitOfWork _unitOfWork;
		public DepartmentsController(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork)
		{
			_departmentRepository = departmentRepository;
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
			var list = _departmentRepository.GetList(request);
			return Json(list);
		}
		[HttpGet]
		[Permission]
		[DisplayName("مشاهده جزییات")]
		public async Task<IActionResult> GetDetail(int id)
		{
			var model = new DepartmentViewModel();
			if (id > 0)
			{
				var department = await _departmentRepository.FindByIdAsync(id);
				model.Id = department.Id;
				model.Name = department.Name;
			}
			return PartialView("_Detail", model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		[Permission]
		[DisplayName("افزودن")]
		public async Task<IActionResult> AddDetail(DepartmentViewModel model)
		{
			var department = new DomainEntities.DepartmentAggregate.Department
			{
				Name = model.Name,
				RecordStatus = Convert.ToBoolean(RecordStatus.NotDeleted),
			};

			_departmentRepository.Add(department);
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
		public async Task<IActionResult> EditDetail(DepartmentViewModel model)
		{
			var department = await _departmentRepository.FindByIdAsync(model.Id);

			department.Name = model.Name;
			_departmentRepository.Update(department);
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
				var department = await _departmentRepository.FindByIdAsync(id);
				department.RecordStatus = Convert.ToBoolean(RecordStatus.Deleted);
				_departmentRepository.Update(department);
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