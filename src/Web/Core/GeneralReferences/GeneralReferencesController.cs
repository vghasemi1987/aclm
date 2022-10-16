using AutoMapper;
using DomainContracts.BankBranchAggregate;
using DomainContracts.BroadcastAggregate;
using DomainContracts.Commons;
using DomainContracts.DepartmentAggregate;
using DomainEntities.ApplicationUserAggregate;
using DomainEntities.BroadcastAggregate;
using DomainEntities.DepartmentAggregate;
using KendoHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Web.ActionFilters;
using Web.Core.Separ.ViewModels;
using Web.Extensions;
using Web.Extensions.Attributes;

namespace Web.Core.GeneralReferences
{
	[DisplayName( "ارسال اخبار" )]
	[IgnoreAntiforgeryToken]
	public class GeneralReferencesController : Controller
	{
		private readonly IConfiguration _configuration;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IBroadCastRepository _broadCastRepository;
		private readonly IReferralBroadCastRepository _referralBroadCastRepository;
		private readonly IUnitOfWork _unitOfWork;

		private readonly IDepartmentRepository _departmentRepository;
		private readonly IBankBranchRepository _bankBranchRepository;


		//لیست گروه ها
		private readonly IGroupingOfficeRepository _groupingOfficeRepository;
		private readonly IGroupingOfficeMemberRepository _groupingOfficeMemberRepository;

		private readonly IMapper _mapper;
		private readonly IUserLogMessageRepository _userLogMessageRepository;
		//لیست موضوعات
		private readonly IProtectionOfficeRepository _protectionOfficeRepositorySubject;
		private readonly IProtectionOfficeMemberRepository _protectionOfficeMemberRepositorySubject;
		//private readonly IProtectionOfficeUserRelationRepository _protectionOfficeUserRelationRepository;

		public GeneralReferencesController( UserManager<ApplicationUser> userManager ,
			IBroadCastRepository broadCastRepository ,
			IUnitOfWork unitOfWork ,
			IReferralBroadCastRepository referralBroadCastRepository ,
			IGroupingOfficeRepository groupingOfficeRepository ,
			IGroupingOfficeMemberRepository groupingOfficeMemberRepository ,
			IDepartmentRepository departmentRepository ,
			IMapper mapper ,
			IUserLogMessageRepository userLogMessageRepository ,
			IConfiguration configuration ,
			IProtectionOfficeRepository protectionOfficeRepository ,
			IProtectionOfficeMemberRepository protectionOfficeMemberRepository ,
			IBankBranchRepository bankBranchRepository
			)
		{
			_userManager = userManager;
			_broadCastRepository = broadCastRepository;
			_unitOfWork = unitOfWork;
			_departmentRepository = departmentRepository;
			_referralBroadCastRepository = referralBroadCastRepository;

			_groupingOfficeRepository = groupingOfficeRepository;
			_groupingOfficeMemberRepository = groupingOfficeMemberRepository;
			_mapper = mapper;
			_userLogMessageRepository = userLogMessageRepository;
			_configuration = configuration;

			_protectionOfficeRepositorySubject = protectionOfficeRepository;
			_protectionOfficeMemberRepositorySubject = protectionOfficeMemberRepository;
			_bankBranchRepository = bankBranchRepository;

		}

		[Permission]
		[DisplayName( displayName: "خبر عمومی" )]
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		[HttpGet]
		public async Task<IActionResult> SendMessage()
		{
			BroadCastViewModelGeneral model =
				await FillSendMessageData();

			return View( model );
		}

		public async Task<BroadCastViewModelGeneral> FillSendMessageData()
		{
			//لیست مضوعات
			//تعیین موضوع توسط کاربر شعبه
			//

			List<ProtectionOffice> subjects =
				await _protectionOfficeRepositorySubject.GetProtectionOfficeAll();

			ViewBag.Subjects = subjects;

			System.Net.IPAddress clientIp = HttpContext.Connection.RemoteIpAddress;


			string ipTown = ApplicationCommon.TownIP.ValidateIPv4( clientIp.ToString() );


			BroadCastViewModelGeneral model = new BroadCastViewModelGeneral();


			ViewBag.Town = ipTown;


			foreach ( GroupingOffice pro in _groupingOfficeRepository.ListAll() )
			{
				model.ProtectionOfficeViewModels.Add( item: new ViewModels.ProtectionOfficeViewModel { Id = pro.Id , Title = pro.Title } );
			}
			return model;
		}

