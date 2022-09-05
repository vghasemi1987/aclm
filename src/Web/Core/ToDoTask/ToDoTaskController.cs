using ApplicationCommon;
using DomainContracts.Commons;
using DomainContracts.NotificationAggregate;
using DomainContracts.SettingAggregate;
using DomainContracts.ToDoTaskAggregate;
using DomainEntities.ApplicationUserAggregate;
using DomainEntities.NotificationAggregate;
using DomainEntities.SettingAggregate;
using DomainEntities.ToDoTaskAggregate;
using KendoHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Web.Core.ToDoTask.ViewModels;
using Web.Extensions;
using Web.Extensions.Attributes;

namespace Web.Core.ToDoTask
{
	[Authorize]
	[DisplayName("مدیریت وظایف")]
	public class ToDoTaskController : Controller
	{
		private readonly IToDoTaskRepository _taskRepository;
		private readonly ITaskStateService _taskStateService;
		private readonly IPriorityService _priorityRepository;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IWebHostEnvironment _environment;
		private readonly IFileService _fileService;
		private readonly IToDoTaskService _toDoTaskService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly INotificationService _notificationService;

		public ToDoTaskController(IToDoTaskRepository taskRepository,
			ITaskStateService taskStateService,
			IPriorityService priorityRepository,
			UserManager<ApplicationUser> userManager,
			IWebHostEnvironment environment,
			IFileService fileService,
			IToDoTaskService toDoTaskService,
			IUnitOfWork unitOfWork,
			INotificationService notificationService)
		{
			_taskRepository = taskRepository;
			_taskStateService = taskStateService;
			_priorityRepository = priorityRepository;
			_userManager = userManager;
			_environment = environment;
			_fileService = fileService;
			_toDoTaskService = toDoTaskService;
			_unitOfWork = unitOfWork;
			_notificationService = notificationService;
		}
		[Permission]
		[DisplayName("لیست نمایشی")]
		public IActionResult Index(SearchViewModel model)
		{
			var where = string.Empty;
			if (model.AssignedToUserId != null)
				where += $" {model.Oper} AssignedToUserId In ({string.Join(",", model.AssignedToUserId)})";
			if (model.CreatorUserId != null)
				where += $" {model.Oper} CreatorUserId In ({string.Join(",", model.CreatorUserId)})";
			if (model.PriorityId != null)
				where += $" {model.Oper} PriorityId In ({string.Join(",", model.PriorityId)})";
			if (model.StateId != null)
				where += $" {model.Oper} StateId In ({string.Join(",", model.StateId)})";

			if (!string.IsNullOrEmpty(model.FromDueDateTime))
				where += $" {model.Oper} DueDateTime >= '{model.FromDueDateTime.ToMiladiDate()}'";
			if (!string.IsNullOrEmpty(model.ToDueDateTime))
				where += $" {model.Oper} DueDateTime <= '{model.ToDueDateTime.ToMiladiDate()}'";
			if (!string.IsNullOrEmpty(model.Description))
				where += $" {model.Oper} Description Like N'%{model.Description}%'";
			if (!string.IsNullOrEmpty(model.Title))
				where += $" {model.Oper} Title Like N'%{model.Title}%'";
			if (!string.IsNullOrEmpty(model.ContactName))
				where += $" {model.Oper} ContactId In (Select Id From Contacts Where Concat(FirstName,' ', LastName) Like N'%{model.ContactName}%')";

			if (!string.IsNullOrEmpty(model.FromEntryDate))
				where += $" {model.Oper} EntryDateTime >= '{model.FromEntryDate.ToMiladiDate()}'";
			if (!string.IsNullOrEmpty(model.ToEntryDate))
				where += $" {model.Oper} EntryDateTime <= '{model.ToEntryDate.ToMiladiDate()}'";

			if (!string.IsNullOrEmpty(model.FromCompletionDateTime))
				where += $" {model.Oper} CompletionDateTime >= '{model.FromCompletionDateTime.ToMiladiDate()}'";
			if (!string.IsNullOrEmpty(model.ToCompletionDateTime))
				where += $" {model.Oper} CompletionDateTime <= '{model.ToCompletionDateTime.ToMiladiDate()}'";

			if (string.IsNullOrEmpty(where)) return View();
			var query =
				$"Select DISTINCT * From ToDoTasks Where {where.Remove(0, 4)}";
			return View((object)query);
		}

