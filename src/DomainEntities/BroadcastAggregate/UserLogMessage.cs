using DomainEntities.Commons;
using System;
//using ApplicationCommon;

namespace DomainEntities.BroadcastAggregate
{
	public class UserLogMessage : Entity<int>
	{
		public string UserName { get; set; }
		public string Action { get; set; }
		public string Controller { get; set; }
		public string Description { get; set; }
		public DateTime DateActivity { get; set; }
		public string IP { get; set; }
		//	public string PersianDate
		//	{
		//		get
		//		{
		//			string result = DateActivity
		//		}
		//	}
	}
}