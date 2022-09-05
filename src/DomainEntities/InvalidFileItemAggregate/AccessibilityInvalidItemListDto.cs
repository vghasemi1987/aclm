namespace DomainEntities.InvalidFileItemAggregate
{
	public class InvalidFileItemListDto
	{
		public int Id { get; set; }
		public bool RecordStatus { get; set; }
		public string InvalidItemTitle { get; set; }
		public int LineNumber { get; set; }
		public int AclFilesUploadId { get; set; }
		public string AclFilesUploadTitle { get; set; }
	}
}