		[HttpPost]
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		public async Task<IActionResult> SendMessage( BroadCastViewModelGeneral inputModel )
		{
			BroadCastViewModelGeneral model = await FillSendMessageData();
			//(0) Find Role Separ
			string roleSeparName =
				_configuration.GetSection( key: "Separ" ).GetSection( key: "Role" ).Value;

			var subject = inputModel.Subject;
			var ipTown = inputModel.Town;
			//Global User Destination
			List<ApplicationUser> finalUserDestinationDefault = new();

			//اگر آی پی مشخص نداشته باشد
			//بنابراین کاربر داخلی می باشد
			if ( ipTown.Contains( value: "داخلی" ) )
			{
				//باید بره 4 اداره ای که موضوع را انتخاب می کنیم
				ProtectionOffice protectionDefault =
					_protectionOfficeRepositorySubject.ListAll()
					.Where( w => w.Title.Contains( value: subject ) ).FirstOrDefault();

				int protectionOfficeDefaultId = protectionDefault.Id;

				List<ProtectionOfficeMember> protectionMemberDefault =
					await _protectionOfficeMemberRepositorySubject
					.GetByProtectionOfficeId( protectionOfficeDefaultId );

				foreach ( ProtectionOfficeMember protec in protectionMemberDefault )
				{
					ApplicationUser userIdDefault =
						await _userManager.FindByIdAsync( protec.ApplicationUserId.ToString() );
					finalUserDestinationDefault.Add( userIdDefault );
				}
			}
			else
			{
				//ابتدا تعیین می کنیم موضوع برای کدام دپاراتمان می باشد
				//(1) - Subject <--> Department
				//  Id	RecordStatus	Name
				//  5	0	اداره حفاظت فناوری اطلاعات و اسناد
				IList<Department> departmentList =
					await _departmentRepository.GetDepartmentByName( subject );

				if ( departmentList.Count <= 0 )
				{
					ViewBag.Message = "حوزه انتخابی فاقد اعتبار می باشد";
					return View( model );
				}
				//5 -->اداره حفاظت فناوری اطلاعات و اسناد
				int departmentInt = departmentList.First().Id;

				//(2) - BranchBank

				//  Id RecordStatus    BranchHeadId Title
				//  522 0   NULL اداره امور شعب همدان
				//  751	0	NULL	داخلی

				//مشخص می کنیم این آی پی برای کدام اداره یا استان می باشد
				DomainEntities.BankBranchAggregate.BankBranch bankBranch =
					await _bankBranchRepository.GetBranchByName( ipTown );
				if ( bankBranch == null )
				{
					ViewBag.Message = "!!! حوزه انتخابی فاقد اعتبار می باشد";
					return View( model );
				}
				//کد اداره یا استان یا شعبه
				int bankBranchId = bankBranch.Id;

				//bankBranchId = 512;

				//(3)
				//Result User (Destination)
				//کاربری که اجازه دارد پیام های مربوط به خود را مشاهده کند
				//UserName BankBranchId    DepartmentId FirstName   LastName
				//8611    520 5   عادل کلهر
				//Towns --> مقصد استان 

				//مشخص کردن کاربرانی که از این اداره یا استان
				//با این حوزه فعالیت هستند

				List<ApplicationUser> destinationUser =
					_userManager.Users
					.Where( w =>
					w.BankBranchId == bankBranchId
					||
					w.DepartmentId == departmentInt )
					.ToList();
				//تا اینجا کاربران مقصد استانی و حوزه انتخاب گردید

				//destinationUser count 0

				if ( destinationUser.Count == 0 )
				{
					ViewBag.Message = "!!! کاربری برای این موضوع مشخص نگردیده است";
					return View( model );
				}
				//حالا باید کاربران را در لیست آقای سبزعلیان فیلتر کنیم 
				//(3.5)


				//SELECT * FROM [dbo].GroupingOfficeMember ss
				//WHERE ss.GroupingOfficeId = 68
				//
				//باید افرادی که در لیست تعیین شده وجود داشته باشن

				List<ApplicationUser> listDestinatioUserByTown =
				await _groupingOfficeRepository.GetUserBySubject( destinationUser: destinationUser , title: ipTown );

				//IList<ApplicationUser> separRoleUsers =
				//	await _userManager.GetUsersInRoleAsync(roleName: roleSeparName);

				//(6)
				//List<ApplicationUser> userFinalSender = new List<ApplicationUser>();

				//foreach ( ApplicationUser userCheck in destinationUser )
				foreach ( ApplicationUser userCheck in listDestinatioUserByTown )
				{
					bool isUserInRole =
					await _userManager.IsInRoleAsync( user: userCheck , role: roleSeparName );

					if ( isUserInRole )
					{
						finalUserDestinationDefault.Add( item: userCheck );
					}
				}

			} // end else

			if ( finalUserDestinationDefault.Count == 0 )
			{
				ViewBag.Message = "!!! کاربری برای این موضوع مشخص نگردیده است";
				return View( model );
			}

			string usernameSender = string.Empty;

			if ( !string.IsNullOrWhiteSpace( inputModel.PersonnelCode ) )
			{
				ApplicationUser result =
					_userManager.Users?
					.Where( w => w.PersonnelCode == inputModel.PersonnelCode )?
					.FirstOrDefault();
				if ( result != null ) usernameSender = result.UserName;
			}

			BroadCast broadCast02 = new BroadCast
			{
				FirstName = inputModel.FirstName ,
				LastName = inputModel.LastName ,
				PersonnelCode = inputModel.PersonnelCode ,
				Subject = inputModel.Subject ,
				Text = inputModel.Text ,
				BroadCastType = DomainEntities.BroadcastAggregate.BroadCastTypeEnum.General ,
				CreateDate = DateTime.Now ,
				UserNameSender = usernameSender ,

			};

			BroadCast broadCast = _broadCastRepository.Add( broadCast02 );
			await _unitOfWork.SaveAsync();

			int broadCastIdAdd = broadCast.Id;

			foreach ( ApplicationUser userDest in finalUserDestinationDefault )
			{
				ReferralBroadCast referralBroadCast = new ReferralBroadCast
				{
					DstUserID = userDest.Id ,
					//BroadCast = broadCast,
					DeadLine = DateTime.Now.AddDays( value: 5 ) ,
					BroadCastId = broadCastIdAdd ,
					IsImmediate = false ,
					Status = ReferralStatusBroadCastEnum.AdameMoshahede ,

				};
				_referralBroadCastRepository.Add( referralBroadCast );
				await _unitOfWork.SaveAsync();
			}

			ViewBag.Message = ".پیام شما با موفقیت ثبت گردید";

			return View( model );
		}
		//777
		[Authorize]
		[Permission]
		[DisplayName( displayName: "لیست مراکز حراست" )]
		[HttpGet]
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		public async Task<IActionResult> ListTowns( string message = "" )
		{
			ViewBag.Message = message;
			var model = await _groupingOfficeRepository.GetGroupingOfficeAll();
			//var model = await _groupingOfficeRepository.GetGroupingOfficeAll();
			return View( model );
		}
		[HttpPost]
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		public async Task<IActionResult> AddTowns( string title )
		{
			_groupingOfficeRepository.Add( new GroupingOffice { Title = title } );
			await _unitOfWork.SaveAsync();
			return RedirectToAction( "ListTowns" );
		}
		[HttpPost]
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		public async Task<IActionResult> DeleteTowns( int? id )
		{
			if ( id.GetValueOrDefault( 0 ) == 0 )
				return RedirectToAction( "ListTowns" );
			var model = await _groupingOfficeRepository.GetById( id );
			_groupingOfficeRepository.Delete( model );
			await _unitOfWork.SaveAsync();
			return RedirectToAction( "ListTowns" );
		}
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		public async Task<IActionResult> EditListTowns( int? id )
		{
			if ( id == null )
				return RedirectToAction( "ListTowns" );
			var townGroup = await _groupingOfficeRepository.GetById( id );
			return Json( townGroup );
		}
		//Update
		[HttpPost]
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		public async Task<IActionResult> UpdateTowns( int? id , string title )
		{
			if ( id == null )
				return RedirectToAction( "ListTowns" );
			var townGroup = await _groupingOfficeRepository.GetById( id );
			townGroup.Title = title;
			_groupingOfficeRepository.Update( townGroup );
			await _unitOfWork.SaveAsync();
			return RedirectToAction( "ListTowns" );
		}
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		public async Task<IActionResult> DeleteTown( int? id )
		{
			if ( id == null )
				return RedirectToAction( "ListTowns" );
			_groupingOfficeRepository.Delete( await _groupingOfficeRepository.GetById( id ) );
			await _unitOfWork.SaveAsync();
			return RedirectToAction( "ListTowns" );
		}
		[Authorize]
		[Permission]
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		[DisplayName( displayName: "نمایش تمام پیام ها" )]
		//[UserLogMessageAttribute]
		public async Task<IActionResult> ShowAllBroadCast()
		{
			//System.Net.IPAddress clientIp = HttpContext.Connection.RemoteIpAddress;

			//string ipTown = ApplicationCommon.TownIP.ValidateIPv4(clientIp.ToString());
			if ( User.IsInRole( role: "admin" ) )
			{
				int countUnRead = await _referralBroadCastRepository.CountUnRead5DayLast();
				ViewBag.CountAdameMoshahede = countUnRead;

				IList<BroadCast> model = ( await _broadCastRepository.ListAllBroadCast() );

				return View( model );
			}
			//(1)
			//	SELECT * FROM dbo.ApplicationUserItems
			//	WHERE id = 111

			int idUserInLogin = User.GetUserId();
			//var broadCastInformationShow =
			//	await _referralBroadCastRepository.GetListtByIdDstUser(idUserInLogin);

			List<BroadCast> daBroad =
				await _referralBroadCastRepository.GetListtByIdDstUserBroadCast( idUserInLogin );


			return View( daBroad );
		}
		[Authorize]
		[Permission]
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		[DisplayName( displayName: "نمایش پیام های خوانده نشده" )]
		public async Task<IActionResult> ShowAllUnReadMessage()
		{
			if ( User.IsInRole( role: "admin" ) )
			{
				IEnumerable<ReferralBroadCast> model =
					( await _referralBroadCastRepository.GetListAllTakhir() );

				return View( model );
			}



			//استخراج کاربران موجود در 4 ناحیه
			IList<ProtectionOfficeMember> usersDefault =
				_protectionOfficeMemberRepositorySubject.ListAll();

			//کاربری که لاگین کرده آیا جزو لیست هست
			var userIsInLogin = User.GetUserId();


			//کاربر در این موضوعات دخیل می باشد
			List<ProtectionOfficeMember> verifyUsers =
				usersDefault.Where( w => w.ApplicationUserId == userIsInLogin )
				.ToList();

			if ( verifyUsers.Count > 0 )
			{
				var subjects = verifyUsers.Select( c => c.ProtectionOffice )
					.Select( c => c.Title );

				List<ReferralBroadCast> final = new List<ReferralBroadCast>();
				foreach ( ReferralBroadCast item in await _referralBroadCastRepository.GetListAllTakhir() )
				{
					foreach ( var subject in subjects )
					{
						if ( item.BroadCast.Subject.Contains( subject ) )
						{
							final.Add( item );
						}
					}
				}

				return View( final.AsEnumerable() );
			}
			return RedirectToAction( actionName: "ShowAllBroadCast" );
		}


