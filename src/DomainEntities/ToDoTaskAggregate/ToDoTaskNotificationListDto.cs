namespace DomainEntities.ToDoTaskAggregate
{
	public class ToDoTaskNotificationListDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string DueDateTime { get; set; }
		public string Status { get; set; }
	}
}
