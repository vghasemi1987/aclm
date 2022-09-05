using ApplicationCommon;
using DomainContracts.AclFilesRecordAggregate;
using DomainContracts.Commons;
using DomainContracts.RouterAggregate;
using DomainEntities.ApplicationUserAggregate;
using KendoHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel;
using Web.Extensions;
using Web.Extensions.Attributes;

namespace Web.Core.AclFilesRecords
{
	[Authorize]
	[DisplayName("Aclفایل")]
	public class AclFilesRecordsController : Controller
	{
		private readonly IAclFilesRecordRepository _aclFilesRecordRepository;
		private readonly IRouterRepository _routerRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<ApplicationUser> _userManager;
		public AclFilesRecordsController(IAclFilesRecordRepository aclFilesRecordsRepository,
										 IRouterRepository routerRepository,
										 IUnitOfWork unitOfWork,
										 UserManager<ApplicationUser> userManager)
		{
			_aclFilesRecordRepository = aclFilesRecordsRepository;
			_routerRepository = routerRepository;
			_unitOfWork = unitOfWork;
			_userManager = userManager;
		}
		[Permission]
		[DisplayName("لیست نمایشی")]
		public IActionResult Index(int aclFileUploadId)
		{
			return View(aclFileUploadId);
		}

		[HttpGet]
		public IActionResult GetRecords(string models, int aclFileRecordsId)
		{
			var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);
			var list = _aclFilesRecordRepository.GetList(request, aclFileRecordsId);
			return Json(list);
		}

		[Permission]
		[DisplayName("تایید رکوردها")]
		public IActionResult ConfirmRecords(int aclFilesUploadId)
		{
			_aclFilesRecordRepository.TransportAclRecords(aclFilesUploadId, User.GetUserId());
			return Json(new
			{
				Message = Message.Show("تایید رکوردها با موفقیت انجام شد...", MessageType.Success),
				RefreshGrid = true
			});
		}
	}
}