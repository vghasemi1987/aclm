using ApplicationCommon;
using DomainContracts.AccessibilityAggregate;
using DomainContracts.AccessRangeAggregate;
using DomainContracts.ActionTypeAggregate;
using DomainContracts.ApplicationUserAggregate;
using DomainContracts.NetworkDiagramAggregate;
using DomainContracts.ProtocolAggregate;
using DomainContracts.ServiceAggregate;
using DomainContracts.SystemsAggregate;
using DomainEntities.AccessibilityAggregate;
using DomainEntities.Commons;
using DomainEntities.NetworkDiagramAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Text.RegularExpressions;

namespace DomainApplication.Services.NetworkDiagramAggregate
{
	public class NetworkDiagramService : INetworkDiagramService
	{
		private readonly IAccessibilityRepository _accessibilityRepository;
		private readonly IServiceRepository _serviceRepository;
		private readonly ISystemsRepository _systemRepository;
		private readonly IActionTypeRepository _actionTypeRepository;
		private readonly IProtocolRepository _protocolRepository;
		private readonly IApplicationUserAccessRageHeaderRepository _applicationUserAccessRageHeaderRepository;
		private readonly IAccessRangeDetailRepository _accessRangeDetailRepository;

		public NetworkDiagramService(IAccessibilityRepository accessibilityRepository,
									 IServiceRepository serviceRepository,
									 ISystemsRepository systemRepository,
									 IActionTypeRepository actionTypeRepository,
									 IProtocolRepository protocolRepository,
									 IAccessRangeDetailRepository accessRangeDetailRepository,
									 IApplicationUserAccessRageHeaderRepository applicationUserAccessRageHeaderRepository)
		{
			_accessibilityRepository = accessibilityRepository;
			_serviceRepository = serviceRepository;
			_systemRepository = systemRepository;
			_actionTypeRepository = actionTypeRepository;
			_protocolRepository = protocolRepository;
			_applicationUserAccessRageHeaderRepository = applicationUserAccessRageHeaderRepository;
			_accessRangeDetailRepository = accessRangeDetailRepository;
		}

		public static bool IsInRange(string startIpAddr, string endIpAddr, string address)
		{
			IPAddress a;
			if (IPAddress.TryParse(startIpAddr, out a) && IPAddress.TryParse(endIpAddr, out a) && IPAddress.TryParse(address, out a))
			{
				long ipStart = BitConverter.ToInt32(IPAddress.Parse(startIpAddr).GetAddressBytes().Reverse().ToArray(), 0);
				long ipEnd = BitConverter.ToInt32(IPAddress.Parse(endIpAddr).GetAddressBytes().Reverse().ToArray(), 0);
				long ip = BitConverter.ToInt32(IPAddress.Parse(address).GetAddressBytes().Reverse().ToArray(), 0);
				return ip >= ipStart && ip <= ipEnd; //edited
			}
			else
			{
				return false;
			}

		}

		public bool IPIsAccess(int userId, string ipAddress)
		{
			if (ipAddress == "" || ipAddress == null)
			{
				return true;
			}
			else
			{
				var accessRageHeaderIdList = _applicationUserAccessRageHeaderRepository.GetListByUserId(userId).
															Select(q => q.AccessRangeHeaderId).ToList();
				var accessRageDetailList = _accessRangeDetailRepository.FindByHeaderId(accessRageHeaderIdList);
				var isRange = false;
				if (accessRageDetailList.Any())
				{
					foreach (var item in accessRageDetailList)
					{
						if (IsInRange(item.IpFrom, item.IpTo, ipAddress))
						{
							isRange = true;
						}
					}
				}
				else
				{
					isRange = true;
				}
				return isRange;
			}

		}