		[HttpGet]
		public IActionResult GetRecords(string models, string query)
		{
			var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);
			var list = _taskRepository.GetList(request);
			return Json(list);
		}

		[HttpGet]
		[Permission]
		[DisplayName("مشاهده جزئیات")]
		public async Task<IActionResult> GetDetail(int? id)
		{
			if (!id.HasValue)
				return NoContent();

			var task = await _taskRepository.FindByIdAsync(id.Value);
			if (task == null)
			{
				return NoContent();
			}

			var taskViewModel = new FormViewModel
			{
				Description = task.Description,
				DueDateTime = task.DueDateTime.HasValue ? task.DueDateTime.Value.ToPersianDateTime("yyyy/MM/dd HH:mm") : string.Empty,
				Title = task.Title,
				PriorityId = task.PriorityId.GetValueOrDefault(),
				StateId = task.StateId.GetValueOrDefault(),
				StateSelectList = new SelectList(await _taskStateService.GetAllAsync(), nameof(State.Id), nameof(State.Title)),
				Users = new SelectList(_userManager.Users.ToList(), nameof(ApplicationUser.Id), nameof(ApplicationUser.Name), task.AssignedToUserId),
				Priority = new SelectList(await _priorityRepository.GetAllAsync(), nameof(Priority.Id), nameof(Priority.Title), task.PriorityId),
				Id = task.Id,
			};
			return PartialView("_Detail", taskViewModel);
		}

		[HttpGet]
		[Permission]
		[DisplayName("جزئیات")]
		public IActionResult Detail(int? id)
		{
			if (!id.HasValue)
			{
				return NoContent();
			}

			var taskDetail = _taskRepository.GetSingleBySpec(o => o.Id.Equals(id.Value));
			if (taskDetail == null)
				return NotFound();
			var dataList = new Dictionary<string, string>
			{
				{"Id", taskDetail.Id.ToString()},
				{"Title", taskDetail.Title}
			};
			return View("ToDoTask", dataList);
		}

		[HttpGet]
		[Permission]
		public async Task<IActionResult> ShowDetail(int? id)
		{
			if (!id.HasValue)
			{
				return NoContent();
			}

			var taskDetail = await _taskRepository.FindByIdAsync(id.Value);

			return PartialView("_ShowDetail", taskDetail);
		}

		[HttpGet]
		[Permission]
		[DisplayName("افزودن")]
		public async Task<IActionResult> AddDetail(int? followId, int? contactId, int? contractId)
		{
			var taskViewModel = new FormViewModel
			{
				StateSelectList = new SelectList(await _taskStateService.GetAllAsync(), nameof(State.Id), nameof(State.Title)),
				Users = new SelectList(_userManager.Users.ToList(), nameof(ApplicationUser.Id), nameof(ApplicationUser.Name), User.GetUserId()),
				Priority = new SelectList(await _priorityRepository.GetAllAsync(), nameof(Priority.Id), nameof(Priority.Title)),
				PriorityId = 2,
				StateId = 1
			};
			return PartialView("_Detail", taskViewModel);
		}

		[HttpPost, ValidateAntiForgeryToken]
		[Permission]
		[DisplayName("افزودن")]
		public async Task<IActionResult> AddDetail(FormViewModel model)
		{
			var userId = User.GetUserId();
			var taskItem = new DomainEntities.ToDoTaskAggregate.ToDoTask
			{
				CreatorUserId = userId,
				Description = model.Description,
				DueDateTime = model.DueDateTime.ToMiladiDate(),
				Title = model.Title,
				PriorityId = model.PriorityId,
				StateId = model.StateId,
				AssignedToUserId = model.AssignedToUserId,
			};

			_taskRepository.Add(taskItem);
			await _unitOfWork.SaveAsync();

			var state = _notificationService.AddNotification(userId,
				$"{User.GetUserFullName()} وظیفه ای با عنوان {model.Title} به شما اختصاص داد.",
				taskItem.AssignedToUserId.GetValueOrDefault(), NotificationType.ToDoTask, taskItem.Id, CategoryEnum.PostedTask);
			if (state) await _unitOfWork.SaveAsync();

			return Json(new
			{
				Message = Message.Show("اطلاعات با موفقیت ثبت شد...", MessageType.Success),
				RefreshTaskGrid = true
			});
		}

