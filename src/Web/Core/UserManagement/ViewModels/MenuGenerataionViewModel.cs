using System;
using System.Collections.Generic;

namespace Web.Core.UserManagement.ViewModels
{
    public class MenuGenerataionViewModel : IEquatable<MenuGenerataionViewModel>
	{
		public string Text { get; set; }
		public string Link { get; set; }
		public string Icon { get; set; }
		public List<MenuGenerataionChildViewModel> Items { get; set; }

		public bool Equals(MenuGenerataionViewModel other)
		{
			if (Text == other.Text && Icon == other.Icon && Link == other.Link)
				return true;

			return false;
		}

		public override int GetHashCode()
		{
			int hashText = Text == null ? 0 : Text.GetHashCode();
			int hashLink = Link == null ? 0 : Link.GetHashCode();
			int hashIcon = Icon == null ? 0 : Icon.GetHashCode();

			return hashText ^ hashLink ^ hashIcon;
		}
	}
	public class MenuGenerataionChildViewModel
	{
		public string Text { get; set; }
		public string Link { get; set; }
		public string Icon { get; set; }
	}
}
