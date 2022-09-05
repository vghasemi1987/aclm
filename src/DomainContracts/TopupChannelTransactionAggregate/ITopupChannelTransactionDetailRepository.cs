using DomainContracts.Commons;
using DomainEntities.TopupChannelTransaction;
using KendoHelper;
using System.Collections.Generic;

namespace DomainContracts.TopupChannelTransactionAggregate
{
	public interface ITopupChannelTransactionDetailRepository : IRepository<TopupChannelTransactionDetail>, IAsyncRepository<TopupChannelTransactionDetail>
	{
		DataSourceResult GetList(DataSourceRequest request);
		DataSourceResult GetListByFilter(DataSourceRequest request, TopupChannelTransactionFilter filter);
		List<TopupChannelTransactionDetailListDto> GetListByFilter(TopupChannelTransactionFilter filter);
		DataSourceResult GetListByCardNumber(DataSourceRequest request, string cardNumber);
		void BulkInsert(List<TopupChannelTransactionDetail> transactionDetailList);
	}
}