		[HttpGet]
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		public async Task<IActionResult> GetDataReferralBroadCasts( int id )
		{
			List<ReferralBroadCast> model =
				await _referralBroadCastRepository.GetListtById( id );

			List<ReferralBroadCastViewModel> broadCastViewModel =
				new List<ReferralBroadCastViewModel>();

			foreach ( ReferralBroadCast pc in model )
			{
				broadCastViewModel.Add( item: new ReferralBroadCastViewModel
				{
					BroadCast = new BroadCastViewModel
					{
						UserNameSender = pc.BroadCast.UserNameSender ,
						CreateDate = pc.BroadCast.CreateDate.Value ,
						Text = pc.BroadCast.Text
					} ,

					ApplicationUser = pc.ApplicationUser ,
					Status = ( ReferralStatusBroadCastEnumViewModel ) pc.Status ,
					DstUserID = ( int ) pc.DstUserID ,
					DeadLine = pc.DeadLine ,
					//SrcUser= (int)pc.SrcUser,
					IsImmediate = pc.IsImmediate ,
					ActionDescription = pc.ActionDescription ,
				} );
			}

			return Json( broadCastViewModel.OrderByDescending( c => c.StatusString ) );
		}
		[HttpGet]
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		public async Task<IActionResult> UsersInfo()
		{

			string roleName = _configuration.GetSection( key: "Separ" ).GetSection( "Role" ).Value;
			//لیست کاربرانی که رول آنها سپر باشد
			IList<ApplicationUser> separ =
				await _userManager.GetUsersInRoleAsync( roleName );


			//var result = _userManager.Users.ToList();
			return Json( separ );
		}
		[HttpGet]
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		public async Task<IActionResult> UsersInfoExist( int id )
		{
			var result = await _groupingOfficeRepository.GetById( id );
			return Json( result );
		}
		[HttpPost]
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		public async Task<IActionResult> AddUser( int protectionMemberId , int [] states )
		{
			try
			{
				GroupingOfficeMember protectionOfficeMember = new();
				protectionOfficeMember.GroupingOfficeId = protectionMemberId;

				foreach ( var item in states )
				{
					var add = new GroupingOfficeMember()
					{
						GroupingOfficeId = protectionMemberId ,
						ApplicationUserId = item ,
					};
					_groupingOfficeMemberRepository.Add( add );
					await _unitOfWork.SaveAsync();
				}
				return RedirectToAction( "ListTowns" );
			}
			catch ( System.Exception )
			{
				return RedirectToAction( "ListTowns" , new { message = "این کاربر قبلاً به اداره دیگری اضافه گردیده است" } );
			}

		}
		[Authorize]
		[Permission]
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		[DisplayName( displayName: "لیست گروه های پیام" )]
		public async Task<IActionResult> ListTownsGroup(/*string message = ""*/)
		{
			//ViewBag.Message = message;
			var model = await _groupingOfficeRepository.GetGroupingOfficeAll();
			return View( model );
		}
		[HttpGet]
		[Authorize]
		[Permission]

