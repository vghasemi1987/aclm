using DomainEntities.TopupAccountExcelFormatAggregate;
using DomainEntities.TopupAccountTransaction;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DomainContracts.TopupAccountTransactionAggregate
{
	public interface ITopupAccountTransactionHeaderService
	{
		Task UploadExcelFile(TopupAccountTransactionHeader transactionHeader, int excelFormatId, IFormFile file, string webPath, TopupAccountFileFormat fileFormat);
	}
}
