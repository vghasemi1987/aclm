
namespace DomainEntities.Transaction
{
	public class TransactionHeaderListDto
	{
		public int Id { get; set; }
		public bool RecordStatus { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string UploadDate { get; set; }
		public int UserId { get; set; }
		public string UserName { get; set; }
		public int CountTransactionDetails { get; set; }
	}
}
