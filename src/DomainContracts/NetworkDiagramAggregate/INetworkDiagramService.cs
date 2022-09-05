using DomainEntities.Commons;
using DomainEntities.NetworkDiagramAggregate;
using System.Collections.Generic;

namespace DomainContracts.NetworkDiagramAggregate
{
	public interface INetworkDiagramService
	{
		DataModel GetNetworkDiagram(NetworkDiagramFilter filter);
		List<DropDownDto> GetDropDownList(int userId);
		bool IPIsAccess(int userId, string ipAddress);
	}
}