		//[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		//(**)
		public async Task<IActionResult> ListTownsGroupAddMember( int? id )
		{
			if ( id == null )
				return RedirectToAction( actionName: "ListTownsGroup" );

			ViewData [ index: "TownsGroupId" ] = id;

			List<GroupingOfficeMember> groupOfficeMember =
				await _groupingOfficeMemberRepository.GetByGroupingOfficeId( id );

			return View( groupOfficeMember );
		}
		[HttpPost]
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]

		public async Task<IActionResult> ListTownsGroupAddMember( int id , string title )
		{
			ViewData [ index: "TownsGroupId" ] = id;
			List<GroupingOfficeMember> groupOfficeMember =
			await _groupingOfficeMemberRepository.GetByGroupingOfficeId( id );


			string userName = string.Empty;
			if ( string.IsNullOrWhiteSpace( title ) )
			{
				ViewData [ index: "Message" ] = "نام کاربری خالی می باشد";
				return View( groupOfficeMember );
			}

			userName = title.Split( separator: '-' ) [ 0 ];

			if ( string.IsNullOrWhiteSpace( userName.Trim() ) )
			{
				ViewData [ index: "Message" ] = "نام کاربری خالی می باشد";
				return View( groupOfficeMember );
			}

			ApplicationUser userByUserName =
					await _userManager.FindByNameAsync( userName );
			GroupingOfficeMember foundedInGroupingByUser =
				await _groupingOfficeMemberRepository.GetByUserIdAndGroupingOfficeId( userByUserName.Id , id );


			if ( foundedInGroupingByUser != null )
			{
				ViewData [ index: "Message" ] = "کاربر انتخابی به این گروه قبلا اضافه گردیده است ";
				return View( groupOfficeMember );
			}

			GroupingOfficeMember re =
				_groupingOfficeMemberRepository.Add( entity: new GroupingOfficeMember
				{
					ApplicationUserId = userByUserName.Id ,
					GroupingOfficeId = id ,
				} );

			await _unitOfWork.SaveAsync();


			groupOfficeMember =
		   await _groupingOfficeMemberRepository.GetByGroupingOfficeId( id );
			ViewData [ index: "Message" ] = "کاربر با موفقیت اضافه گردید";

			return View( groupOfficeMember );
		}
		public async Task<IActionResult> DeleteGroupingOfficeMember( int id )
		{
			var foundedUser =
				await _groupingOfficeMemberRepository.GetByIdentityId( id );

			_groupingOfficeMemberRepository.Delete( foundedUser );
			await _unitOfWork.SaveAsync();
			return RedirectToAction( actionName: "ListTownsGroupAddMember" , new { id = foundedUser.GroupingOfficeId } );
		}
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		public async Task<IActionResult> AddTownsGroup( string title )
		{
			_groupingOfficeRepository.Add( new GroupingOffice { Title = title } );
			await _unitOfWork.SaveAsync();
			return RedirectToAction( "ListTownsGroup" );
		}
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		public async Task<IActionResult> EditListTownsGroup( int? id )
		{
			if ( id == null )
				return RedirectToAction( "ListTownsGroup" );
			var townGroup = await _groupingOfficeRepository.GetById( id );
			return Json( townGroup );
		}

