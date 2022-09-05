using System.Collections.Generic;

namespace DomainContracts.Dapper
{
	public interface IDapperRepository
	{
		IEnumerable<T> ExecuteQuery<T>(string query, object param);
		void ExecuteNonQueryProcedure(string procName, object param);
	}
}
