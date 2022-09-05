using DomainEntities.ExcelFormatAggregate;
using DomainEntities.Transaction;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DomainContracts.TransactionAggregate
{
	public interface ITransactionHeaderService
	{
		Task<string> UploadExcelFile(TransactionHeader transactionHeader, ExcelFormatHeader excelFormatHeader, IFormFile file, string webPath, string path);
	}
}
