using DomainContracts.Commons;
using DomainEntities.TopupAccountTransaction;
using KendoHelper;
using System.Collections.Generic;

namespace DomainContracts.TopupAccountTransactionAggregate
{
	public interface ITopupAccountTransactionDetailRepository : IRepository<TopupAccountTransactionDetail>, IAsyncRepository<TopupAccountTransactionDetail>
	{
		DataSourceResult GetList(DataSourceRequest request);
		DataSourceResult GetListByFilter(DataSourceRequest request, TopupAccountTransactionFilter filter);
		List<TopupAccountTransactionDetailListDto> GetListByFilter(TopupAccountTransactionFilter filter);
		DataSourceResult GetListByCardNumber(DataSourceRequest request, string cardNumber);
		void BulkInsert(List<TopupAccountTransactionDetail> transactionDetailList);
	}
}