		public List<DropDownDto> GetDropDownList(int userId)
		{
			var systems = _systemRepository.GetDropDownList();
			var result = new List<DropDownDto>();
			var accessRageHeaderIdList = _applicationUserAccessRageHeaderRepository.GetListByUserId(userId).
											Select(q => q.AccessRangeHeaderId).ToList();
			var accessRageDetailList = _accessRangeDetailRepository.FindByHeaderId(accessRageHeaderIdList);
			foreach (var system in systems)
			{
				var isRange = false;
				if (accessRageDetailList.Any())
				{
					foreach (var item in accessRageDetailList)
					{
						if (IsInRange(item.IpFrom, item.IpTo, system.IpAddress))
						{
							isRange = true;
						}
					}
				}
				if (isRange || !accessRageDetailList.Any())
				{
					result.Add(new DropDownDto
					{
						Id = system.Id,
						IpAddress = system.IpAddress,
						Title = system.Title,
					});
				}
			}
			return result;
		}
		//
		public DataModel GetNetworkDiagram(NetworkDiagramFilter filter)
		{
			#region Declare Variable
			var desIpList = new List<string>();
			var level2AccessList = new List<Accessibility>();
			var nodes = new List<Node>();
			var edges = new List<Edges>();
			var node = new Node();
			var color = new Color();
			#endregion

			#region Get Lists
			var serviceList = _serviceRepository.GetList();
			var systemList = _systemRepository.GetList();
			var accessRageHeaderIdList = _applicationUserAccessRageHeaderRepository.GetListByUserId(filter.UserId).
											Select(q => q.AccessRangeHeaderId).ToList();
			var accessRageDetailList = _accessRangeDetailRepository.FindByHeaderId(accessRageHeaderIdList);
			#endregion

			#region Generate Query
			var query = _accessibilityRepository.GetListAsQueryable();

			/*if (accessRageDetailList.Any())
            {
                var predicate = PredicateBuilder.True<Accessibility>();
                foreach (var item in accessRageDetailList)
                {
                    predicate = predicate.Or(p => string.Compare(p.SourceIp, item.IpFrom) >= 0 ||
                                                  string.Compare(p.DestinationIp, item.IpTo) <= 0);
                }
                query = query.Where(predicate);
            }
            */
			if (filter.ImportanceFactor > 0)
			{
				query = query.Include(o => o.SourceSystem)
					.Where(p => p.SourceSystem.ImportanceFactor == filter.ImportanceFactor);
			}
			if (filter.SystemId.HasValue)
			{
				filter.Address = systemList.FirstOrDefault(q => q.Id == filter.SystemId).IpAddress;
			}

			if (filter.PersonnelCode != 0)
			{
				var person = systemList.FirstOrDefault(q => q.PersonelCode == filter.PersonnelCode);
				query = query.Where(p => p.SourceSystemId == person.Id || p.DestinationSystemId == person.Id ||
									p.SourceIp == person.IpAddress || p.DestinationIp == person.IpAddress);
			}

			if (filter.ServiceId.HasValue)
			{
				var service = serviceList.FirstOrDefault(q => q.Id == filter.ServiceId);
				query = query.Where(q => q.Id == filter.ServiceId ||
									q.SourcePort == service.Port.ToString() || q.DestinationPort == service.Port.ToString() ||
									q.Port == service.Port.ToString() || q.Port == service.Name ||
									q.DestinationPort == service.Port.ToString() || q.DestinationPort == service.Name ||
									q.SourcePort == service.Port.ToString() || q.SourcePort == service.Name);
			}

			if (filter.RouterId.HasValue)
			{
				query = query.Where(q => q.RouterId == filter.RouterId);
			}

			if (filter.AccessCount > 0)
			{
				desIpList = _accessibilityRepository.GetAccessibilityReportByCount(filter.AccessCount)
							.Select(p => p.DestinationIp).ToList();
				query = query.Where(p => desIpList.Contains(p.DestinationIp));
			}
			//(1)
			if (filter.Address.HasValue())
			{
				query = query.Where(q => q.SourceIp.Contains(filter.Address) || q.DestinationIp.Contains(filter.Address));
			}

			if (filter.Level == 2)
			{
				var query2 = _accessibilityRepository.GetListAsQueryable();
				level2AccessList = (from q in query
									from q2 in query2
									where q.SourceIp == q2.DestinationIp ||
										  q.SourceIp == q2.SourceIp ||
										  q.DestinationIp == q2.SourceIp ||
										  q.DestinationIp == q2.DestinationIp
									select q2).ToList();
			}

			if (filter.SourceIp.HasValue() && filter.DestinationIp.HasValue())
			{
				query = query.Where(q => q.SourceIp == filter.SourceIp && q.DestinationIp == filter.DestinationIp);
			}

			else if (filter.SourceIp.HasValue())
			{
				query = query.Where(q => q.SourceIp == filter.SourceIp);
			}

			else if (filter.DestinationIp.HasValue())
			{
				query = query.Where(q => q.DestinationIp == filter.DestinationIp);
			}

			if (filter.RecoveryDate.HasValue())
			{
				query = query.Where(q => q.VersionDate == filter.RecoveryDate.ToMiladiDate() && q.RecordStatus == Convert.ToBoolean(RecordStatus.Deleted));
			}
			//(2)
			else
			{
				query = query.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted));
			}