		[HttpPost, ValidateAntiForgeryToken]
		[Permission]
		[DisplayName("ویرایش")]
		public async Task<IActionResult> EditDetail(FormViewModel model)
		{
			var userId = User.GetUserId();
			var taskItem = _taskRepository.GetSingleBySpec(o => o.Id.Equals(model.Id));
			taskItem.Id = model.Id;
			taskItem.Description = model.Description;
			taskItem.DueDateTime = model.DueDateTime.ToMiladiDate();
			taskItem.Title = model.Title;
			taskItem.PriorityId = model.PriorityId;
			taskItem.StateId = model.StateId;
			taskItem.AssignedToUserId = model.AssignedToUserId;

			if (model.StateId == 3)
				taskItem.CompletionDateTime = DateTime.Now;

			_taskRepository.Update(taskItem);
			await _unitOfWork.SaveAsync();

			var state = _notificationService.AddNotification(userId,
				$"{User.GetUserFullName()} وظیفه ای با عنوان {model.Title} به شما اختصاص داد.",
				taskItem.AssignedToUserId.GetValueOrDefault(), NotificationType.ToDoTask, taskItem.Id, CategoryEnum.PostedTask);
			if (state) await _unitOfWork.SaveAsync();

			return Json(new
			{
				Message = Message.Show("اطلاعات با موفقیت ثبت شد...", MessageType.Success),
				RefreshTaskGrid = true
			});
		}
		[HttpGet]
		[Permission]
		[DisplayName("تغییر وضعیت")]
		public async Task<IActionResult> ChangeTaskState(int? toDoTaskId)
		{
			var taskDetail = _taskRepository.GetSingleBySpec(o => o.Id.Equals(toDoTaskId.GetValueOrDefault()));//FindByIdAsync(toDoTaskId.GetValueOrDefault());
			if (taskDetail == null)
				return NoContent();

			var taskState = new ChangeStateViewModel
			{
				Status = new SelectList(await _taskStateService.GetAllAsync(),
					nameof(State.Id), nameof(State.Title)),
				StateId = taskDetail.StateId.GetValueOrDefault(),
				Id = taskDetail.Id
			};
			return PartialView("_ChangeTaskState", taskState);
		}

		[HttpPost, ValidateAntiForgeryToken]
		[Permission]
		public async Task<IActionResult> ChangeTaskState(ChangeStateViewModel model)
		{
			var task = await _taskRepository.FindByIdAsync(model.Id);
			task.StateId = model.StateId;
			if (task.StateId == 3)
			{
				task.CompletionDateTime = DateTime.Now;
				_notificationService.AddNotification(User.GetUserId(),
					$"وظیفه اختصاص داده شده توسط {User.GetUserFullName()} انجام شد.",
					task.CreatorUserId, NotificationType.ToDoTask, task.Id, CategoryEnum.ChangeTaskState);
			}
			else
			{
				task.CompletionDateTime = null;
			}

			_taskRepository.Update(task, o => o.StateId, o => o.CompletionDateTime);
			await _unitOfWork.SaveAsync();
			return Json(new
			{
				Message = Message.Show("تغییر وضعیت با موفقیت انجام شد...", MessageType.Success)
			});
		}

		public async Task<IActionResult> TaskList(int followId)
		{
			var follows = (await _taskRepository.ListByFollowIdAsync(followId)).Select(x => new ToDoTaskListViewModel
			{
				DueDateTime = x.DueDateTime.GetValueOrDefault(),
				Status = x.State?.Title,
				Title = x.Title,
				Priority = x.Priority?.Title,
				User = x.AssignedToUser?.Name,
				Id = x.Id,
			}).ToList();
			return PartialView("_TaskList", follows);
		}

