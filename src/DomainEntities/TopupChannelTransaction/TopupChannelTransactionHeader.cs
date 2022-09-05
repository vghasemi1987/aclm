namespace DomainEntities.TopupChannelTransaction
{
	public class TopupChannelTransactionHeaderListDto
	{
		public int Id { get; set; }
		public bool RecordStatus { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string UploadDate { get; set; }
		public int UserId { get; set; }
		public string UserName { get; set; }
	}
}
