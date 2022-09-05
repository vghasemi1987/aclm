using ApplicationCommon;
using DomainContracts.ServiceAggregate;
using DomainEntities.Commons;
using DomainEntities.SearchLogAggregate;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.SearchLogAggregate
{
	public class SearchLogDetailRepository : EfRepository<SearchLogDetail>, ISearchLogDetailRepository
	{
		private readonly DbSet<SearchLogDetail> _dbSet;
		private readonly ServerAccessibilityMonitorContext _dbContext;

		public SearchLogDetailRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
			_dbSet = dbContext.Set<SearchLogDetail>();
		}

		public DataSourceResult GetList(DataSourceRequest request)
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				 .Select(o => new SearchLogDetailListDto
				 {
					 FirstName = o.FirstName,
					 LastName = o.LastName,
					 NationalCode = o.NationalCode,
					 FatherName = o.FatherName,
					 Address = o.Address,
					 LetterIdentifier = o.LetterIdentifier,
					 CardNumber = o.CardNumber,
					 SearchDate = o.SearchDate.ToPersianDateTime("yyyy/MM/dd"),
					 SearchTime = o.SearchTime,
					 IsSuccess = o.IsSuccess ? "موفقیت آمیز" : "ناموفق",
					 IsVictim = o.IsVictim ? "قربانی" : "سواستفاده کننده"
				 })
				 .AsNoTracking()
				 .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
		}

		public Task<SearchLogDetail> FindByIdAsync(int id)
		{
			return _dbSet.Where(q => q.Id == id).FirstOrDefaultAsync();
		}

		public DataSourceResult GetAbundanceReport(DataSourceRequest request)
		{
			return _dbContext.AbundanceReports
				  .FromSqlRaw("execute dbo.uspAbundanceReport")
				  .AsNoTracking()
				  .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
		}

		public DataSourceResult GetSajayaReport(DataSourceRequest request, SearchLogFilter filter)
		{
			return _dbSet.Where(q => (q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted)) &&
							   (string.IsNullOrEmpty(filter.LetterIdentifier) || q.LetterIdentifier.CompareTo(filter.LetterIdentifier) == 0) &&
							   (string.IsNullOrEmpty(filter.FirstName) || q.FirstName.CompareTo(filter.FirstName) == 0) &&
							   (string.IsNullOrEmpty(filter.LastName) || q.FirstName.CompareTo(filter.LastName) == 0) &&
							   (string.IsNullOrEmpty(filter.SearchDate) || q.SearchDate.Date == filter.SearchDate.ToMiladiDate().Date) &&
							   (q.IsSuccess == filter.IsSuccess) &&
							   (q.IsVictim == filter.IsVictim))
				.Select(o => new SearchLogDetailListDto
				{
					Id = o.Id,
					FirstName = o.FirstName,
					LastName = o.LastName,
					NationalCode = o.NationalCode,
					FatherName = o.FatherName,
					Address = o.Address,
					LetterIdentifier = o.LetterIdentifier,
					CardNumber = o.CardNumber,
					SearchDate = o.SearchDate.Date.ToPersianDateTime("yyyy/MM/dd"),
					SearchTime = o.SearchTime,
					IsSuccess = o.IsSuccess ? "موفقیت آمیز" : "ناموفق",
					IsVictim = o.IsVictim ? "قربانی" : "سواستفاده کننده",

				})
				.AsNoTracking()
				.ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
		}

		public List<SearchLogDetailListDto> GetByLetterIdentifier(string letterIdentifier)
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted) &&
							   (q.LetterIdentifier.CompareTo(letterIdentifier) == 0))
				.Select(o => new SearchLogDetailListDto
				{
					FirstName = o.FirstName,
					LastName = o.LastName,
					NationalCode = o.NationalCode,
					FatherName = o.FatherName,
					Address = o.Address,
					LetterIdentifier = o.LetterIdentifier,
					CardNumber = o.CardNumber,
					SearchDate = o.SearchDate.ToPersianDateTime("yyyy/MM/dd"),
					SearchTime = o.SearchTime,
					IsSuccess = o.IsSuccess ? "موفقیت آمیز" : "ناموفق",
					IsVictim = o.IsVictim ? "قربانی" : "سواستفاده کننده"
				})
				 .AsNoTracking()
				 .ToList();
		}
	}
}
