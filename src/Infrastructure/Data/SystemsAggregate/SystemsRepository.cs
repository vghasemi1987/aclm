using DomainContracts.SystemsAggregate;
using DomainEntities.Commons;
using DomainEntities.SystemsAggregate;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.SystemsAggregate
{
	public class SystemsRepository : EfRepository<Systems>, ISystemsRepository
	{
		private readonly DbSet<Systems> _dbSet;
		public SystemsRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<Systems>();
		}

		public DataSourceResult GetList(DataSourceRequest request)
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				 .Select(o => new SystemsListDto
				 {
					 Id = o.Id,
					 SystemName = o.SystemName,
					 AccessibilityLevelId = o.AccessibilityLevelId,
					 AccessibilityLevelTitle = o.AccessibilityLevel.Title,
					 Description = o.Description,
					 InformationAccessibilityLevelId = o.InformationAccessibilityLevelId,
					 InformationAccessibilityLevelTitle = o.InformationAccessibilityLevel.Title,
					 IpAddress = o.IpAddress,
					 DepartmentId = o.DepartmentId,
					 DepartmentTitle = o.Department.Name,
					 FirstName = o.FirstName,
					 LastName = o.LastName,
					 ImportanceFactor = o.ImportanceFactor,
					 PersonelCode = o.PersonelCode,
					 KindId = o.KindId,
					 IpTo = o.IpTo,
					 IpFrom = o.IpFrom,
				 })
				 .AsNoTracking()
				 .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
		}

		public Systems FindByIdAsync(int id)
		{
			return _dbSet.Where(p => p.Id == id).FirstOrDefault();
		}

		public List<DropDownDto> GetDropDownList()
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				.Select(o => new DropDownDto
				{
					Id = o.Id,
					Title = o.SystemName,
					IpAddress = o.IpAddress,
				})
				.AsNoTracking()
				.ToList();
		}
		public Task<List<DropDownDto>> GetDropDownListAsync()
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				.Select(o => new DropDownDto
				{
					Id = o.Id,
					Title = o.SystemName
				})
				.AsNoTracking()
				.ToListAsync();
		}

		public List<Systems> GetList()
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted)).ToList();
		}
		public object GetSystemIpList()
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				.Select(o => new
				{
					o.Id,
					o.SystemName,
					o.IpAddress
				})
				.AsNoTracking()
				.ToList();
		}
	}
}