			var accessList = query.OrderByDescending(p => p.SourceIp).ToList().Union(level2AccessList, new AccessComparer());

			#endregion

			#region Create Nodes & Edges
			var id = 1;
			foreach (var access in accessList)
			{
				var width = 1;
				var sourceLabel = access.SourceIp;
				var sourceSystemName = systemList.FirstOrDefault(p => p.IpAddress == access.SourceIp)?.SystemName;

				if (sourceSystemName != null)
					sourceLabel += "(" + sourceSystemName + ")";

				var source = nodes.FirstOrDefault(n => GetLabelIp(n.label) == GetLabelIp(sourceLabel));
				var sourceId = 0;
				int? sourceAccesslevel = 0;

				if (source == null)
				{
					sourceId = id;
					sourceAccesslevel = access.SourceSystemId.HasValue ?
										systemList.FirstOrDefault(q => q.Id == access.SourceSystemId.Value)?.AccessibilityLevelId : 0;
					var isRange = false;
					if (accessRageDetailList.Any())
					{
						foreach (var item in accessRageDetailList)
						{
							var sourceLabelSplit = access.SourceIp.Split('/')[0];
							if (IsInRange(item.IpFrom, item.IpTo, sourceLabelSplit))
							{
								isRange = true;
							}
						}
					}
					if (isRange || !accessRageDetailList.Any())
					{
						nodes.Add(new Node
						{
							id = id,
							label = sourceLabel,
							image = GetIcon(access.SourceIp, sourceSystemName, sourceAccesslevel.Value, filter.Address),
							shape = "image"
						});
					}
					id++;
				}
				else
				{
					sourceId = source.id;
				}
				var destLabel = access.DestinationIp;
				var desSystemName = systemList.FirstOrDefault(p => p.IpAddress == access.DestinationIp)?.SystemName;
				if (desSystemName != null)
					destLabel += "(" + desSystemName + ")";

				var dest = nodes.FirstOrDefault(n => GetLabelIp(n.label) == GetLabelIp(destLabel));
				var destId = 0;
				int? destAccesslevel = 0;

				if (dest == null)
				{
					destId = id;
					destAccesslevel = access.DestinationSystemId.HasValue ?
					systemList.FirstOrDefault(q => q.Id == access.DestinationSystemId.Value).AccessibilityLevelId : 0;
					var isRange = false;
					if (accessRageDetailList.Any())
					{
						foreach (var item in accessRageDetailList)
						{
							var sourceLabelSplit = access.DestinationIp.Split('/')[0];
							if (IsInRange(item.IpFrom, item.IpTo, sourceLabelSplit))
							{
								isRange = true;
							}
						}
					}
					if (isRange || !accessRageDetailList.Any())
					{
						nodes.Add(new Node
						{
							id = id,
							label = destLabel,
							image = GetIcon(access.DestinationIp, desSystemName, destAccesslevel.Value, filter.Address, desIpList.Any()),
							shape = "image"
						});
					}
					id++;
				}
				else
				{
					destId = dest.id;
				}
				var dsc = "Action:" + access.ActionType + "\n ; Protocol:" + access.Protocol;
				dsc += access.SourcePort.HasValue() ? "\n ; Source Port:" + access.SourcePort : string.Empty;
				dsc += access.DestinationPort.HasValue() ? "\n ; Destination Port:" + access.DestinationPort : string.Empty;

				width = GetWidth(access.SourceIp);

				if (access.ActionType == "deny")
					color.color = "#db4437";
				else
					color.color = "#07508E";

				var senstivePortList = serviceList.Where(s => s.ServiceLevelId == 4);
				if (senstivePortList.Any())
				{
					if (senstivePortList.Select(q => q.Id).Any(q => q == access.DestinationServiceId || q == access.ServiceId))
						color.color = "#f71c1c";
				}
				edges.Add(new Edges
				{
					from = sourceId,
					to = destId,
					arrows = "to",
					title = dsc,
					width = width,
					color = color,
				});
			}
			var smoothes = GetSmooths();

			var edgesGroup = edges.GroupBy(p => new { p.from, p.to, p.title }).
								   Select(p => new { p.Key.from, p.Key.to, p.Key.title }).ToList();

