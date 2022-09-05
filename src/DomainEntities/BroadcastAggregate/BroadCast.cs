using System;
using DomainEntities.Commons;
using System.Collections.Generic;

namespace DomainEntities.BroadcastAggregate
{
	public class BroadCast : Entity<int>
	{
		public string UserNameSender { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PersonnelCode { get; set; }
		public string Subject { get; set; }
		public string Text { get; set; }
		public string FileAddress { get; set; }
		public string FileName { get; set; }
		public DateTime? CreateDate { get; set; }
		public BroadCastTypeEnum BroadCastType { get; set; }
		public List<ReferralBroadCast> ReferralBroadCasts { get; set; }
		public BroadCast()
		{
			ReferralBroadCasts = new List<ReferralBroadCast>();
		}
		//Ignore Property
		public string CreateDatePersian
		{
			get
			{
				System.Globalization.PersianCalendar pcr = new System.Globalization.PersianCalendar();
				try
				{
					if (CreateDate.Value != null)
					{
						string result = $"{pcr.GetYear((DateTime)CreateDate)}/{pcr.GetMonth((DateTime)CreateDate)}/{pcr.GetDayOfMonth((DateTime)CreateDate)} --  {CreateDate.Value.Hour}:{CreateDate.Value.Minute}";
						return result;
					}
					return "ندارد";
				}
				catch (Exception ex)
				{
					return "ندارد";
				}


			}
		}
	}
	public enum BroadCastTypeEnum
	{
		Immediate, General,
	}
}