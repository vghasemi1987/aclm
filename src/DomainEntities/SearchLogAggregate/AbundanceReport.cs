namespace DomainEntities.SearchLogAggregate
{
	public class AbundanceReport
	{
		public int Id { get; set; }
		public string SuccessStatus { get; set; }
		public int UserId { get; set; }
		public string UserName { get; set; }
		public int Count { get; set; }
	}
}
