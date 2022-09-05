using DomainContracts.Commons;
using DomainEntities.Transaction;
using KendoHelper;
using System.Collections.Generic;

namespace DomainContracts.TransactionAggregate
{
	public interface ITransactionDetailRepository : IRepository<TransactionDetail>, IAsyncRepository<TransactionDetail>
	{
		DataSourceResult GetList(DataSourceRequest request);
		DataSourceResult GetListByFilter(DataSourceRequest request, TransactionFilter filter);
		List<TransactionDetailListDto> GetListByFilter(TransactionFilter filter);
		DataSourceResult GetListByCardNumber(DataSourceRequest request, string cardNumber);
		void BulkInsert(List<TransactionDetail> transactionDetailList);
	}
}
