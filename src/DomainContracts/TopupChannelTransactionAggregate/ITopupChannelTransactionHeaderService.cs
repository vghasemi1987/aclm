using DomainEntities.TopupChannelExcelFormatAggregate;
using DomainEntities.TopupChannelTransaction;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DomainContracts.TopupChannelTransactionAggregate
{
	public interface ITopupChannelTransactionHeaderService
	{
		Task UploadExcelFile(TopupChannelTransactionHeader transactionHeader, int excelFormatId, IFormFile file, string webPath, TopupChannelFileFormat fileFormat);
	}
}
