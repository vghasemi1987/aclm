using Dapper;
using DomainContracts.Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Infrastructure.Data.Dapper
{
	public class DapperRepository : IDapperRepository
	{
		private readonly IConfiguration _config;
		public DapperRepository(IConfiguration config)
		{
			_config = config;
		}
		public IDbConnection Connection
		{
			get
			{
				return new SqlConnection(_config.GetConnectionString("DatabaseConnection"));
			}
		}

		public void ExecuteNonQueryProcedure(string procName, object param)
		{
			var parameters = new DynamicParameters(param);
			Connection.Query(sql: procName, param: parameters, commandType: CommandType.StoredProcedure);
		}

		public IEnumerable<T> ExecuteQuery<T>(string query, object param)
		{
			var parameters = new DynamicParameters(param);
			return Connection.Query<T>(query, param: parameters, commandType: CommandType.StoredProcedure).ToList();
		}
	}
}
