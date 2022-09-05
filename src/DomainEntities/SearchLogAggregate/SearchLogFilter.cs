namespace DomainEntities.SearchLogAggregate
{
	public class SearchLogFilter
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string NationalCode { get; set; }
		public string FatherName { get; set; }
		public string Address { get; set; }
		public string LetterIdentifier { get; set; }
		public string CardNumber { get; set; }
		public int UserId { get; set; }
		public string SearchDate { get; set; }
		public bool IsSuccess { get; set; }
		public bool IsVictim { get; set; }

	}
}