		public async Task<IActionResult> UncompletedTaskList(int? followId, int? contractId, int? contactId)
		{
			List<DomainEntities.ToDoTaskAggregate.ToDoTask> follows;
			if (followId.HasValue)
			{
				follows = await _taskRepository.ListByFollowIdAsync(followId.GetValueOrDefault());

			}
			else if (contractId.HasValue)
			{
				follows = await _taskRepository.ListByContractIdAsync(contractId.GetValueOrDefault());
			}
			else
			{
				follows = await _taskRepository.ListByContactIdAsync(contactId.GetValueOrDefault());
			}

			var list = follows.Where(o => o.StateId != 3)
				.Select(x => new ToDoTaskListViewModel
				{
					DueDateTime = x.DueDateTime.GetValueOrDefault(),
					Status = x.State?.Title,
					Title = x.Title,
					Priority = x.Priority?.Title,
					User = x.AssignedToUser?.Name,
					Id = x.Id,
				}).ToList();
			return PartialView("_TaskList", list);
		}
		public async Task<IActionResult> CompleteTaskList(int? followId, int? contractId, int? contactId)
		{
			List<DomainEntities.ToDoTaskAggregate.ToDoTask> follows;
			if (followId.HasValue)
				follows = await _taskRepository.ListByFollowIdAsync(followId.GetValueOrDefault());
			else if (contractId.HasValue)
				follows = await _taskRepository.ListByContractIdAsync(contractId.GetValueOrDefault());
			else
				follows = await _taskRepository.ListByContactIdAsync(contactId.GetValueOrDefault());

			var list = follows.Where(o => o.StateId == 3)
				.Select(x => new ToDoTaskListViewModel
				{
					DueDateTime = x.DueDateTime.GetValueOrDefault(),
					Status = x.State?.Title,
					Title = x.Title,
					Priority = x.Priority?.Title,
					User = x.AssignedToUser?.Name,
					Id = x.Id,
				}).ToList();
			return PartialView("_TaskList", list);
		}

		[HttpDelete]
		[Permission]
		[DisplayName("حذف")]
		public async Task<IActionResult> Delete(int? id, string path)
		{
			if (!id.HasValue) return Json(false);
			await _toDoTaskService.DeleteAsync(id.GetValueOrDefault(),
				$"{_environment.WebRootPath}\\uploads\\user-content\\");
			if (path.Contains(nameof(ShowDetail).ToLower()))
			{
				return Json(new
				{
					Message = Message.Show("وظیفه انتخابی حذف شد...", MessageType.Success),
					Redirect = Url.Action(nameof(Index))
				});
			}

			return Json(new
			{
				Message = Message.Show("وظیفه انتخابی حذف شد...", MessageType.Success),
				RefreshTaskGrid = true
			});
		}

		[HttpDelete]
		[Permission]
		public virtual async Task<ActionResult> DeleteRows(string[] ids)
		{
			if (!ids.Any()) return Json(false);
			await _toDoTaskService.DeleteAsync(ids.Select(int.Parse).ToList(),
				$"{_environment.WebRootPath}\\uploads\\user-content\\");
			//await _unitOfWork.SaveAsync();
			return Ok();
		}

		[DisplayName("جستجوی پیشرفته")]
		[Permission]
		public async Task<IActionResult> Search()
		{
			var viewModel = new SearchViewModel
			{
				StateSelectList = new SelectList(await _taskStateService.GetAllAsync(), nameof(State.Id), nameof(State.Title)),
				Users = new SelectList(_userManager.Users.ToList(), nameof(ApplicationUser.Id), nameof(ApplicationUser.Name)),
				Priority = new SelectList(await _priorityRepository.GetAllAsync(), nameof(Priority.Id), nameof(Priority.Title))
			};
			return View("Search", viewModel);
		}
	}
}