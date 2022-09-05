using ApplicationCommon;
using DomainContracts.Commons;
using DomainContracts.PolicyAggregate;
using DomainContracts.ProtocolAggregate;
using DomainEntities.Commons;
using KendoHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Web.Core.Policies.ViewModels;
using Web.Extensions.Attributes;

namespace Web.Core.Policies
{
	[Authorize]
	[DisplayName("قوانین")]
	public class PoliciesController : Controller
	{
		private readonly IPolicyRepository _policyRepository;
		private readonly IProtocolRepository _protocolRepository;
		private readonly IUnitOfWork _unitOfWork;
		public PoliciesController(IPolicyRepository policyRepository, IProtocolRepository protocolRepository, IUnitOfWork unitOfWork)
		{
			_policyRepository = policyRepository;
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
			var list = _policyRepository.GetList(request);
			return Json(list);
		}
		[HttpGet]
		[Permission]
		[DisplayName("مشاهده جزییات")]
		public async Task<IActionResult> GetDetail(int id)
		{
			var model = new PolicyViewModel
			{
				ProtocolSelectList = new SelectList(await _protocolRepository.GetDropDownList(),
				nameof(DropDownDto.Id), nameof(DropDownDto.Title))
			};
			if (id > 0)
			{
				var policies = await _policyRepository.FindByIdAsync(id);
				model.Id = policies.Id;
				model.Title = policies.Title;
				model.Description = policies.Description;
				model.ProtocolId = policies.ProtocolId;
			}
			return PartialView("_Detail", model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		[Permission]
		[DisplayName("افزودن")]
		public async Task<IActionResult> AddDetail(PolicyViewModel model)
		{
			var policies = new DomainEntities.PolicyAggregate.Policy
			{
				Description = model.Description,
				Title = model.Title,
				ProtocolId = model.ProtocolId,
				RecordStatus = Convert.ToBoolean(RecordStatus.NotDeleted),
			};

			_policyRepository.Add(policies);
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
		public async Task<IActionResult> EditDetail(PolicyViewModel model)
		{
			var policies = await _policyRepository.FindByIdAsync(model.Id);

			policies.Title = model.Title;
			policies.Description = model.Description;
			policies.ProtocolId = model.ProtocolId;
			_policyRepository.Update(policies);
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
				var policies = await _policyRepository.FindByIdAsync(id);
				policies.RecordStatus = Convert.ToBoolean(RecordStatus.Deleted);
				_policyRepository.Update(policies);
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