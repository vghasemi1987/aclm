using DomainContracts.NetworkDiagramAggregate;
using DomainContracts.RouterAggregate;
using DomainContracts.ServiceAggregate;
using DomainContracts.SystemsAggregate;
using DomainEntities.Commons;
using DomainEntities.NetworkDiagramAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Threading.Tasks;
using Web.Core.NetworkDiagram.ViewModels;
using Web.Extensions;

namespace Web.Core.NetworkDiagrams
{
	[DisplayName("گراف دسترسی")]
	public class NetworkDiagramsController : Controller
	{
		private readonly ISystemsRepository _systemsRepository;
		private readonly IServiceRepository _serviceRepository;
		private readonly IRouterRepository _routerRepository;
		private readonly INetworkDiagramService _networkDiagramService;

		public NetworkDiagramsController(ISystemsRepository systemsRepository,
										IServiceRepository serviceRepository,
										IRouterRepository routerRepository,
										INetworkDiagramService networkDiagramService)
		{
			_systemsRepository = systemsRepository;
			_serviceRepository = serviceRepository;
			_routerRepository = routerRepository;
			_networkDiagramService = networkDiagramService;
		}
		[DisplayName("نمایش گراف")]
		public async Task<IActionResult> Index()
		{
			var r = new SearchViewModel
			{
				SystemList = new SelectList(_networkDiagramService.GetDropDownList(User.GetUserId()), nameof(DropDownDto.Id), nameof(DropDownDto.Title)),
				ServiceList = new SelectList(await _serviceRepository.GetDropDownList(), nameof(DropDownDto.Id), nameof(DropDownDto.Title)),
				RouterList = new SelectList(await _routerRepository.GetDropDownList(), nameof(DropDownDto.Id), nameof(DropDownDto.Title)),
			};
			return View(r);
		}
		public async Task<IActionResult> Search()
		{
			return PartialView("_Detail", new SearchViewModel
			{
				SystemList = new SelectList(await _systemsRepository.GetDropDownListAsync(), nameof(DropDownDto.Id), nameof(DropDownDto.Title)),
				ServiceList = new SelectList(await _serviceRepository.GetDropDownList(), nameof(DropDownDto.Id), nameof(DropDownDto.Title)),
				RouterList = new SelectList(await _routerRepository.GetDropDownList(), nameof(DropDownDto.Id), nameof(DropDownDto.Title)),
			});
		}
		[HttpPost]
		public string GetNetworkDiagram([FromBody] SearchViewModel model)
		{
			var ipIsAccessSourceIp = _networkDiagramService.IPIsAccess(User.GetUserId(), model.SourceIp);
			var ipIsAccessDestinationIp = _networkDiagramService.IPIsAccess(User.GetUserId(), model.DestinationIp);

			if (ipIsAccessSourceIp && ipIsAccessDestinationIp)
			{
				var network = _networkDiagramService.GetNetworkDiagram(new NetworkDiagramFilter
				{
					AccessCount = model.AccessCount,
					Address = model.Address,
					DestinationIp = model.DestinationIp,
					Level = model.Level,
					RouterId = model.RouterId,
					ServiceId = model.ServiceId,
					SourceIp = model.SourceIp,
					SystemId = model.SystemId,
					UserId = User.GetUserId(),
					PersonnelCode = model.PersonelCode,
					RecoveryDate = model.RecoveryDate,
					ImportanceFactor = model.ImportanceFactor,
				});

				SetNetworkImages(network);
				return JsonConvert.SerializeObject(network);
			}
			else
			{
				return JsonConvert.SerializeObject(null);
			}

		}

		private DataModel SetNetworkImages(DataModel network)
		{
			foreach (var node in network.nodes)
			{
				node.image = Url.Content($"~/images/network/{node.image}");
			}
			return network;
		}
	}
}
