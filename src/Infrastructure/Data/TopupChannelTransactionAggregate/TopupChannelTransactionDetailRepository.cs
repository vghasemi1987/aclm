using DomainContracts.TopupChannelTransactionAggregate;
using DomainEntities.Commons;
using DomainEntities.TopupChannelTransaction;
using EFCore.BulkExtensions;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data.TopupChannelTransactionAggregate
{
	public class TopupChannelTransactionDetailRepository : EfRepository<TopupChannelTransactionDetail>, ITopupChannelTransactionDetailRepository
	{
		private readonly DbSet<TopupChannelTransactionDetail> _dbSet;
		ServerAccessibilityMonitorContext _dbContext;
		public TopupChannelTransactionDetailRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<TopupChannelTransactionDetail>();
			_dbContext = dbContext;
		}

		public DataSourceResult GetList(DataSourceRequest request)
		{
			return _dbSet
				.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				.Select(q => new TopupChannelTransactionDetailListDto
				{
					Id = q.Id,
					HeaderId = q.HeaderId,
					//TransactionDate = q.TransactionDate.ToString().ToPersianDateTime().Substring(0, 10),
					AmountText = q.AmountText,
					ChannelType = q.ChannelType,
					CustomerAccountNumber = q.CustomerAccountNumber,
					Extra1 = q.Extra1,
					Extra2 = q.Extra2,
					FollowupCode = q.FollowupCode,
					FollowupCode2 = q.FollowupCode2,
					MobileNumber = q.MobileNumber,
					OperatorName = q.OperatorName,
					TransactionAmount = q.TransactionAmount,
					TransactionDate = q.TransactionDate,
					TransactionStatus = q.TransactionStatus,
				})
				.AsNoTracking()
				.ToDataSourceResult(request);
		}

		public DataSourceResult GetListByCardNumber(DataSourceRequest request, string cardNumber)
		{
			var cardDetails = cardNumber.Split('_');

			return _dbSet
			 .Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted) /*&&
                       (string.IsNullOrEmpty(cardDetails.First()) || q.SourcePan.StartsWith(cardDetails.First())) &&
                       (string.IsNullOrEmpty(cardDetails.Last()) || q.SourcePan.EndsWith(cardDetails.Last()))*/)
			  .Select(q => new TopupChannelTransactionDetailListDto
			  {
				  Id = q.Id,
				  HeaderId = q.HeaderId,
				  //TransactionDate = q.TransactionDate.ToString().ToPersianDateTime().Substring(0, 10),
				  AmountText = q.AmountText,
				  ChannelType = q.ChannelType,
				  CustomerAccountNumber = q.CustomerAccountNumber,
				  Extra1 = q.Extra1,
				  Extra2 = q.Extra2,
				  FollowupCode = q.FollowupCode,
				  FollowupCode2 = q.FollowupCode2,
				  MobileNumber = q.MobileNumber,
				  OperatorName = q.OperatorName,
				  TransactionAmount = q.TransactionAmount,
				  TransactionDate = q.TransactionDate,
				  TransactionStatus = q.TransactionStatus,
			  })
			.AsNoTracking()
			.ToDataSourceResult(request);
		}

		public DataSourceResult GetListByFilter(DataSourceRequest request, TopupChannelTransactionFilter filter)
		{
			return GetTransactionDetailListsAsQueryble(filter)
				.AsNoTracking()
			   .ToDataSourceResult(request);
		}

		public List<TopupChannelTransactionDetailListDto> GetListByFilter(TopupChannelTransactionFilter filter)
		{
			return GetTransactionDetailListsAsQueryble(filter)
					  .ToList();
		}

		public void BulkInsert(List<TopupChannelTransactionDetail> transactionDetailList)
		{
			_dbContext.BulkInsert(transactionDetailList);
		}

		private IQueryable<TopupChannelTransactionDetailListDto> GetTransactionDetailListsAsQueryble(TopupChannelTransactionFilter filter)
		{
			//var sourcePan = filter.SourcePan?.Replace("-", "");
			//var targetPan = filter.TargetPan?.Replace("-", "");

			return _dbSet
				.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted) &&
						(filter.HeaderId == 0 || q.HeaderId == filter.HeaderId) &&
						(string.IsNullOrEmpty(filter.TransactionDate) || q.TransactionDate.Contains(filter.TransactionDate) /*q.TransactionDateDate == filter.TransactionDate.ToMiladiDate().Date*/) &&
						(string.IsNullOrEmpty(filter.TransactionAmount) || filter.TransactionAmount == "0" || q.TransactionAmount == filter.TransactionAmount) &&
						(string.IsNullOrEmpty(filter.FollowupCode) || filter.FollowupCode == "" || q.FollowupCode == filter.FollowupCode) &&
						(string.IsNullOrEmpty(filter.CustomerAccountNumber) || filter.CustomerAccountNumber == "" || q.CustomerAccountNumber == filter.CustomerAccountNumber) &&
						(string.IsNullOrEmpty(filter.TransactionStatus) || filter.TransactionStatus == "" || q.TransactionStatus == filter.TransactionStatus) &&
						(string.IsNullOrEmpty(filter.OperatorName) || filter.OperatorName == "" || q.OperatorName == filter.OperatorName)
						)
				 .Select(q => new TopupChannelTransactionDetailListDto
				 {
					 Id = q.Id,
					 HeaderId = q.HeaderId,
					 //TransactionDate = q.TransactionDate.ToString().ToPersianDateTime().Substring(0, 10),
					 AmountText = q.AmountText,
					 ChannelType = q.ChannelType,
					 CustomerAccountNumber = q.CustomerAccountNumber,
					 Extra1 = q.Extra1,
					 Extra2 = q.Extra2,
					 FollowupCode = q.FollowupCode,
					 FollowupCode2 = q.FollowupCode2,
					 MobileNumber = q.MobileNumber,
					 OperatorName = q.OperatorName,
					 TransactionAmount = q.TransactionAmount,
					 TransactionDate = q.TransactionDate,
					 TransactionStatus = q.TransactionStatus,
				 });
		}
	}
}
