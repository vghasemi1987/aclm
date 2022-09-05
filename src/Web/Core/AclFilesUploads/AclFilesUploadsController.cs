using ApplicationCommon;
using DomainContracts.AclFilesUploadAggregate;
using DomainContracts.Commons;
using DomainContracts.RouterAggregate;
using DomainEntities.AclFilesUploadAggregate;
using DomainEntities.Commons;
using KendoHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Web.Core.AclFilesUploads.ViewModels;
using Web.Extensions.Attributes;

namespace Web.Core.AclFilesUploads
{
	[Authorize]
	[DisplayName("بارگذاری فایل")]
	public class AclFilesUploadsController : Controller
	{
		private readonly IAclFilesUploadRepository _aclFilesUploadsRepository;
		private readonly IAclFileUploadService _aclFileUploadService;
		private readonly IRouterRepository _routerRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly /*IWebHostEnvironment*/IWebHostEnvironment _hostingEnvironment;
		public AclFilesUploadsController(IAclFilesUploadRepository aclFilesUploadsRepository,
										 IAclFileUploadService aclFilesUploadsService,
										 IRouterRepository routerRepository,
										 IWebHostEnvironment hostingEnvironment,
		IUnitOfWork unitOfWork)
		{
			_aclFilesUploadsRepository = aclFilesUploadsRepository;
			_aclFileUploadService = aclFilesUploadsService;
			_routerRepository = routerRepository;
			_hostingEnvironment = hostingEnvironment;
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
			var list = _aclFilesUploadsRepository.GetList(request);
			return Json(list);
		}

		[HttpGet]
		[Permission]
		[DisplayName("مشاهده جزییات فایل Acl")]
		public async Task<IActionResult> GetDetail(int id)
		{

			var model = new AclFilesUploadViewModel
			{
				RouterSelectList = new SelectList(await _routerRepository.GetDropDownList(),
				nameof(DropDownDto.Id), nameof(DropDownDto.Title))
			};
			if (id > 0)
			{
				//    var aclFilesUpload = await _aclFilesUploadsRepository.FindByIdAsync(id);
				//    model.Id = aclFilesUpload.Id;
				//    model.FileName = aclFilesUpload.FileName;
				//    model.Description = aclFilesUpload.Description;
				//    model.RouterId = aclFilesUpload.RouterId;
			}
			return PartialView("_Detail", model);
		}

		[HttpPost]
		[Permission]
		[DisplayName("افزودن")]
		public async Task<IActionResult> AddDetail(AclFilesUploadViewModel model, IFormFile file)
		{
			Commons.File.SaveFile($"{_hostingEnvironment.WebRootPath}\\uploads\\user-excel\\", file);

			var aclFilesUpload = new AclFilesUpload
			{
				FileName = model.FileName,
				Description = model.Description,
				RouterId = model.RouterId,
				RecordStatus = Convert.ToBoolean(RecordStatus.NotDeleted),
			};
			await _aclFileUploadService.UploadAclFile(aclFilesUpload, file);

			return Json(new
			{
				Message = Message.Show("اطلاعات مشتری با موفقیت ثبت شد...", MessageType.Success),
				RefreshGrid = true
			});
		}

		[HttpPost, ValidateAntiForgeryToken]
		[Permission]
		[DisplayName("ویرایش")]
		public async Task<IActionResult> EditDetail(ViewModels.AclFilesUploadViewModel model)
		{
			var aclFilesUpload = await _aclFilesUploadsRepository.FindByIdAsync(model.Id);

			aclFilesUpload.FileName = model.FileName;
			aclFilesUpload.Description = model.Description;
			aclFilesUpload.RouterId = model.RouterId;
			_aclFilesUploadsRepository.Update(aclFilesUpload);
			await _unitOfWork.SaveAsync();
			return Json(new
			{
				Message = Message.Show("اطلاعات  با موفقیت ویرایش شد...", MessageType.Success),
				RefreshGrid = true
			});
		}

		[HttpPost]
		[Permission]
		[DisplayName("حذف")]
		public async Task<IActionResult> DeleteRows(int id)
		{
			var aclFilesUpload = await _aclFilesUploadsRepository.FindByIdAsync(id);
			aclFilesUpload.RecordStatus = Convert.ToBoolean(RecordStatus.Deleted);
			_aclFilesUploadsRepository.Update(aclFilesUpload);
			await _unitOfWork.SaveAsync();
			return Ok();
		}


	}
}