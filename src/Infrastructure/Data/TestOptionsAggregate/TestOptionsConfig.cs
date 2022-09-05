using DomainEntities.TestOptionsAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.TestOptionsAggregate
{
	public class TestOptionsConfig : IEntityTypeConfiguration<TestOptions>
	{
		public void Configure(EntityTypeBuilder<TestOptions> builder)
		{
			builder.ToTable("Common_TestOptions");
			builder.HasKey(o => o.Id);

			builder.Property(o => o.Title)
				.IsUnicode()
				.HasMaxLength(100);

			builder.HasData(new TestOptions { Id = 1, Title = "Not Applicable", TableId = TestOptionsTable.Status },
				new TestOptions { Id = 2, Title = "Passes", TableId = TestOptionsTable.Status },
				new TestOptions { Id = 3, Title = "Failed", TableId = TestOptionsTable.Status },
				new TestOptions { Id = 4, Title = "Not Accessible", TableId = TestOptionsTable.Status },
				new TestOptions { Id = 5, Title = "Local-Adjacent Network", TableId = TestOptionsTable.AccessVector },
				new TestOptions { Id = 6, Title = "Network", TableId = TestOptionsTable.AccessVector },
				new TestOptions { Id = 7, Title = "High", TableId = TestOptionsTable.AccessComplexity },
				new TestOptions { Id = 8, Title = "Medium", TableId = TestOptionsTable.AccessComplexity },
				new TestOptions { Id = 9, Title = "Low", TableId = TestOptionsTable.AccessComplexity },

				new TestOptions { Id = 10, Title = "Multiple", TableId = TestOptionsTable.Authentication },
				new TestOptions { Id = 11, Title = "Single", TableId = TestOptionsTable.Authentication },
				new TestOptions { Id = 12, Title = "None", TableId = TestOptionsTable.Authentication },

				new TestOptions { Id = 13, Title = "None", TableId = TestOptionsTable.ConfidentlyImpact },
				new TestOptions { Id = 14, Title = "Partial", TableId = TestOptionsTable.ConfidentlyImpact },
				new TestOptions { Id = 15, Title = "Complete", TableId = TestOptionsTable.ConfidentlyImpact },

				new TestOptions { Id = 16, Title = "None", TableId = TestOptionsTable.IntegrityImpact },
				new TestOptions { Id = 17, Title = "Partial", TableId = TestOptionsTable.IntegrityImpact },
				new TestOptions { Id = 18, Title = "Complete", TableId = TestOptionsTable.IntegrityImpact },

				new TestOptions { Id = 19, Title = "None", TableId = TestOptionsTable.AvailabilityImpact },
				new TestOptions { Id = 20, Title = "Partial", TableId = TestOptionsTable.AvailabilityImpact },
				new TestOptions { Id = 21, Title = "Complete", TableId = TestOptionsTable.AvailabilityImpact },

				new TestOptions { Id = 22, Title = "Critical", TableId = TestOptionsTable.OwaspRiskRating },
				new TestOptions { Id = 23, Title = "High", TableId = TestOptionsTable.OwaspRiskRating },
				new TestOptions { Id = 24, Title = "Medium", TableId = TestOptionsTable.OwaspRiskRating },
				new TestOptions { Id = 25, Title = "Low", TableId = TestOptionsTable.OwaspRiskRating },
				new TestOptions { Id = 26, Title = "Information", TableId = TestOptionsTable.OwaspRiskRating },

				new TestOptions { Id = 27, Title = "تایید", TableId = TestOptionsTable.UserTestStatus },
				new TestOptions { Id = 28, Title = "عدم تایید", TableId = TestOptionsTable.UserTestStatus },
				new TestOptions { Id = 29, Title = "امکان تست وجود ندارد", TableId = TestOptionsTable.UserTestStatus }
				);
		}
	}
}