		#region[ForProtection]
		[HttpGet]
		[Authorize]
		[Permission]
		public async Task<IActionResult> ListProtectionAddMember( int? id )
		{
			if ( id == null )
				return RedirectToAction( "ListTownsSubject" );

			ViewData [ index: "TownsGroupId" ] = id;

			List<ProtectionOfficeMember> protectionOfficeMember =
				//await _groupingOfficeMemberRepository.GetByGroupingOfficeId( id );
				await _protectionOfficeMemberRepositorySubject.GetByProtectionOfficeId( id );

			return View( protectionOfficeMember );
		}
		[HttpPost]
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		public async Task<IActionResult> ListProtectionAddMember( int id , string title )
		{
			ViewData [ index: "TownsGroupId" ] = id;
			var protectionOfficeMember =
			await _protectionOfficeMemberRepositorySubject.GetByProtectionOfficeId( id );


			string userName = string.Empty;
			if ( string.IsNullOrWhiteSpace( title ) )
			{
				ViewData [ index: "Message" ] = "نام کاربری خالی می باشد";
				return View( protectionOfficeMember );
			}

			userName = title.Split( separator: '-' ) [ 0 ];

			if ( string.IsNullOrWhiteSpace( userName.Trim() ) )
			{
				ViewData [ index: "Message" ] = "نام کاربری خالی می باشد";
				return View( protectionOfficeMember );
			}

			ApplicationUser userByUserName =
					await _userManager.FindByNameAsync( userName );

			ProtectionOfficeMember foundedInProtectionByUser =
				await _protectionOfficeMemberRepositorySubject
				.GetByUserIdAndProtectionOfficeId( userByUserName.Id , id );


			if ( foundedInProtectionByUser != null )
			{
				ViewData [ index: "Message" ] = "کاربر انتخابی به این گروه قبلا اضافه گردیده است ";
				return View( protectionOfficeMember );
			}

			ProtectionOfficeMember re =
				_protectionOfficeMemberRepositorySubject.Add( entity: new ProtectionOfficeMember
				{
					ApplicationUserId = userByUserName.Id ,
					ProtectionOfficeId = id ,
				} );

			await _unitOfWork.SaveAsync();


			protectionOfficeMember =
		   await _protectionOfficeMemberRepositorySubject.GetByProtectionOfficeId( id );
			ViewData [ index: "Message" ] = "کاربر با موفقیت اضافه گردید";

			return View( protectionOfficeMember );
		}
		public async Task<IActionResult> DeleteProtectionOfficeMember( int id )
		{
			var foundedUser =
				await _protectionOfficeMemberRepositorySubject.GetByIdentityId( id );

			_protectionOfficeMemberRepositorySubject.Delete( foundedUser );
			await _unitOfWork.SaveAsync();
			return RedirectToAction( actionName: "ListProtectionAddMember" , new { id = foundedUser.ProtectionOfficeId } );
		}
		#endregion
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		//Update
		[HttpPost]
		public async Task<IActionResult> UpdateTownsGroup( int? id , string title )
		{
			if ( id == null )
				return RedirectToAction( "ListTownsGroup" );

			var townGroup = await _groupingOfficeRepository.GetById( id );

			townGroup.Title = title;
			_groupingOfficeRepository.Update( townGroup );
			await _unitOfWork.SaveAsync();
			return RedirectToAction( "ListTownsGroup" );
		}

		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		public async Task<IActionResult> DeleteTownGroup( int? id )
		{
			if ( id == null )
				return RedirectToAction( "ListTownsGroup" );
			_groupingOfficeRepository.Delete( await _groupingOfficeRepository.GetById( id ) );
			await _unitOfWork.SaveAsync();
			return RedirectToAction( "ListTownsGroup" );
		}
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		public async Task<IActionResult> AddUserGroup( int protectionMemberId , int [] states )
		{
			try
			{


				await _groupingOfficeMemberRepository.DeleteByGroupingOfficeMemberListAsync( protectionMemberId );
				await _unitOfWork.SaveAsync();

				GroupingOfficeMember groupingOfficeMember = new GroupingOfficeMember();

				groupingOfficeMember.GroupingOfficeId = protectionMemberId;

				foreach ( int item in states )
				{
					GroupingOfficeMember add = new GroupingOfficeMember
					{
						GroupingOfficeId = protectionMemberId ,
						ApplicationUserId = item ,
					};

					GroupingOfficeMember groupingOfficeMemberInfo = _groupingOfficeMemberRepository.Add( add );

					await _unitOfWork.SaveAsync();
				}
				return RedirectToAction( actionName: "ListTownsGroup" );
			}
			catch ( System.Exception )
			{
				return RedirectToAction( actionName: "ListTownsGroup" , new { message = "این کاربر قبلاً به اداره دیگری اضافه گردیده است" } );
			}

		}
		//
		[HttpGet]
		public async Task<IActionResult> UsersInfoGroup()
		{
			string roleName = _configuration.GetSection( "Separ" ).GetSection( "Role" ).Value;
			//لیست کاربرانی که رول آنها سپر باشد
			var separ = await _userManager.GetUsersInRoleAsync( roleName );


			//var result = _userManager.Users.ToList();
			return Json( separ );
		}
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		[HttpGet]
		public async Task<IActionResult> UsersInfoExistGroup( int id )
		{
			var result = await _groupingOfficeMemberRepository.GetByGroupingOfficeId( id );
			return Json( result );
		}

