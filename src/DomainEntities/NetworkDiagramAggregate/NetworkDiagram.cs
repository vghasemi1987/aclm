using System.Collections.Generic;

namespace DomainEntities.NetworkDiagramAggregate
{
	public class Node
	{
		public int id { get; set; }

		public string label { get; set; }

		public string image { get; set; }

		public string shape { get; set; }

		public string color { get; set; }
	}

	public class Edges
	{
		public int from { get; set; }

		public int to { get; set; }

		public string arrows { get; set; }

		public string title { get; set; }

		public int width { get; set; }

		public Color color { get; set; }
		public EdgeSmooth smooth { get; set; }

		public class EdgeSmooth
		{
			public string type { get; set; }
			public double roundness { get; set; }
		}
	}

	public class Color
	{
		public string color { get; set; }
	}

	public class DataModel
	{
		public IList<Node> nodes { get; set; }
		public IList<Edges> edges { get; set; }
	}
}
