using ApplicationCommon;
using DomainContracts.BroadcastAggregate;
using DomainEntities.ApplicationUserAggregate;
using DomainEntities.BroadcastAggregate;
using DomainEntities.Commons;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.BroadCastAggregate
{
	public class ReferralBroadCastRepository : EfRepository<ReferralBroadCast>, IReferralBroadCastRepository
	{
		private readonly DbSet<ReferralBroadCast> _dbSet;
		private readonly UserManager<ApplicationUser> _userManager;
		public ReferralBroadCastRepository(ServerAccessibilityMonitorContext dbContext, UserManager<ApplicationUser> userManager) : base(dbContext)
		{
			_dbSet = dbContext.Set<ReferralBroadCast>();
			_userManager = userManager;
		}

		public async Task<IList<ReferralBroadCast>> GetReferralJoinBoard()
		{
			return await DbContext.Set<ReferralBroadCast>().Include(c => c.BroadCast).ToListAsync();
		}
		public async Task<DataSourceResult> GetList(DataSourceRequest request, int dstUserID)
		{
			var result =
				_dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted) && q.DstUserID == dstUserID)
				.Select(o => new
				{
					Id = o.Id,
					DeadLine = o.DeadLine.ToPersianDateTime("yyyy/MM/dd - HH:mm"),
					SrcUser = _userManager.Users.Where(s => s.Id == o.SrcUser).FirstOrDefault().Name,
					BroadCastId = o.BroadCastId,
					Description = o.Description,
					Status = o.Status.DescriptionAttr(),
					IsImmediateText = o.IsImmediate == true ? "فوری" : "عادی",
				})
				.AsNoTracking()
			.ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);

			return result;
		}
		public async Task<ReferralBroadCast> GetById(int? id)
		{
			var model =
				await _dbSet.Where(w => w.Id == id).AsNoTracking().Include(c => c.ApplicationUser).Include(c => c.BroadCast).FirstOrDefaultAsync();
			return model;
		}
		public async Task<ReferralBroadCast> GetReferralBroadCastFromBroadCast(int id)
		{
			var result =
				await _dbSet.Where(w => w.BroadCastId == id)
				.Include(c => c.ApplicationUser)
				.Include(c => c.BroadCast)
				.FirstOrDefaultAsync();


			if (result == null) throw new Exception();
			//var model =
			//	_dbSet.Include(c => c.ApplicationUser).Include(c => c.BroadCast);

			//var result =
			//	await model.Where(c => c.BroadCastId == id).FirstOrDefaultAsync();

			return result;
		}
		public async Task<ReferralBroadCast> GetByIdUpdate(int? id)
		{
			var model = await _dbSet.FindAsync(id);
			return model;
		}
		public async Task<List<ReferralBroadCast>> GetListtById(int? id)
		{
			var model = await _dbSet.Where(w => w.BroadCastId == id).Include(c => c.ApplicationUser).Include(c => c.BroadCast).AsNoTracking().ToListAsync();
			return model;
		}
		public async Task<List<ReferralBroadCast>> GetListtByIdDstUser(int? id)
		{
			List<ReferralBroadCast> model =
				await _dbSet.Where(w => w.DstUserID == id)
				.Include(c => c.ApplicationUser).Include(c => c.BroadCast).AsNoTracking().ToListAsync();
			return model;
		}
		public async Task<List<BroadCast>> GetListtByIdDstUserBroadCast(int? id)
		{
			List<BroadCast> broadCastsList = new List<BroadCast>();
			var model =
				await _dbSet.Where(w => w.DstUserID == id)
				.Include(c => c.BroadCast)
				.AsNoTracking().ToListAsync();

			foreach (var bb in model)
			{
				broadCastsList.Add(bb.BroadCast);
			}
			return broadCastsList;
		}
		public async Task<IEnumerable<ReferralBroadCast>> GetListAllAdameMoshahede()
		{
			var result =
			_dbSet.Where(w => w.Status == ReferralStatusBroadCastEnum.AdameMoshahede)
			.Include(c => c.ApplicationUser)
			.Include(c => c.BroadCast)
			.AsNoTracking();

			return result;
		}
		public async Task<IEnumerable<ReferralBroadCast>> GetListAllTakhir()
		{
			var result =
				await
			_dbSet.Where(w =>
			(w.ActionDescription == null || w.ActionDescription == "")
			&&
			w.BroadCast.CreateDate.Value.AddDays(5) < DateTime.Now
			).AsNoTracking()
			.Include(c => c.BroadCast)
			.Include(c => c.ApplicationUser)
			.ToListAsync();
			return result;
		}
		public async Task<int> CountUnRead5DayLast()
		{
			var result =
			_dbSet.Include(c => c.BroadCast).Where(w =>

			//w.Status == ReferralStatusBroadCastEnum.AdameMoshahede

			(w.ActionDescription == null || w.ActionDescription == "") &&
			w.BroadCast.CreateDate.Value.AddDays(5) < DateTime.Now

			)

			.Include(c => c.ApplicationUser)

			.AsNoTracking();

			return result.Count();
		}
	}
}