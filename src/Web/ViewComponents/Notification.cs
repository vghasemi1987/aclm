using DomainContracts.NotificationAggregate;
using DomainContracts.ToDoTaskAggregate;
using DomainEntities.NotificationAggregate;
using DomainEntities.ToDoTaskAggregate;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Extensions;

namespace Web.ViewComponents
{
	public class Notification : ViewComponent
	{
		private readonly INotificationRepository _notificationRepository;
		private readonly IToDoTaskRepository _toDoTaskRepository;

		public Notification(INotificationRepository notificationRepository,
			IToDoTaskRepository toDoTaskRepository)
		{
			_notificationRepository = notificationRepository;
			_toDoTaskRepository = toDoTaskRepository;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var taskList = await _toDoTaskRepository.ListByUserIdForNotification(HttpContext.User.GetUserId());

			var notification = await _notificationRepository.GetByUserIdNotificationsAsync(HttpContext.User.GetUserId(), 30);

			return View(new Tuple<List<ToDoTaskNotificationListDto>, List<NotificationItem>>(taskList, notification));
		}
	}
}