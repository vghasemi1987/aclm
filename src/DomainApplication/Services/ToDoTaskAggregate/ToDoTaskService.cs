using ApplicationCommon;
using DomainContracts.Commons;
using DomainContracts.NotificationAggregate;
using DomainContracts.ToDoTaskAggregate;
using DomainEntities.NotificationAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainApplication.Services.ToDoTaskAggregate
{
	public class ToDoTaskService : IToDoTaskService
	{
		private readonly IToDoTaskRepository _taskRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly INotificationService _notificationService;

		public ToDoTaskService(IToDoTaskRepository taskRepository ,
			IUnitOfWork unitOfWork ,
			INotificationService notificationService)
		{
			_taskRepository = taskRepository;
			_unitOfWork = unitOfWork;
			_notificationService = notificationService;
		}

		public async Task DeleteAsync(int itemId , string fileLocation)
		{
			var items = new List<int> { itemId };
			if ( items.Any() )
			{
				await _taskRepository.Delete(items);
				await _unitOfWork.SaveAsync();
			}
		}

		public async Task DeleteAsync(IEnumerable<int> itemId , string fileLocation)
		{
			if ( itemId.Any() )
			{
				await _taskRepository.Delete(itemId);
				await _unitOfWork.SaveAsync();
			}
		}

		public async Task BackgroundTaskNotificationAsync()
		{
			//var andCriteria = new List<Predicate<ToDoTask>> {o => o.ToDoTaskStateId != 3 && o.ToDoTaskStateId != 4};
			//List<Predicate<ToDoTask>> orCriteria;
			//orCriteria.Add(c => c.IsMarried);

			//Expression<Func<ToDoTask, bool>> criteria=;
			//Expression<Func<ToDoTask, bool>> criteria =
			//    c => andCriteria.All(pred => pred(c));
			const string dateFormat = "yyyy/MM/dd HH:mm";
			var dateList = new List<object>
			{
				new
				{
					Date = DateTime.Now.ToString(dateFormat),
					Message = "وظیفه ای با عنوان @@@ هم اکنون می بایست انجام شود."
				},
				new
				{
					Date = DateTime.Now.AddHours(1).ToString(dateFormat),
					Message = "وظیفه ای با عنوان @@@ در یک ساعت آینده باید انجام شود."
				},
				new
				{
					Date = DateTime.Now.AddDays(1).ToString(dateFormat),
					Message = "وظیفه ای با عنوان @@@ در 24 ساعت آینده باید انجام شود."
				}
			};
			foreach ( var obj in dateList )
			{

				List<DomainEntities.ToDoTaskAggregate.ToDoTask> taskList =

					await _taskRepository
					.GetByCriteria(o =>
					o.DueDateTime.Value.ToString(dateFormat) == obj.GetObjectValue<string>("Date") &&
					o.StateId != 3 && o.StateId != 4);

				if ( !taskList.Any() ) continue;
				foreach ( var task in taskList )
				{
					_notificationService.AddNotification(task.CreatorUserId ,
						obj.GetObjectValue<string>("Message").Replace("@@@" , task.Title) , task.AssignedToUserId.GetValueOrDefault() ,
						NotificationType.ToDoTask , task.Id , CategoryEnum.DateOfDuty , true);
				}
				await _unitOfWork.SaveAsync();
			}
		}
	}
}