using ApplicationCommon;
using DomainContracts.TransactionAggregate;
using DomainEntities.Commons;
using DomainEntities.Transaction;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Infrastructure.Data.TransactionAggregate
{
	public class TransactionDetailRepository : EfRepository<TransactionDetail>, ITransactionDetailRepository
	{
		private readonly DbSet<TransactionDetail> _dbSet;
		ServerAccessibilityMonitorContext _dbContext;
		public TransactionDetailRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<TransactionDetail>();
			_dbContext = dbContext;
		}

		public DataSourceResult GetList(DataSourceRequest request)
		{
			return _dbSet
				.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				.Select(q => new TransactionDetailListDto
				{
					Id = q.Id,
					Amount = (long)q.Amount,
					HeaderId = (int)q.HeaderId,
					IpAddress = q.IpAddress,
					RefNumber = q.RefNumber,
					SourcePan = q.SourcePan,
					TargetPan = q.TargetPan,
					TransactionDate = q.TransactionDate.ToString().ToPersianDateTime().Substring(0, 10),
					UserAgent = q.UserAgent,
					TransactionTime = q.TransactionTime,
					Tel = q.Tel,
					Status = q.Status,
				})
				.AsNoTracking()
				.ToDataSourceResult(request);
		}

		public DataSourceResult GetListByCardNumber(DataSourceRequest request, string cardNumber)
		{
			//var cardDetails = cardNumber.Split('_');
			var cardDetails = cardNumber.Substring(0, 6) + "xxxxxx" + cardNumber.Substring(12, 4);

			return _dbSet
			 .Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted) &&
					   (cardDetails == q.SourcePan || cardDetails == q.TargetPan))
			  .Select(q => new TransactionDetailListDto
			  {
				  Id = q.Id,
				  Amount = (long)q.Amount,
				  HeaderId = (int)q.HeaderId,
				  IpAddress = q.IpAddress,
				  RefNumber = q.RefNumber,
				  SourcePan = q.SourcePan,
				  TargetPan = q.TargetPan,
				  TransactionDate = q.TransactionDate.ToString().ToPersianDateTime().Substring(0, 10),
				  UserAgent = q.UserAgent,
				  TransactionTime = q.TransactionTime,
				  Tel = q.Tel,
				  Status = q.Status,
			  })
			.AsNoTracking()
			.ToDataSourceResult(request);
		}

		public DataSourceResult GetListByFilter(DataSourceRequest request, TransactionFilter filter)
		{
			return GetTransactionDetailListsAsQueryble(filter)
				.AsNoTracking()
			   .ToDataSourceResult(request);
		}

		public List<TransactionDetailListDto> GetListByFilter(TransactionFilter filter)
		{
			return GetTransactionDetailListsAsQueryble(filter)
					  .ToList();
		}

		public void BulkInsert(List<TransactionDetail> transactionDetailList)
		{
			try
			{
				string connectionString = _dbContext.Database.GetDbConnection().ConnectionString;

				DataTable tbl = new DataTable();
				tbl.Columns.Add(new DataColumn("RecordStatus", typeof(bool)));
				tbl.Columns.Add(new DataColumn("SourcePan", typeof(string)));
				tbl.Columns.Add(new DataColumn("TargetPan", typeof(string)));
				tbl.Columns.Add(new DataColumn("SourcePanOrgianl", typeof(string)));
				tbl.Columns.Add(new DataColumn("TargetPanOrgianl", typeof(string)));
				tbl.Columns.Add(new DataColumn("TransactionDate", typeof(DateTime)));
				tbl.Columns.Add(new DataColumn("TransactionTime", typeof(string)));
				tbl.Columns.Add(new DataColumn("Tel", typeof(string)));
				tbl.Columns.Add(new DataColumn("Amount", typeof(long)));
				tbl.Columns.Add(new DataColumn("IpAddress", typeof(string)));
				tbl.Columns.Add(new DataColumn("LogDate", typeof(DateTime)));
				tbl.Columns.Add(new DataColumn("Status", typeof(string)));
				tbl.Columns.Add(new DataColumn("RefNumber", typeof(string)));
				tbl.Columns.Add(new DataColumn("UserAgent", typeof(string)));
				tbl.Columns.Add(new DataColumn("Application", typeof(string)));
				tbl.Columns.Add(new DataColumn("HeaderId", typeof(int)));

				//var props = TypeDescriptor.GetProperties(typeof(TransactionDetail));


				for (int i = 0; i < transactionDetailList.Count; i++)
				{
					DataRow dr = tbl.NewRow();

					dr["RecordStatus"] = transactionDetailList[i]?.RecordStatus;

					dr["SourcePan"] = transactionDetailList[i]?.SourcePan;
					dr["TargetPan"] = transactionDetailList[i]?.TargetPan;
					dr["SourcePanOrgianl"] = transactionDetailList[i]?.SourcePanOrgianl;
					dr["TargetPanOrgianl"] = transactionDetailList[i]?.TargetPanOrgianl;
					dr["TransactionDate"] = transactionDetailList[i]?.TransactionDate;
					dr["TransactionTime"] = transactionDetailList[i]?.TransactionTime;
					dr["Tel"] = transactionDetailList[i]?.Tel;
					dr["Amount"] = transactionDetailList[i]?.Amount;
					dr["IpAddress"] = transactionDetailList[i]?.IpAddress;
					dr["LogDate"] = transactionDetailList[i]?.LogDate;
					dr["Status"] = transactionDetailList[i]?.Status;
					dr["RefNumber"] = transactionDetailList[i]?.RefNumber;
					dr["UserAgent"] = transactionDetailList[i]?.UserAgent;
					dr["Application"] = transactionDetailList[i]?.Application;
					dr["HeaderId"] = transactionDetailList[i]?.HeaderId;
					tbl.Rows.Add(dr);
				}



				SqlConnection connection = new SqlConnection(connectionString);
				SqlBulkCopy bulkCopy = new SqlBulkCopy(connection);
				bulkCopy.BulkCopyTimeout = 5000;
				bulkCopy.BatchSize = 10000;
				bulkCopy.DestinationTableName = $"dbo.[TransactionDetails]";


				bulkCopy.ColumnMappings.Add("RecordStatus", "RecordStatus");
				bulkCopy.ColumnMappings.Add("SourcePan", "SourcePan");
				bulkCopy.ColumnMappings.Add("TargetPan", "TargetPan");

				bulkCopy.ColumnMappings.Add("SourcePanOrgianl", "SourcePanOrgianl");
				bulkCopy.ColumnMappings.Add("TargetPanOrgianl", "TargetPanOrgianl");
				bulkCopy.ColumnMappings.Add("TransactionDate", "TransactionDate");
				bulkCopy.ColumnMappings.Add("TransactionTime", "TransactionTime");
				bulkCopy.ColumnMappings.Add("Tel", "Tel");
				bulkCopy.ColumnMappings.Add("Amount", "Amount");
				bulkCopy.ColumnMappings.Add("IpAddress", "IpAddress");
				bulkCopy.ColumnMappings.Add("LogDate", "LogDate");
				bulkCopy.ColumnMappings.Add("Status", "Status");
				bulkCopy.ColumnMappings.Add("RefNumber", "RefNumber");
				bulkCopy.ColumnMappings.Add("UserAgent", "UserAgent");
				bulkCopy.ColumnMappings.Add("Application", "Application");
				bulkCopy.ColumnMappings.Add("HeaderId", "HeaderId");

				connection.Open();

				bulkCopy.WriteToServer(tbl);
				connection.Close();
			}
			catch (Exception ex)
			{

				throw;
			}







		}

		private IQueryable<TransactionDetailListDto> GetTransactionDetailListsAsQueryble(TransactionFilter filter)
		{
			var sourcePan = filter.SourcePan?.Replace("-", "").Replace("*", "x");
			var targetPan = filter.TargetPan?.Replace("-", "").Replace("*", "x");
			var beginTransactionDate = filter.BeginTransactionDate?.ToMiladiDate();
			var endTransactionDate = filter.EndTransactionDate?.ToMiladiDate();
			var query = _dbSet
		   .Where(q => //q.TargetPan == targetPan && 
		   q.SourcePan == sourcePan &&
		   q.TransactionDate >= beginTransactionDate &&
		   q.TransactionDate <= endTransactionDate
		   );
			if (!(targetPan == ""))
			{
				query = query.Where(q => q.TargetPan == targetPan);
			}
			if (!(!filter.HeaderId.HasValue || filter.HeaderId == 0))
			{
				query = query.Where(q => q.HeaderId == filter.HeaderId);
			}
			if (!(filter.Amount == 0))
			{
				query = query.Where(q => q.Amount == filter.Amount);
			}
			var r = query.Select(q => new TransactionDetailListDto
			{
				Id = q.Id,
				Amount = (long)q.Amount,
				HeaderId = (int)q.HeaderId,
				IpAddress = q.IpAddress,
				RefNumber = q.RefNumber,
				SourcePan = q.SourcePan,
				TargetPan = q.TargetPan,
				TransactionDate = q.TransactionDate.ToString().ToPersianDateTime().Substring(0, 10),
				UserAgent = q.UserAgent,
				TransactionTime = q.TransactionTime,
				Tel = q.Tel,
				Status = q.Status,
			});
			return r;
		}
	}
}
