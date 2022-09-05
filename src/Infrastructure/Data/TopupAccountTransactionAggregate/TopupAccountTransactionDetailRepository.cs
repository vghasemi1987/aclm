using DomainContracts.TopupAccountTransactionAggregate;
using DomainEntities.Commons;
using DomainEntities.TopupAccountTransaction;
using EFCore.BulkExtensions;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data.TopupAccountTransactionAggregate
{
	public class TopupAccountTransactionDetailRepository : EfRepository<TopupAccountTransactionDetail>, ITopupAccountTransactionDetailRepository
	{
		private readonly DbSet<TopupAccountTransactionDetail> _dbSet;
		ServerAccessibilityMonitorContext _dbContext;
		public TopupAccountTransactionDetailRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<TopupAccountTransactionDetail>();
			_dbContext = dbContext;
		}

		public DataSourceResult GetList(DataSourceRequest request)
		{
			return _dbSet
				.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				.Select(q => new TopupAccountTransactionDetailListDto
				{
					Id = q.Id,
					AccountNumber = q.AccountNumber,
					BranchCode = q.BranchCode,
					CustomerAccountNumber = q.CustomerAccountNumber,
					Extra1 = q.Extra1,
					Extra2 = q.Extra2,
					Extra3 = q.Extra3,
					Extra4 = q.Extra4,
					FollowupCode = q.FollowupCode,
					FollowupCode2 = q.FollowupCode2,
					HeaderId = q.HeaderId,
					RefrenceCode = q.RefrenceCode,
					TransactionAmount = q.TransactionAmount,
					TransactionAmountText = q.TransactionAmountText,
					TransactionDate = q.TransactionDate,
					TransactionStatus = q.TransactionStatus,
					TransactionTime = q.TransactionTime,
					TransactionType = q.TransactionType,
					//TransactionDate = q.TransactionDate.ToString().ToPersianDateTime().Substring(0, 10),
				})
				.AsNoTracking()
				.ToDataSourceResult(request);
		}

		public DataSourceResult GetListByCardNumber(DataSourceRequest request, string cardNumber)
		{
			var cardDetails = cardNumber.Split('_');

			return _dbSet
			 .Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted)/* &&
                       (string.IsNullOrEmpty(cardDetails.First()) || q.SourcePan.StartsWith(cardDetails.First())) &&
                       (string.IsNullOrEmpty(cardDetails.Last()) || q.SourcePan.EndsWith(cardDetails.Last()))*/)
			  .Select(q => new TopupAccountTransactionDetailListDto
			  {
				  Id = q.Id,
				  AccountNumber = q.AccountNumber,
				  BranchCode = q.BranchCode,
				  CustomerAccountNumber = q.CustomerAccountNumber,
				  Extra1 = q.Extra1,
				  Extra2 = q.Extra2,
				  Extra3 = q.Extra3,
				  Extra4 = q.Extra4,
				  FollowupCode = q.FollowupCode,
				  FollowupCode2 = q.FollowupCode2,
				  HeaderId = q.HeaderId,
				  RefrenceCode = q.RefrenceCode,
				  TransactionAmount = q.TransactionAmount,
				  TransactionAmountText = q.TransactionAmountText,
				  TransactionDate = q.TransactionDate,
				  TransactionStatus = q.TransactionStatus,
				  TransactionTime = q.TransactionTime,
				  TransactionType = q.TransactionType,
				  //TransactionDate = q.TransactionDate.ToString().ToPersianDateTime().Substring(0, 10),
			  })
			.AsNoTracking()
			.ToDataSourceResult(request);
		}

		public DataSourceResult GetListByFilter(DataSourceRequest request, TopupAccountTransactionFilter filter)
		{
			return GetTransactionDetailListsAsQueryble(filter)
				.AsNoTracking()
			   .ToDataSourceResult(request);
		}

		public List<TopupAccountTransactionDetailListDto> GetListByFilter(TopupAccountTransactionFilter filter)
		{
			return GetTransactionDetailListsAsQueryble(filter)
					  .ToList();
		}

		public void BulkInsert(List<TopupAccountTransactionDetail> transactionDetailList)
		{
			_dbContext.BulkInsert(transactionDetailList);
		}

		private IQueryable<TopupAccountTransactionDetailListDto> GetTransactionDetailListsAsQueryble(TopupAccountTransactionFilter filter)
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
						(string.IsNullOrEmpty(filter.TransactionTime) || filter.TransactionTime == "" || q.TransactionTime == filter.TransactionTime)
						)
				 .Select(q => new TopupAccountTransactionDetailListDto
				 {
					 Id = q.Id,
					 AccountNumber = q.AccountNumber,
					 BranchCode = q.BranchCode,
					 CustomerAccountNumber = q.CustomerAccountNumber,
					 Extra1 = q.Extra1,
					 Extra2 = q.Extra2,
					 Extra3 = q.Extra3,
					 Extra4 = q.Extra4,
					 FollowupCode = q.FollowupCode,
					 FollowupCode2 = q.FollowupCode2,
					 HeaderId = q.HeaderId,
					 RefrenceCode = q.RefrenceCode,
					 TransactionAmount = q.TransactionAmount,
					 TransactionAmountText = q.TransactionAmountText,
					 TransactionDate = q.TransactionDate,
					 TransactionStatus = q.TransactionStatus,
					 TransactionTime = q.TransactionTime,
					 TransactionType = q.TransactionType,
					 //TransactionDate = q.TransactionDate.ToString().ToPersianDateTime().Substring(0, 10),
				 });
		}
	}
}
