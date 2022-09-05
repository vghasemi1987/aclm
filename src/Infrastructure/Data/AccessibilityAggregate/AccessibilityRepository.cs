using DomainContracts.AccessibilityAggregate;
using DomainContracts.Dapper;
using DomainEntities.AccessibilityAggregate;
using DomainEntities.Commons;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.AccessibilityAggregate
{
	public class AccessibilityRepository : EfRepository<Accessibility>, IAccessibilityRepository
	{
		private readonly IDapperRepository _dapperRepository;
		private readonly ServerAccessibilityMonitorContext _dbContext;
		private readonly DbSet<Accessibility> _dbSet;

		public AccessibilityRepository(ServerAccessibilityMonitorContext dbContext, IDapperRepository dapperRepository) : base(dbContext)
		{
			_dbSet = dbContext.Set<Accessibility>();
			_dbContext = dbContext;
			_dapperRepository = dapperRepository;
		}

		public DataSourceResult GetList(DataSourceRequest request)
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				 .Select(o => new AccessibilityListDto
				 {
					 Id = o.Id,
					 DestinationPort = o.DestinationPort ?? o.DestinationService.Port.ToString(),
					 IsTemp = o.IsTemp,
					 SourcePort = o.SourcePort ?? o.Service.Port.ToString(),
					 AclFilesUploadId = o.AclFilesUploadId,
					 AclFilesUploadTitle = o.AclFilesUpload.FileName,
					 ActionTypeId = o.ActionTypesId,
					 ActionType = o.ActionType ?? o.ActionTypes.Title,
					 DestinationSystemId = o.DestinationSystemId,
					 DestinationSystemTitle = o.DestinationSystem.SystemName,
					 DestinationIp = o.DestinationIp ?? o.DestinationSystem.IpAddress,
					 ProtocolId = o.ProtocolsId,
					 Protocol = o.Protocol ?? o.Protocols.Name,
					 RouterId = o.RouterId,
					 RouterTitle = o.Router.Name,
					 ServiceId = o.ServiceId,
					 ServiceTitle = o.Service.Name,
					 SourceSystemId = o.SourceSystemId,
					 SourceSystemTitle = o.SourceSystem.SystemName,
					 SourceIp = o.SourceIp ?? o.SourceSystem.IpAddress,
				 })
				 .AsNoTracking()
				 .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
		}

		public void DeleteTemp(int userId)
		{
			_dapperRepository.ExecuteNonQueryProcedure("dbo.uspAccessibilityDeleteTemp",
			   new
			   {
				   UserId = userId
			   });
		}

		public void ConfirmAccessibilities(int userId)
		{
			_dapperRepository.ExecuteNonQueryProcedure("dbo.uspConfirmAccessibilities",
				new
				{
					UserId = userId
				});
		}

		public Task<Accessibility> FindByIdAsync(int id)
		{
			return _dbSet.Where(p => p.Id == id).FirstOrDefaultAsync();
		}

		public IQueryable<Accessibility> GetListAsQueryable()
		{
			return _dbSet.
				Where(q => q.RecordStatus != Convert.ToBoolean(RecordStatus.Deleted)).
				AsQueryable();
		}

		public DataSourceResult GetAccessibilityReportByCount(DataSourceRequest request, int count)
		{
			return _dbContext.AccessibilityReportByCount
				  .FromSqlRaw("execute dbo.uspAccessibilityReportByCount @Count", new SqlParameter("@Count", count))
				  .AsNoTracking()
				   .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
		}
		public List<AccessibilityReportByCount> GetAccessibilityReportByCount(int count)
		{
			return _dbContext.AccessibilityReportByCount
				  .FromSqlRaw("execute dbo.uspAccessibilityReportByCount @Count", new SqlParameter("@Count", count)).ToList();
		}
		public DataSourceResult GetReportByFilter(DataSourceRequest request, AccessibilityFilter filter)
		{
			#region SqlParameters
			var sqlParameters = new List<SqlParameter>
			{
				new SqlParameter("@SourceSystemId", SqlDbType.Int)
				{
					Value = (object)filter.SourceSystemId ?? DBNull.Value
				},
				 new SqlParameter("@DestinationSystemId", SqlDbType.Int)
				{
					Value =(object) filter.DestinationSystemId??DBNull.Value
				},
				new SqlParameter("@SourceIP", SqlDbType.NVarChar)
				{
					Value = (object)filter.SourceIp??DBNull.Value
				},
				new SqlParameter("@DestinationIP", SqlDbType.NVarChar)
				{
					Value = (object)filter.DestinationIp??DBNull.Value
				},
				new SqlParameter("@ServiceId", SqlDbType.Int)
				{
					Value =(object) filter.ServiceId??DBNull.Value
				},
				new SqlParameter("@DestinationServiceId", SqlDbType.Int)
				{
				   Value = (object)filter.DestinationServiceId??DBNull.Value
				},
				new SqlParameter("@RequestingUserId", SqlDbType.Int)
				{
					Value = (object)filter.RequestingUserId??DBNull.Value
				},
				new SqlParameter("@ConfirmUserId", SqlDbType.Int)
				{
					Value = (object)filter.ConfirmUserId??DBNull.Value
				},
				new SqlParameter("@UserDepartmentId", SqlDbType.Int)
				{
					Value = (object)filter.UserDepartmentId??DBNull.Value
				},
				new SqlParameter("@RequestDepartmentId", SqlDbType.Int)
				{
				   Value =(object) filter.RequestDepartmentId??DBNull.Value
				},
				new SqlParameter("@AccessibilityTypeId", SqlDbType.Int)
				{
					Value = (object)filter.AccessibilityTypeId??DBNull.Value
				},
			};
			#endregion

			return _dbContext.AccessibilityReportByFilter
					.FromSqlRaw("dbo.uspAccessibilityReportByFilter @SourceSystemId," +
							 "@DestinationSystemId,@SourceIP,@DestinationIP,@ServiceId,@DestinationServiceId,@RequestingUserId," +
							 "@ConfirmUserId,@UserDepartmentId,@RequestDepartmentId,@AccessibilityTypeId", sqlParameters.ToArray())
					.AsNoTracking()
					.ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
		}

		public void DeleteByRouterId(int routerId)
		{
			_dapperRepository.ExecuteNonQueryProcedure("dbo.uspUpdateAccessibilitiesByRouterId",
			   new
			   {
				   VersionDate = DateTime.Now.Date,
				   RouterId = routerId
			   });
		}
	}
}
