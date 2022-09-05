using ApplicationCommon;
using DomainContracts.Commons;
using DomainContracts.ProtocolAggregate;
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
using Web.Core.Protocols.ViewModels;
using Web.Extensions.Attributes;

namespace Web.Core.Protocols
{
	[Authorize]
	[DisplayName("پروتکل")]
	public class ProtocolsController : Controller
	{
		private readonly IProtocolRepository _protocolRepository;
		private readonly IUnitOfWork _unitOfWork;
		public ProtocolsController(IProtocolRepository protocolRepository, IUnitOfWork unitOfWork)
		{
			_protocolRepository = protocolRepository;
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
			var list = _protocolRepository.GetList(request);
			return Json(list);
		}
		[HttpGet]
		[Permission]
		[DisplayName("مشاهده جزییات")]
		public async Task<IActionResult> GetDetail(int id)
		{
			var model = new ProtocolViewModel();
			if (id > 0)
			{
				var protocol = await _protocolRepository.FindByIdAsync(id);
				model.Id = protocol.Id;
				model.Name = protocol.Name;
				model.Description = protocol.Description;
			}
			return PartialView("_Detail", model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		[Permission]
		[DisplayName("افزودن")]
		public async Task<IActionResult> AddDetail(ProtocolViewModel model)
		{
			var protocol = new DomainEntities.ProtocolAggregate.Protocol
			{
				Description = model.Description,
				Name = model.Name,
				RecordStatus = Convert.ToBoolean(RecordStatus.NotDeleted),
			};

			_protocolRepository.Add(protocol);
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
		public async Task<IActionResult> EditDetail(ProtocolViewModel model)
		{
			var protocol = await _protocolRepository.FindByIdAsync(model.Id);

			protocol.Name = model.Name;
			protocol.Description = model.Description;
			_protocolRepository.Update(protocol);
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
				var protocol = await _protocolRepository.FindByIdAsync(id);
				protocol.RecordStatus = Convert.ToBoolean(RecordStatus.Deleted);
				_protocolRepository.Update(protocol);
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