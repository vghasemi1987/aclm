using DomainEntities.TopupAccountExcelFormatAggregate;
using DomainEntities.TopupAccountTransaction;
using ExcelMapper;
using System.Collections.Generic;
using System.Linq;

namespace DomainApplication.ExcelMapper
{
	public class TopupAccountExcelColumnMap : ExcelClassMap<TopupAccountTransactionDetail>
	{
		public TopupAccountExcelColumnMap(List<TopupAccountExcelFormatDetail> excelFormatDetails)
		{
			var TransactionDate = excelFormatDetails.FirstOrDefault(p => p.Column == "TransactionDate")?.MappedColumn;
			var TransactionTime = excelFormatDetails.FirstOrDefault(p => p.Column == "TransactionTime")?.MappedColumn;
			var TransactionType = excelFormatDetails.FirstOrDefault(p => p.Column == "TransactionType")?.MappedColumn;
			var TransactionStatus = excelFormatDetails.FirstOrDefault(p => p.Column == "TransactionStatus")?.MappedColumn;
			var TransactionAmountText = excelFormatDetails.FirstOrDefault(p => p.Column == "TransactionAmountText")?.MappedColumn;
			var TransactionAmount = excelFormatDetails.FirstOrDefault(p => p.Column == "TransactionAmount")?.MappedColumn;
			var RefrenceCode = excelFormatDetails.FirstOrDefault(p => p.Column == "RefrenceCode")?.MappedColumn;
			var Extra1 = excelFormatDetails.FirstOrDefault(p => p.Column == "Extra1")?.MappedColumn;
			var Extra2 = excelFormatDetails.FirstOrDefault(p => p.Column == "Extra2")?.MappedColumn;
			var Extra3 = excelFormatDetails.FirstOrDefault(p => p.Column == "Extra3")?.MappedColumn;
			var Extra4 = excelFormatDetails.FirstOrDefault(p => p.Column == "Extra4")?.MappedColumn;
			var BranchCode = excelFormatDetails.FirstOrDefault(p => p.Column == "BranchCode")?.MappedColumn;
			var FollowupCode2 = excelFormatDetails.FirstOrDefault(p => p.Column == "FollowupCode2")?.MappedColumn;
			var AccountNumber = excelFormatDetails.FirstOrDefault(p => p.Column == "AccountNumber")?.MappedColumn;
			var CustomerAccountNumber = excelFormatDetails.FirstOrDefault(p => p.Column == "CustomerAccountNumber")?.MappedColumn;
			var FollowupCode = excelFormatDetails.FirstOrDefault(p => p.Column == "FollowupCode")?.MappedColumn;

			if (TransactionDate != null) Map(m => m.TransactionDate).WithColumnIndex(int.Parse(TransactionDate) - 1);
			if (TransactionTime != null) Map(m => m.TransactionTime).WithColumnIndex(int.Parse(TransactionTime) - 1);
			if (TransactionType != null) Map(m => m.TransactionType).WithColumnIndex(int.Parse(TransactionType) - 1);
			if (TransactionStatus != null) Map(m => m.TransactionStatus).WithColumnIndex(int.Parse(TransactionStatus) - 1);
			if (TransactionAmountText != null) Map(m => m.TransactionAmountText).WithColumnIndex(int.Parse(TransactionAmountText) - 1);
			if (TransactionAmount != null) Map(m => m.TransactionAmount).WithColumnIndex(int.Parse(TransactionAmount) - 1);
			if (RefrenceCode != null) Map(m => m.RefrenceCode).WithColumnIndex(int.Parse(RefrenceCode) - 1);
			if (Extra1 != null) Map(m => m.Extra1).WithColumnIndex(int.Parse(Extra1) - 1);
			if (Extra2 != null) Map(m => m.Extra2).WithColumnIndex(int.Parse(Extra2) - 1);
			if (Extra3 != null) Map(m => m.Extra3).WithColumnIndex(int.Parse(Extra3) - 1);
			if (Extra4 != null) Map(m => m.Extra4).WithColumnIndex(int.Parse(Extra4) - 1);
			if (BranchCode != null) Map(m => m.BranchCode).WithColumnIndex(int.Parse(BranchCode) - 1);
			if (FollowupCode2 != null) Map(m => m.FollowupCode2).WithColumnIndex(int.Parse(FollowupCode2) - 1);
			if (AccountNumber != null) Map(m => m.AccountNumber).WithColumnIndex(int.Parse(AccountNumber) - 1);
			if (CustomerAccountNumber != null) Map(m => m.CustomerAccountNumber).WithColumnIndex(int.Parse(CustomerAccountNumber) - 1);
			if (FollowupCode != null) Map(m => m.FollowupCode).WithColumnIndex(int.Parse(FollowupCode) - 1);
		}
	}
}
