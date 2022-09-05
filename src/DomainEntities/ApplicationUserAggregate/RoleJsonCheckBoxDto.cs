using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities.ApplicationUserAggregate
{
	public class RoleJsonCheckBoxDto
	{
		public int Id { get; set; }
		public string Text { get; set; }
		public bool Checked { get; set; } = false;
	}
}
