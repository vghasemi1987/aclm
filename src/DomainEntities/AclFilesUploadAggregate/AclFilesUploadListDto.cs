namespace DomainEntities.AclFilesUploadAggregate
{
	public class AclFilesUploadListDto
	{
		public int Id { get; set; }
		public bool RecordStatus { get; set; }
		public string FileName { get; set; }
		public string Description { get; set; }
		public int? RouterId { get; set; }
		public string RouterTitle { get; set; }
	}
}