			int edgeSmoothIndex = 0;
			var firstEdge = edgesGroup.FirstOrDefault();
			for (int i = 0; i < edgesGroup.Count; i++)
			{
				var edge = edgesGroup[i];
				if (edge.from != firstEdge.from || edge.to != firstEdge.to || edgeSmoothIndex > 19)
				{
					edgeSmoothIndex = 0;
					firstEdge = edge;
				}
				var edgeList = edges.Where(e => e.from == edge.from &&
										   e.to == edge.to &&
										   e.title == edge.title &&
										   e.smooth == null).ToList();

				foreach (var item in edgeList)
				{
					item.smooth = smoothes[edgeSmoothIndex];
				}
				edgeSmoothIndex++;
			}
			#endregion

			return new DataModel { nodes = nodes, edges = edges };
		}
		private string GetLabelIp(string label)
		{
			var regexResult = Regex.Match(label, @"^((\d{0,3}\.){3}(\d{0,3}){1})", RegexOptions.IgnoreCase);
			if (regexResult.Success)
			{
				return regexResult.Value;
			}
			return label;
		}
		private List<Edges.EdgeSmooth> GetSmooths()
		{
			return new List<Edges.EdgeSmooth> {
					new Edges.EdgeSmooth {roundness = 0.5 ,type = "curvedCW" },
					new Edges.EdgeSmooth {roundness = 0.5 ,type = "curvedCCW" },
					new Edges.EdgeSmooth {roundness = 1.0 ,type = "curvedCW" },
					new Edges.EdgeSmooth {roundness = 1.0 ,type = "curvedCCW" },
					new Edges.EdgeSmooth {roundness = 0.2 ,type = "curvedCW" },
					new Edges.EdgeSmooth {roundness = 0.2 ,type = "curvedCCW" },
					new Edges.EdgeSmooth {roundness = 0.7 ,type = "curvedCW" },
					new Edges.EdgeSmooth {roundness  = 0.7,type = "curvedCCW" },
					new Edges.EdgeSmooth {roundness = 0.3 ,type = "curvedCW" },
					new Edges.EdgeSmooth {roundness = 0.3 ,type = "curvedCCW" },
					new Edges.EdgeSmooth {roundness = 0.8 ,type = "curvedCW" },
					new Edges.EdgeSmooth {roundness = 0.8 ,type = "curvedCCW" },
					new Edges.EdgeSmooth {roundness = 0.1 ,type = "curvedCW" },
					new Edges.EdgeSmooth {roundness = 0.1 ,type = "curvedCCW" },
					new Edges.EdgeSmooth {roundness = 0.9 ,type = "curvedCW" },
					new Edges.EdgeSmooth {roundness = 0.9 ,type = "curvedCCW" },
					new Edges.EdgeSmooth {roundness = 0.4 ,type = "curvedCW" },
					new Edges.EdgeSmooth {roundness = 0.4 ,type = "curvedCCW" },
					new Edges.EdgeSmooth {roundness = 0.6 ,type = "curvedCW" },
					new Edges.EdgeSmooth {roundness = 0.6 ,type = "curvedCCW" }
				};
		}
		private string GetIcon(string address, string system, int accesslevel, string searchAddress, bool hasMoreAccess = false)
		{
			string networkImage = "networking.png", hostImage = "computer10.png", anyImage = "cloud.png", serverImage = "server.png",
				   matchedHostImage = "computer12.png", matchedServerImage = "server-2.png";

			if (accesslevel == 1)
				hostImage = "computer1.png";
			else if (accesslevel == 2)
				hostImage = "computer9.png";
			else if (accesslevel == 3)
				hostImage = "computer3.png";
			else if (accesslevel == 4)
				hostImage = "computer8.png";

			if (searchAddress == address)
				hostImage = matchedHostImage;

			if (hasMoreAccess)
				hostImage = "computer2.png";

			if (address.ToLower() == "any")
				return anyImage;
			else if (address.Contains("/"))
				return networkImage;
			else if (!string.IsNullOrEmpty(system))
				return address == searchAddress ? matchedServerImage : serverImage;
			else
				return hostImage;
		}
		private int GetWidth(string address)
		{
			if (address.ToLower() == "any")
				return 3;
			else if (address.Contains("/"))
				return 2;
			else
				return 1;
		}
	}
}

class AccessComparer : IEqualityComparer<Accessibility>
{
	public bool Equals(Accessibility x, Accessibility y)
	{
		return x.Id == y.Id;
	}

	public int GetHashCode(Accessibility obj)
	{
		return obj.Id;
	}
}