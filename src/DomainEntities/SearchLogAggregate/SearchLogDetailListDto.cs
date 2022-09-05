using DomainEntities.Commons;

namespace DomainEntities.SearchLogAggregate
{
	public class SearchLogDetailListDto : Entity<int>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string NationalCode { get; set; }
		public string FatherName { get; set; }
		public string Address { get; set; }
		public string LetterIdentifier { get; set; }
		public string CardNumber { get; set; }
		public int UserId { get; set; }
		public string UserName { get; set; }
		public string SearchDate { get; set; }
		public string SearchTime { get; set; }
		public string IsSuccess { get; set; }
		public string IsVictim { get; set; }
	}
}