		//777 --> End

		//888 --> begin------------------------------------------------------------------------

		[Authorize]
		[Permission]
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		[DisplayName( displayName: "لیست گروه های پیام" )]
		public async Task<IActionResult> ListTownsSubject()
		{
			//ViewBag.Message = message;
			var model = await _protectionOfficeRepositorySubject.GetProtectionOfficeAll(); ;
			return View( model );
		}
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		//AddTownsGroup
		public async Task<IActionResult> AddTownsSubject( string title )
		{
			_protectionOfficeRepositorySubject.Add( new ProtectionOffice { Title = title } );
			await _unitOfWork.SaveAsync();
			return RedirectToAction( "ListTownsSubject" );
		}
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		public async Task<IActionResult> EditListTownsSubject( int? id )
		{
			if ( id == null )
				return RedirectToAction( "ListTownsSubject" );
			var townGroup = await _protectionOfficeRepositorySubject.GetById( id );
			return Json( townGroup );
		}
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		[HttpPost]
		public async Task<IActionResult> UpdateTownsSubject( int? id , string title )
		{
			if ( id == null )
				return RedirectToAction( "ListTownsSubject" );
			var townGroup = await _protectionOfficeRepositorySubject.GetById( id );
			townGroup.Title = title;
			_protectionOfficeRepositorySubject.Update( townGroup );
			await _unitOfWork.SaveAsync();
			return RedirectToAction( "ListTownsSubject" );
		}
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		public async Task<IActionResult> DeleteTownSubject( int? id )
		{
			if ( id == null )
				return RedirectToAction( "ListTownsSubject" );
			_protectionOfficeRepositorySubject.Delete( await _protectionOfficeRepositorySubject.GetById( id ) );
			await _unitOfWork.SaveAsync();
			return RedirectToAction( "ListTownsSubject" );
		}
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		public async Task<IActionResult> AddUserSubject( int protectionMemberId , int [] states )
		{
			try
			{
				await _protectionOfficeMemberRepositorySubject.DeleteByprotectionMemberIdListAsync( protectionMemberId );
				await _unitOfWork.SaveAsync();

				ProtectionOfficeMember protectionOfficeMember = new();
				protectionOfficeMember.ProtectionOfficeId = protectionMemberId;

				foreach ( var item in states )
				{
					var add = new ProtectionOfficeMember()//GroupingOfficeMember()
					{
						//GroupingOfficeId = protectionMemberId,
						ProtectionOfficeId = protectionMemberId ,
						ApplicationUserId = item ,
					};
					//_protectionOfficeMemberRepository.Add(add);
					_protectionOfficeMemberRepositorySubject.Add( add );
					await _unitOfWork.SaveAsync();
				}
				return RedirectToAction( "ListTownsSubject" );
			}
			catch ( System.Exception )
			{
				return RedirectToAction( "ListTownsSubject" , new { message = "این کاربر قبلاً به اداره دیگری اضافه گردیده است" } );
			}

		}
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		[HttpGet]
		public async Task<IActionResult> UsersInfoSubject()
		{
			string roleName = _configuration.GetSection( "Separ" ).GetSection( "Role" ).Value;
			//لیست کاربرانی که رول آنها سپر باشد
			var separ = await _userManager.GetUsersInRoleAsync( roleName );


			//var result = _userManager.Users.ToList();
			return Json( separ );
		}
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		[HttpGet]
		public async Task<IActionResult> UsersInfoExistSubject( int id )
		{
			var result = await _protectionOfficeMemberRepositorySubject.GetById( id );
			return Json( result );
		}
		//88 --> end----------------------------------------------------------------------------
		[Permission]
		[Authorize( Roles = "admin,developer" )]
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		[HttpGet]
		[DisplayName( displayName: "لیست نمایشی لاگ" )]
		public IActionResult UserLogMessage()
		{
			return View();
		}
		public IActionResult GetUserLogMessageData( string models )
		{
			var request = JsonConvert.DeserializeObject<DataSourceRequest>( models );
			var model = ( _userLogMessageRepository.GetList( request ) );
			return Json( model );
		}
		//[HttpGet]
		[Permission]
		[DisplayName( displayName: "مشاهده جزییات" )]
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		public async Task<IActionResult> ShowDetails( int id )
		{
			var result = await _referralBroadCastRepository.GetReferralBroadCastFromBroadCast( id );
			// Update Status

			//اما دارد !!!
			result.Status = ReferralStatusBroadCastEnum.MoshahedeShode;

			_referralBroadCastRepository.Update( result );
			await _unitOfWork.SaveAsync();

			ReferralBroadCastViewModel model = new ReferralBroadCastViewModel();

			//bind Model
			model.BroadCastId = result.Id;
			model.BroadCast.Subject = result.BroadCast.Subject;
			model.BroadCast.Text = result.BroadCast.Text;
			model.DeadLine = result.DeadLine;
			model.ActionDescription = result.ActionDescription;
			model.DeadLine = result.DeadLine;

			return PartialView( viewName: "_Details" , model );
		}
		//ActionEghdam
		[HttpPost]
		[Permission]
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		public async Task<string> ActionEghdam( int broadCastId , string actionDescription )
		{
			var result = await _referralBroadCastRepository.GetByIdUpdate( broadCastId );
			result.ActionDescription = actionDescription;
			result.Status = ReferralStatusBroadCastEnum.EghdamShode;
			_referralBroadCastRepository.Update( result );
			await _unitOfWork.SaveAsync();
			//return RedirectToAction("ShowAllBroadCast");
			return "اقدام مورد نظر با موفقیت ثبت گردید";
		}
		[HttpPost]
		[ServiceFilter( typeof( UserLogMessageAttribute ) )]
		public async Task<IActionResult> ReplyMessage( int broadCastId , int userId )
		{
			try
			{
				BroadCast broadCast =
				await _broadCastRepository.GetById( broadCastId );



				var userDestination =
					await _userManager.FindByIdAsync( userId.ToString() );


				var newBroaCast =
					new BroadCast
					{
						BroadCastType = broadCast.BroadCastType ,
						CreateDate = DateTime.Now ,
						FirstName = userDestination.FirstName ,//broadCast.FirstName ,
						LastName = userDestination.LastName ,//broadCast.LastName ,
						PersonnelCode = broadCast.PersonnelCode ,
						Subject = broadCast.Subject ,
						Text = broadCast.Text ,
						UserNameSender = broadCast.UserNameSender ,
						RecordStatus = broadCast.RecordStatus ,
						//ReferralBroadCasts = broadCast.ReferralBroadCasts,

					};


				BroadCast broadCastAdd = _broadCastRepository.Add( newBroaCast );
				//Error
				await _unitOfWork.SaveAsync();

				int broadCastIdAdd = broadCastAdd.Id;


				//(2)
				var refferalAdd =
					await _referralBroadCastRepository.GetById( broadCastId );

				ReferralBroadCast referralBroadCast = new ReferralBroadCast
				{
					DstUserID = userDestination.Id ,
					//BroadCast = broadCast,
					DeadLine = DateTime.Now.AddDays( value: 5 ) ,
					BroadCastId = broadCastIdAdd ,
					IsImmediate = false ,
					Status = ReferralStatusBroadCastEnum.AdameMoshahede ,

				};
				_referralBroadCastRepository.Add( referralBroadCast );
				await _unitOfWork.SaveAsync();

				return RedirectToAction( actionName: "ShowAllUnReadMessage" );
			}
			catch ( Exception ex )
			{
				string message = ex.Message;
				string inerMessage = ex.InnerException?.Message;
				throw;
			}
			return View();
		}
	}
}