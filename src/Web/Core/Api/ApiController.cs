using DomainContracts.BroadcastAggregate;
using DomainContracts.Commons;
using DomainEntities.ApplicationUserAggregate;
using DomainEntities.BroadcastAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Core.Api.ViewModel;
using Web.Extensions;

namespace Web.Core.Api
{
	public class ApiController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IBroadCastRepository _broadCastRepository;
		private readonly IReferralBroadCastRepository _referralBroadCastRepository;
		private readonly IUnitOfWork _unitOfWork;


		private readonly IMessageHandlerRepository _messageHandlerRepository;


		public ApiController(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			IBroadCastRepository broadCastRepository,
			IReferralBroadCastRepository referralBroadCastRepository,
			IUnitOfWork unitOfWork,
			IMessageHandlerRepository messageHandlerRepository
			)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_broadCastRepository = broadCastRepository;
			_referralBroadCastRepository = referralBroadCastRepository;
			_unitOfWork = unitOfWork;
			_messageHandlerRepository = messageHandlerRepository;
		}
		[HttpPost]
		public async Task<IActionResult> IsExists([FromBody] UsernameViewModel inputModel)
		{
			try
			{
				var user = _userManager.Users
			   .Include(o => o.ApplicationUserRoles)
			   .FirstOrDefault(o => o.UserName.Equals(inputModel.Username));

				if (user == null)
				{
					return Json(new { Message = "نام کاربری یا رمز عبور اشتباه است.", IsAuthenticate = false });
				}

				//decripted Password
				string decriptedPassword = "";

				if (inputModel.IsEncrypted)
					decriptedPassword = inputModel.Password.DecryptString();
				else
					decriptedPassword = inputModel.Password;


				var result = await _signInManager.PasswordSignInAsync(user, decriptedPassword, false, false);
				if (result.Succeeded)
				{
					return Json(new { Message = "ورود موفقیت آمیز می باشد.", IsAuthenticate = true, UserId = user.Id });
				}
				else
					return Json(new { Message = "نام کاربری یا رمز عبور اشتباه است.", IsAuthenticate = false });
			}
			catch (System.Exception ex)
			{
				return Json(new { Message = ex.Message, IsAuthenticate = false });
			}
		}
		/// <summary>
		/// GetReferralCount
		/// </summary>
		/// <param name="filterDto"></param>
		/// <returns>count referral message</returns>
		[HttpPost]
		public async Task<GetReferralResult> GetReferralCount([FromBody] FilterDto filterDto)
		{
			var result = (await _referralBroadCastRepository.ListAllAsync())
				.Where(w => w.Status == filterDto.Status && w.IsImmediate == filterDto.IsImmediate && w.DstUserID == filterDto.UserId).Count();
			var dataResult = new GetReferralResult { CountMessage = result, TextMessage = "" };

			string info = (await _messageHandlerRepository.GetMessageAll()).Where(w => w.MessageType == MessageType.Agent).FirstOrDefault()?.Description;
			if (!string.IsNullOrWhiteSpace(info))
			{
				dataResult.TextMessage = info;
			}
			else
			{
				dataResult.TextMessage = "لطفا سریعاً به سامانه اتوماسیون محرمانه مراجعه فرمائید";
			}


			return dataResult;
		}
		[HttpPost]
		public async Task<IList<BroadCast>> GetAllBroadCast()
		{
			return await _broadCastRepository.ListAllAsync();
		}
		[HttpPost]
		public async Task<bool> UpdateStatus([FromBody] int dstUserID)
		{
			var updateStatus = (await _referralBroadCastRepository.ListAllAsync()).Where(w => w.DstUserID == dstUserID).ToList();

			foreach (var item in updateStatus)
			{
				item.Status = ReferralStatusBroadCastEnum.MoshahedeShode;
				_referralBroadCastRepository.Update(item);
				await _unitOfWork.SaveAsync();
			}
			return true;
		}

		//[HttpPost]
		//public bool UpdateMoshahedeShode(List<ReferralBroadCast> InputViewModel)
		//{
		//    foreach (var item in InputViewModel)
		//    {
		//        item.Status = ReferralStatusBroadCastEnum.MoshahedeShode;
		//        _referralBroadCastRepository.Update(item);
		//    }
		//    return true;
		//}
	}
}