using DomainEntities.ExcelFormatAggregate;
using DomainEntities.Transaction;
using ExcelMapper;
using System.Collections.Generic;
using System.Linq;

namespace DomainApplication.ExcelMapper
{
	public class ExcelColumnMap : ExcelClassMap<TransactionDetailDto>
	{
		public ExcelColumnMap(List<ExcelFormatDetail> excelFormatDetails)
		{
			var amountMapColumn = excelFormatDetails.FirstOrDefault(p => p.Column == "Amount")?.MappedColumn;
			var sourcePanMapColumn = excelFormatDetails.FirstOrDefault(p => p.Column == "SourcePan")?.MappedColumn;
			var targetPanMapColumn = excelFormatDetails.FirstOrDefault(p => p.Column == "TargetPan")?.MappedColumn;
			var transDateMapColumn = excelFormatDetails.FirstOrDefault(p => p.Column == "TransactionDate")?.MappedColumn;
			var tranTimeMapColumn = excelFormatDetails.FirstOrDefault(p => p.Column == "TransactionTime")?.MappedColumn;
			var telMapColumn = excelFormatDetails.FirstOrDefault(p => p.Column == "Tel")?.MappedColumn;
			var ipMapColumn = excelFormatDetails.FirstOrDefault(p => p.Column == "IpAddress")?.MappedColumn;
			var userAgentMapColumn = excelFormatDetails.FirstOrDefault(p => p.Column == "UserAgent")?.MappedColumn;
			var statusMapColumn = excelFormatDetails.FirstOrDefault(p => p.Column == "Status")?.MappedColumn;
			var refNumbertMapColumn = excelFormatDetails.FirstOrDefault(p => p.Column == "RefNumber")?.MappedColumn;
			var applicationMapColumn = excelFormatDetails.FirstOrDefault(p => p.Column == "Application")?.MappedColumn;

			if (amountMapColumn != null) Map(m => m.Amount).WithColumnName(amountMapColumn);
			if (sourcePanMapColumn != null) Map(m => m.SourcePan).WithColumnName(sourcePanMapColumn);
			if (targetPanMapColumn != null) Map(m => m.TargetPan).WithColumnName(targetPanMapColumn);
			if (transDateMapColumn != null) Map(m => m.TransactionDateString).WithColumnName(transDateMapColumn);
			if (tranTimeMapColumn != null) Map(m => m.TransactionTime).WithColumnName(tranTimeMapColumn);
			if (telMapColumn != null) Map(m => m.Tel).WithColumnName(telMapColumn);
			if (ipMapColumn != null) Map(m => m.IpAddress).WithColumnName(ipMapColumn);
			if (userAgentMapColumn != null) Map(m => m.UserAgent).WithColumnName(userAgentMapColumn);
			if (statusMapColumn != null) Map(m => m.Status).WithColumnName(statusMapColumn);
			if (refNumbertMapColumn != null) Map(m => m.RefNumber).WithColumnName(refNumbertMapColumn);
			if (applicationMapColumn != null) Map(m => m.Application).WithColumnName(applicationMapColumn);
		}
	}
}
