namespace Web.Core.Api.ViewModel
{
	public class FilterDto
	{
		public int UserId { get; set; }
		public bool IsImmediate { get; set; }
		public DomainEntities.BroadcastAggregate.ReferralStatusBroadCastEnum Status { get; set; }
	}
}
