using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Live
{
	abstract class Unit
	{
		public double x { get; set; }
		public double y { get; set; }
		public World World { get; set; }
		public string Color { get; set; }
		public float Size { get; set; }
		public Unit(double _x, double _y, World world)
		{
			this.x = _x;
			this.y = _y;
			this.World = world;
		}
		public void SetColor(string c)
		{
			Color = c;
		}
		public void SetSize(int s)
		{
			Size = s;
		}
	}
}
