using DomainEntities.BankBranchAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.BankBranchAggregate
{
	public class BankBranchConfig : IEntityTypeConfiguration<BankBranch>
	{
		public void Configure(EntityTypeBuilder<BankBranch> builder)
		{
			builder.ToTable("BankBranches");
			builder.HasKey(ci => ci.Id);

			builder.Property(current => current.Id)
				.ValueGeneratedNever();

			builder.Property(current => current.Title)
				.IsRequired()
				.HasMaxLength(50)
				.IsUnicode();

			builder.HasOne(x => x.BranchHead)
				.WithMany(x => x.SubBranchHeads)
				.HasForeignKey(x => x.BranchHeadId);

			//builder.HasOne(ur => ur.City).WithMany().HasForeignKey(r => r.CityId).OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasData(
				new BankBranch { Id = 515, Title = "اداره منطقه یک تهران" },
				new BankBranch { Id = 516, Title = "اداره منطقه دو تهران" },
				new BankBranch { Id = 510, Title = "اداره منطقه سه تهران" },
				new BankBranch { Id = 107, Title = "شعبه مستقل بلوار" },
				new BankBranch { Id = 164, Title = "صندوق توسعه ملی" },
				new BankBranch { Id = 101, Title = "شعبه مستقل مرکزی" },
				new BankBranch { Id = 500, Title = "اداره امور شعب خراسان رضوی" },
				new BankBranch { Id = 503, Title = "اداره امور شعب فارس" },
				new BankBranch { Id = 504, Title = "اداره امور شعب گیلان" },
				new BankBranch { Id = 507, Title = "اداره امور شعب مازندران" },
				new BankBranch { Id = 509, Title = "اداره امور شعب کرمان" },
				new BankBranch { Id = 512, Title = "اداره امور شعب سیستان و بلوچستان" },
				new BankBranch { Id = 513, Title = "اداره امور شعب یزد" },
				new BankBranch { Id = 517, Title = "اداره امور شعب سمنان" },
				new BankBranch { Id = 519, Title = "اداره امور شعب گلستان" },
				new BankBranch { Id = 524, Title = "اداره امور شعب هرمزگان" },
				new BankBranch { Id = 528, Title = "اداره امور شعب چهارمحال بختیاری" },
				new BankBranch { Id = 529, Title = "اداره امور شعب کهگلویه و بویراحمد" },
				new BankBranch { Id = 622, Title = "اداره امور شعب خراسان شمالی" },
				new BankBranch { Id = 624, Title = "اداره امور شعب جنوبی" },
				new BankBranch { Id = 642, Title = "اداره امور شعب بوشهر" },
				new BankBranch { Id = 501, Title = "اداره امور شعب آذربایجان شرقی" },
				new BankBranch { Id = 502, Title = "اداره امور شعب اصفهان" },
				new BankBranch { Id = 505, Title = "اداره امور شعب کرمانشاه" },
				new BankBranch { Id = 506, Title = "اداره امور شعب خوزستان" },
				new BankBranch { Id = 508, Title = "اداره امور شعب مرکزی" },
				new BankBranch { Id = 514, Title = "اداره امور شعب آذربایجان غربی" },
				new BankBranch { Id = 518, Title = "اداره امور شعب اردبیل" },
				new BankBranch { Id = 520, Title = "اداره امور شعب قزوین" },
				new BankBranch { Id = 521, Title = "اداره امور شعب کردستان" },
				new BankBranch { Id = 522, Title = "اداره امور شعب همدان" },
				new BankBranch { Id = 523, Title = "اداره امور شعب لرستان" },
				new BankBranch { Id = 525, Title = "اداره امور شعب زنجان" },
				new BankBranch { Id = 526, Title = "اداره امور شعب البرز" },
				new BankBranch { Id = 527, Title = "اداره امور شعب قم" },
				new BankBranch { Id = 750, Title = "اداره امور شعب ایلام" },
				new BankBranch { Id = 21, Title = "اداره خزانه داری" }
			);
		}
	}
}
