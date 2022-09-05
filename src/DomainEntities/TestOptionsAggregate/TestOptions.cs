using DomainEntities.Commons;

namespace DomainEntities.TestOptionsAggregate
{
	public class TestOptions : Entity<short>
	{
		public TestOptionsTable TableId { get; set; }
		public string Title { get; set; }
	}

	public enum TestOptionsTable : short
	{
		Status = 1,
		AccessVector = 2,
		AccessComplexity = 3,
		Authentication = 4,
		ConfidentlyImpact = 5,
		IntegrityImpact = 6,
		AvailabilityImpact = 7,
		OwaspRiskRating = 8,
		UserTestStatus = 9
	}
}
