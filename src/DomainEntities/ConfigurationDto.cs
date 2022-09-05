namespace DomainEntities
{
	public class ConfigurationDto
	{
		public ConfigurationDto(string connectionString)
		{
			ConnectionString = connectionString;
		}
		public string ConnectionString { get; private set; }
	}
}
