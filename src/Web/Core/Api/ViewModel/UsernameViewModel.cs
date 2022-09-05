namespace Web.Core.Api.ViewModel
{
	public class UsernameViewModel
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public bool IsEncrypted { get; set; }
		public UsernameViewModel()
		{
			Username = string.Empty;
			Password = string.Empty;
			IsEncrypted = false;
		}
	}
}