using DomainEntities.ApplicationUserAggregate;
using DomainEntities.Commons;
using System;
using System.Collections.Generic;

namespace DomainEntities.SearchLogAggregate
{
	public class SearchLogDetail : Entity<int>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string NationalCode { get; set; }
		public string FatherName { get; set; }
		public string Address { get; set; }
		public string LetterIdentifier { get; set; }
		public string CardNumber { get; set; }
		public int UserId { get; set; }
		public ApplicationUser User { get; set; }
		public DateTime SearchDate { get; set; }
		public string SearchTime { get; set; }
		public bool IsSuccess { get; set; }
		public bool IsVictim { get; set; }
		public ICollection<SearchLogTransactionDetail> SearchLogTransactionDetails { get; set; }
	}
}
