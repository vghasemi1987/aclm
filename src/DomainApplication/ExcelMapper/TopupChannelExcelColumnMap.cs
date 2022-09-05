using DomainEntities.TopupChannelExcelFormatAggregate;
using DomainEntities.TopupChannelTransaction;
using ExcelMapper;
using System.Collections.Generic;
using System.Linq;

namespace DomainApplication.ExcelMapper
{
	public class TopupChannelExcelColumnMap : ExcelClassMap<TopupChannelTransactionDetail>
	{
		public TopupChannelExcelColumnMap(List<TopupChannelExcelFormatDetail> excelFormatDetails)
		{
			var FollowupCode = excelFormatDetails.FirstOrDefault(p => p.Column == "FollowupCode")?.MappedColumn;
			var CustomerAccountNumber = excelFormatDetails.FirstOrDefault(p => p.Column == "CustomerAccountNumber")?.MappedColumn;
			var FollowupCode2 = excelFormatDetails.FirstOrDefault(p => p.Column == "FollowupCode2")?.MappedColumn;
			var Extra1 = excelFormatDetails.FirstOrDefault(p => p.Column == "Extra1")?.MappedColumn;
			var Extra2 = excelFormatDetails.FirstOrDefault(p => p.Column == "Extra2")?.MappedColumn;
			var ChannelType = excelFormatDetails.FirstOrDefault(p => p.Column == "ChannelType")?.MappedColumn;
			var TransactionDate = excelFormatDetails.FirstOrDefault(p => p.Column == "TransactionDate")?.MappedColumn;
			var MobileNumber = excelFormatDetails.FirstOrDefault(p => p.Column == "MobileNumber")?.MappedColumn;
			var AmountText = excelFormatDetails.FirstOrDefault(p => p.Column == "AmountText")?.MappedColumn;
			var TransactionAmount = excelFormatDetails.FirstOrDefault(p => p.Column == "TransactionAmount")?.MappedColumn;
			var TransactionStatus = excelFormatDetails.FirstOrDefault(p => p.Column == "TransactionStatus")?.MappedColumn;
			var OperatorName = excelFormatDetails.FirstOrDefault(p => p.Column == "OperatorName")?.MappedColumn;

			if (FollowupCode != null) Map(m => m.FollowupCode).WithColumnIndex(int.Parse(FollowupCode) - 1);
			if (CustomerAccountNumber != null) Map(m => m.CustomerAccountNumber).WithColumnIndex(int.Parse(CustomerAccountNumber) - 1);
			if (FollowupCode2 != null) Map(m => m.FollowupCode2).WithColumnIndex(int.Parse(FollowupCode2) - 1);
			if (Extra1 != null) Map(m => m.Extra1).WithColumnIndex(int.Parse(Extra1) - 1);
			if (Extra2 != null) Map(m => m.Extra2).WithColumnIndex(int.Parse(Extra2) - 1);
			if (ChannelType != null) Map(m => m.ChannelType).WithColumnIndex(int.Parse(ChannelType) - 1);
			if (TransactionDate != null) Map(m => m.TransactionDate).WithColumnIndex(int.Parse(TransactionDate) - 1);
			if (MobileNumber != null) Map(m => m.MobileNumber).WithColumnIndex(int.Parse(MobileNumber) - 1);
			if (AmountText != null) Map(m => m.AmountText).WithColumnIndex(int.Parse(AmountText) - 1);
			if (TransactionAmount != null) Map(m => m.TransactionAmount).WithColumnIndex(int.Parse(TransactionAmount) - 1);
			if (TransactionStatus != null) Map(m => m.TransactionStatus).WithColumnIndex(int.Parse(TransactionStatus) - 1);
			if (OperatorName != null) Map(m => m.OperatorName).WithColumnIndex(int.Parse(OperatorName) - 1);
		}
	}
}
