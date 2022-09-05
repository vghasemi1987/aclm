using ApplicationCommon;
using DomainContracts.AccessibilityRequestAggregate;
using DomainEntities.AccessibilityRequestAggregate;
using DomainEntities.Commons;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.AccessibilityRequestAggregate
{
	public class AccessibilityRequestRepository : EfRepository<AccessibilityRequest>, IAccessibilityRequestRepository
	{
		private readonly DbSet<AccessibilityRequest> _dbSet;
		public AccessibilityRequestRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<AccessibilityRequest>();
		}

		public DataSourceResult GetList(DataSourceRequest request)
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted)).
					Select(o => new AccessibilityRequestListDto
					{
						Id = o.Id,
						AccessEndDate = o.AccessEndDate,
						AccessStartDate = o.AccessStartDate,
						Description = o.Description,
						SourceIp = o.SourceIp,
						DestinationIp = o.DestinationIp,
						LetterDate = o.LetterDate.GetValueOrDefault().ToString().ToPersianDateTime(),
						LetterNo = o.LetterNo,
						PhoneNumber = o.PhoneNumber,
						SourcePort = o.Service.Port,
						DestinationPort = o.DestinationService.Port,
						AccessibilityTypeId = o.AccessibilityTypeId,
						ConfirmUserId = o.ConfirmUserId,
						ConfirmUser = o.ConfirmUser.Name,
						DestAccessibilityLevelId = o.DestAccessibilityLevelId,
						DestinationProtocolId = o.DestinationProtocolId,
						DestinationServiceId = o.DestinationServiceId,
						DestinationSystemId = o.DestinationSystemId,
						DestinationSystemTitle = o.DestinationSystem.SystemName,
						RequestDepartmentId = o.RequestDepartmentId,
						RequestDepartmentTitle = o.RequestDepartment.Name,
						RequestingUserId = o.RequestingUserId,
						RequestingUser = o.RequestingUser.Name,
						ServiceId = o.ServiceId,
						SourceAccessibilityLevelId = o.SourceAccessibilityLevelId,
						SourceProtocolId = o.SourceProtocolId,
						SourceSystemId = o.SourceSystemId,
						SourceSystemTitle = o.SourceSystem.SystemName,
						UserDepartmentId = o.UserDepartmentId,
					}).AsNoTracking()
			.ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
		}
		public Task<AccessibilityRequest> FindByIdAsync(int id)
		{
			return _dbSet.Where(p => p.Id == id)
				.FirstOrDefaultAsync();
		}
	}
}
